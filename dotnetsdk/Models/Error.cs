using System;
using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class Error
    {
        public int HttpStatus { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> Details { get; set; }
        public string Resource { get; set; }
        public string GatewayMessage { get; set; }
        public string GatewayCode { get; set; }
        public ErrorCodeType Code {get; set;}
        public bool IsRetryable {get; set;}
        public string Type { get; set; }
    }
}
