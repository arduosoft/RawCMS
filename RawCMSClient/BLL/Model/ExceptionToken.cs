using System;

namespace RawCMSClient.BLL.Model
{
    public class ExceptionToken: Exception
    {
        public string Code { get; set; }
        public string OriginalCode { get; set; }
        public string Message { get; set; }

        public ExceptionToken(string Code, string message) : this(Code, message, null)
        {
            this.Code = Code;
            this.Message = message;

        }

        public ExceptionToken(string Code, string message, Exception inner) : base(message, inner)
        {
            this.Code = this.OriginalCode = Code;
            this.Message = message;
        }

    }
}
