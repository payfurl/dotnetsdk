using payfurl.sdk.Models;

namespace payfurl.sdk.Helpers
{
    public static class ErrorCodeTypeTranslator
    {
        private const string TypeBaseUrl = "https://docs.payfurl.com/errorcodes.html#";
        
        public static string GetErrorCodeUrlByCode(ErrorCodeType code)
        {
            return TypeBaseUrl + (int)code;
        }
    }
}