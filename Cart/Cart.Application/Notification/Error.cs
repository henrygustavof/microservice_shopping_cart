namespace Cart.Application.Notification
{
    using System;
    public class Error
    {
        public string Message { get; set; }
        public Exception Cause { get; set; }

        public Error(string message, Exception cause)
        {
            Message = message;
            Cause = cause;
        }
    }
}
