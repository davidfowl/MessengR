using System.Net;

namespace MessengR.Client
{
    public class AuthenticationResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public Cookie AuthCookie { get; set; }
    }
}
