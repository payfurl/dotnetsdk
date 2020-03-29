using System;

namespace payfurl.sdk.Models
{
    public class ApiException : Exception
    {
        public Error ErrorData { get; private set; }

        public ApiException(Error errorData, string error, Exception innerException = null)
            : base(error, innerException)
        {
            this.ErrorData = errorData;
        }
    }
}
