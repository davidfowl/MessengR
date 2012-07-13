using System;
using System.Net;
using System.Text;

namespace MessengR.Client
{
    public static class LoginHelper
    {
        public static Cookie Login(string url, string userName, string password)
        {
            // We're going to login and retrieve the auth token
            var uri = new Uri(new Uri(url, UriKind.Absolute), "Account/Login.ashx");
            var webRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
            webRequest.Headers["Authorization"] = "Basic " + GetAuthHeader(userName, password);
            webRequest.CookieContainer = new CookieContainer();

            using (var response = (HttpWebResponse)webRequest.GetResponse())
            {
                // Return the auth cookie
                return response.Cookies[".ASPXAUTH"];
            }
        }

        private static string GetAuthHeader(string userName, string password)
        {
            string authorizationHeader = userName + ":" + password;
            authorizationHeader = Convert.ToBase64String(Encoding.Default.GetBytes(authorizationHeader));
            return authorizationHeader;
        }
    }
}
