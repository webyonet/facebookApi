using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Webyonet
{
    public class FacebookConnect
    {
        internal static void VideoEmbedPost(string data) {
            try
            {
                byte[] postData = Encoding.UTF8.GetBytes(data);
                HttpWebRequest requestVideo = (HttpWebRequest)HttpWebRequest.Create("https://graph.facebook.com/me/feed");
                requestVideo.Method = WebRequestMethods.Http.Post;
                requestVideo.ContentType = "application/x-www-form-urlencoded";
                requestVideo.ContentLength = postData.Length;
                Stream st = requestVideo.GetRequestStream();
                st.Write(postData, 0, postData.Length);
                st.Flush();
                st.Close();
            }
            catch (Exception)
            {}
        }

        internal static void WallTextPost(string data) {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://api.facebook.com/method/stream.publish?" + data);
                request.GetResponse();
            }
            catch (Exception)
            {}
        }

        internal static string UserFrientList(string token) 
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://graph.facebook.com/me/friends?fields=id,name,picture,link&access_token=" + token);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string frientList = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return frientList;
        }

        public static StringDictionary ParseQueryString(string qstring)
        {
            qstring = qstring + "&";
            var outc = new StringDictionary();
            var r = new Regex(@"(?<name>[^=&]+)=(?<value>[^&]+)&", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            IEnumerator enums = r.Matches(qstring).GetEnumerator();
            while (enums.MoveNext() && enums.Current != null)
            {
                outc.Add(((Match)enums.Current).Result("${name}"), ((Match)enums.Current).Result("${value}"));
            }
            return outc;
        }
        
    }

    public class FacebookUserWallPost : FacebookConnect {
        StringBuilder sb = new StringBuilder();
        //public Tocken tk { get; set; }
        
        /*youtube vimeo embed post*/
        public void wallVideoEmbedPost(string token, string videoLink)
        {
            string data = "access_token=" + token + "&link=" + videoLink;
            FacebookConnect.VideoEmbedPost(data);
        }
        public void wallVideoEmbedPost(string token, string videoLink, string message)
        {
            string data = "access_token=" + token + "&link=" + videoLink + "&message=" + message;
            FacebookConnect.VideoEmbedPost(data);
        }
        public void wallVideoEmbedPost(string token, string videoLink, string videoSource, string videoPicture)
        {
            string data = "access_token=" + token + "&link=" + videoLink + "&source=" + videoSource + "&picture=" + videoPicture;
            FacebookConnect.VideoEmbedPost(data);
        }
        public void wallVideoEmbedPost(string token, string videoLink, string videoSource, string videoPicture, string message)
        {
            string data = "access_token=" + token + "&link=" + videoLink + "&source=" + videoSource + "&picture=" + videoPicture + "&message=" + message;
            FacebookConnect.VideoEmbedPost(data);
        }
        public void wallVideoEmbedPost(string token, string videoLink, string videoSource, string videoPicture, string message, string videoDescription, string videoName)
        {
            string data = "access_token=" + token + "&link=" + videoLink + "&source=" + videoSource + "&picture=" + videoPicture + "&message=" + message + "&description=" + videoDescription + "&name=" + videoName;
            FacebookConnect.VideoEmbedPost(data);
        }

        /*user wall text post*/
        public void wallStreamTextPost(string token, string message)
        {
            sb.Clear();
            sb.Append("message=" + HttpUtility.UrlEncode(message));
            sb.Append("&access_token=" + token);
            FacebookConnect.WallTextPost(sb.ToString());
        }
        public void wallStreamTextPost(string token, string message, string linkText, string linkHref)
        {
            string url = "[{\"text\":\"" + linkText + "\",\"href\":\"" + linkHref + "\"}]";
            sb.Clear();
            sb.Append("message=" + HttpUtility.UrlEncode(message));
            sb.Append("&action_links=" + HttpUtility.UrlEncode(url));
            sb.Append("&access_token=" + token);
            FacebookConnect.WallTextPost(sb.ToString());
        }
    }

    public class FacebookUserInfo : FacebookConnect
    {
        /*user frient list*/
        public string getUserFrientList(string token)
        {
            return UserFrientList(token);
        }
    }

    public class FacebookGetFrientList 
    {
        public class firients
        {
            public firients() 
            { 
                data = new List<FacebookSubFrients>(); 
            }
            public List<FacebookSubFrients> data { get; set; }
        }

        public class FacebookSubFrients
        {
            public string id { get; set; }
            public string name { get; set; }
            public string link { get; set; }
            public string picture { get; set; }
        }
    }

    public class FacebookUser
    {
        public string id { get; set; }
        public string name { get; set; }
        public string about { get; set; }
        public string link { get; set; }
        public string email { get; set; }
        public string birthday { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string bio { get; set; }
        public string quotes { get; set; }
        public string gender { get; set; }
        public locations location { get; set; }
        public List<FacebookSubEducations> education { get; set; }
    }

    public class FacebookSubEducations
    {
        public School school { get; set; }
        public List<Concentration> concentration { get; set; }
    }

    public class Concentration
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class School
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class locations
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}