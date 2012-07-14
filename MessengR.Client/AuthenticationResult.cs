using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MessengR.Client
{
    public class AuthenticationResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public Cookie Entity { get; set; }
    }
}
