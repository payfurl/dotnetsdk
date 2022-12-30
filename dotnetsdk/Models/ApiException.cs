using System;

namespace payfurl.sdk.Models
{
    public class ApiException : Exception
    {
        public Error ErrorData { get; private set; }
        public int Code {get; private set;}
        public bool IsRetryable {get; private set;}

        public ApiException(Error errorData, string error, int code, bool isRetryable, Exception innerException = null)
            : base(error, innerException)
        {
            ErrorData = errorData;
            Code = code;
            IsRetryable = isRetryable;
        }
    }
}
