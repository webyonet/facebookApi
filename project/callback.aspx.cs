using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;
using Hammock;
using Webyonet;
using System.Net;
using System.Web;

public partial class callback : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["code"]) && !Page.IsPostBack)
        {
            getFacebookData();
        }
        else
        {
            string callbackUrl = "http://localhost:27393/facebookyeni/callback.aspx";

            Response.Redirect(string.Format("https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&scope=email,user_birthday,friends_about_me,offline_access,publish_stream", ConfigurationManager.AppSettings["FacebookClientId"], callbackUrl));  
        }
    }

    void getFacebookData()
    {
        string CallbackUrl = "http://localhost:27393/facebookyeni/callback.aspx";
        var client = new RestClient { Authority = "https://graph.facebook.com/oauth/" };
        var request = new RestRequest { Path = "access_token" };

        request.AddParameter("client_id", ConfigurationManager.AppSettings["FacebookClientId"]);
        request.AddParameter("redirect_uri", CallbackUrl);
        request.AddParameter("client_secret", ConfigurationManager.AppSettings["FacebookApplicationSecret"]);
        request.AddParameter("code", Request["code"]);

        RestResponse response = client.Request(request);
        StringDictionary result = FacebookConnect.ParseQueryString(response.Content);
        string aToken = result["access_token"];

        responseFacebookUserInfo(aToken);
    }

    void responseFacebookUserInfo(string sToken)
    {
        var client = new RestClient { Authority = "https://graph.facebook.com/" };
        var request = new RestRequest { Path = "me" };
        request.AddParameter("access_token", sToken);
        RestResponse facebookResponse = client.Request(request);

        JavaScriptSerializer ser = new JavaScriptSerializer();
        var FacebookUser = ser.Deserialize<FacebookUser>(facebookResponse.Content);

        HttpCookie cookie = new HttpCookie("FacebookUserInfo");
        cookie.Expires = DateTime.Now.AddDays(1);

        cookie.Values.Add("id", FacebookUser.id);
        cookie.Values.Add("firsName", FacebookUser.first_name);
        cookie.Values.Add("lastName", FacebookUser.last_name);
        cookie.Values.Add("userName", FacebookUser.username);
        cookie.Values.Add("birthday", FacebookUser.birthday);
        cookie.Values.Add("email", FacebookUser.email);
        cookie.Values.Add("location", FacebookUser.location.name);
        cookie.Values.Add("imageUrl", string.Format("http://graph.facebook.com/{0}/picture?type=small", FacebookUser.id));
        cookie.Values.Add("accessToken", sToken);

        foreach (var item in FacebookUser.education)
        {
            cookie.Values.Add("schoolName", item.school.name);
            foreach (var items in item.concentration)
            {
                cookie.Values.Add("schoolSectionName", items.name);
                break;
            }
            break;
        }

        Response.Cookies.Add(cookie);

        ClientScript.RegisterStartupScript(this.GetType(), "pageClose", "<script>closePage();</script>");
    }
}