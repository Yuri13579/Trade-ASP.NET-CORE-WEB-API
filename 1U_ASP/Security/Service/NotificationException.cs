using System;

namespace _1U_ASP.Security.Service
{
    public class NotificationException : Exception
    {
        public NotificationException(string message, object obj)
            : base(message)
        {
            Parameters = obj;
        }

        public NotificationException(string message, Exception inner, object obj)
            : base(message, inner)
        {
            Parameters = obj;
        }

        public object Parameters { get; set; }
    }
}
