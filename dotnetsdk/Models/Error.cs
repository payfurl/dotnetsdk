using System;
using System.Collections.Generic;

namespace evertech.sdk.Models
{
    public class Error
    {
        public int HttpStatus { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> Details { get; set; }
        public string Resource { get; set; }
        public string GatewayMessage { get; set; }
        public string GatewayCode { get; set; }
    }
}
