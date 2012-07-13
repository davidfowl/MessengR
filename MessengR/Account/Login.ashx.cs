using System;
using System.Text;
using System.Web;
using System.Web.Security;
using MembershipService.Common;

namespace MessengR.Account
{
    /// <summary>
    /// Summary description for Login
    /// </summary>
    public class LoginHandler : IHttpHandler
    {
        // HTTP 1.1 Authorization header
        public const string HttpAuthorizationHeader = "Authorization";

        // HTTP 1.1 Basic Challenge Scheme Name
        public const string HttpBasicSchemeName = "Basic";

        // HTTP 1.1 Credential username and password separator
        public const char HttpCredentialSeparator = ':';

        public void ProcessRequest(HttpContext context)
        {
            DoNotRedirectToLoginModule.ApplyForRequest(new HttpContextWrapper(context));

            string username;
            string password;
            if (TryExtractBasicAuthCredentials(context.Request, out username, out password) &&
                Membership.ValidateUser(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, createPersistentCookie: false);
            }
            else
            {
                context.Response.StatusCode = 401;
                context.Response.Status = "401 Unauthorized";
                context.Response.AddHeader("WWW-Authenticate", "Basic");
            }
        }

        public static bool TryExtractBasicAuthCredentials(HttpRequest request, out string username, out string password)
        {
            string authorizationHeader = request.Headers[HttpAuthorizationHeader];

            return TryExtractBasicAuthCredentialsFromHeader(authorizationHeader, out username, out password);
        }

        public static bool TryExtractBasicAuthCredentialsFromHeader(string authorizationHeader, out string username, out string password)
        {
            username = null;
            password = null;

            if (String.IsNullOrEmpty(authorizationHeader))
            {
                return false;
            }

            string verifiedAuthorizationHeader = authorizationHeader.Trim();
            if (verifiedAuthorizationHeader.IndexOf(HttpBasicSchemeName) != 0)
            {
                return false;
            }

            // Get the credential payload
            verifiedAuthorizationHeader = verifiedAuthorizationHeader.Substring(HttpBasicSchemeName.Length, verifiedAuthorizationHeader.Length - HttpBasicSchemeName.Length).Trim();

            return TryParseBasicAuthCredentialsFromHeaderParameter(verifiedAuthorizationHeader, out username, out password);
        }

        public static bool TryParseBasicAuthCredentialsFromHeaderParameter(string verifiedAuthorizationHeader, out string username, out string password)
        {
            username = null;
            password = null;

            // Decode the base 64 encoded credential payload 
            byte[] credentialBase64DecodedArray = Convert.FromBase64String(verifiedAuthorizationHeader);

            string decodedAuthorizationHeader = Encoding.UTF8.GetString(credentialBase64DecodedArray, 0, credentialBase64DecodedArray.Length);

            // get the username, password, and realm 
            int separatorPosition = decodedAuthorizationHeader.IndexOf(HttpCredentialSeparator);

            if (separatorPosition <= 0)
            {
                return false;
            }

            username = decodedAuthorizationHeader.Substring(0, separatorPosition).Trim();
            password = decodedAuthorizationHeader.Substring(separatorPosition + 1).Trim();

            if (String.IsNullOrEmpty(username))
            {
                return false;
            }

            return true;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}