using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.IO;
using Webyonet;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string ali = "{\"newid\":[\"222\",\"666\"],\"editid\":[[\"1001\",\"sdfsdfsadas\"],[\"1002\",\"dsfsdfsdlşljkljkl\"],[\"1003\",\"jkhkjhkj\"]]}";

        JObject j = JObject.Parse(ali);
        JArray ja = (JArray)j["editid"];

        if (Request.Cookies["FacebookUserInfo"] != null)
        {
           lblId.Text = Request.Cookies["FacebookUserInfo"].Values["id"];
           lblusername.Text = Request.Cookies["FacebookUserInfo"].Values["userName"];
           lblfirstname.Text = Request.Cookies["FacebookUserInfo"].Values["firsName"];
           lbllastname.Text = Request.Cookies["FacebookUserInfo"].Values["lastName"];
           lblbirthday.Text = Request.Cookies["FacebookUserInfo"].Values["birthday"];
           lbllocation.Text = Request.Cookies["FacebookUserInfo"].Values["location"];
           lblemail.Text = Request.Cookies["FacebookUserInfo"].Values["email"];
           lblschoolName.Text = Request.Cookies["FacebookUserInfo"].Values["schoolName"];
           lblschoolsectionname.Text = Request.Cookies["FacebookUserInfo"].Values["schoolSectionName"];
           imguser.ImageUrl = Request.Cookies["FacebookUserInfo"].Values["imageUrl"];
        }
    }
    protected void getfrient_Click(object sender, EventArgs e)
    {
        FacebookUserInfo fbUserInfo = new FacebookUserInfo();
        string accessToken = Request.Cookies["FacebookUserInfo"].Values["accessToken"];

        JavaScriptSerializer js = new JavaScriptSerializer();
        var jsSerialize = js.Deserialize<FacebookGetFrientList.firients>(fbUserInfo.getUserFrientList(accessToken));

        foreach (var item in jsSerialize.data)
        {
            Response.Write(item.id + " " + item.name + " " + item.link + " " + item.picture + "<br/>");
        }
    }
    protected void textpost_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["FacebookUserInfo"] != null)
        {
            string content = txtpostinput.Text;
            string token = Request.Cookies["FacebookUserInfo"].Values["accessToken"];
            string link = @"http://webyonet.net";
            FacebookUserWallPost requestText = new FacebookUserWallPost();
            requestText.wallStreamTextPost(token, content, "webyonet", link);

            
        }
    }
}