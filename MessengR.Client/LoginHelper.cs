using System;
using System.Net;
using System.Text;

namespace MessengR.Client
{
    public static class LoginHelper
    {
        public static AuthenticationResult Login(string url, string userName, string password)
        {
            // We're going to login and retrieve the auth token
            var uri = new Uri(new Uri(url, UriKind.Absolute), "Account/Login.ashx");
            var webRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
            var authResult = new AuthenticationResult() { StatusCode = HttpStatusCode.Unused };
            webRequest.Credentials = new NetworkCredential(userName, password);
            webRequest.CookieContainer = new CookieContainer();

            try
            {
                using (var response = (HttpWebResponse)webRequest.GetResponse())
                {
                    authResult.StatusCode = response.StatusCode;
                    authResult.AuthCookie = response.Cookies[".ASPXAUTH"];

                    // Return the auth cookie
                    return authResult;
                }
            }
            catch (WebException wex)
            {
                var response = wex.Response as HttpWebResponse;
                if (response != null)
                {
                    authResult.StatusCode = response.StatusCode;
                    authResult.Message = wex.Message;
                }
                return authResult;
            }
        }
    }
}
