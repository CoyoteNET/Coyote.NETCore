using System;

namespace CoyoteNETCore.Shared
{
    public class EmailLog
    {
        private EmailLog()
        {

        }

        public EmailLog(string to, string message, string topic, EmailMessageType type)
        {
            To = to;
            Message = message;
            Topic = topic;
            Type = type;
        }

        public int Id { get; }

        public string To { get; }

        public string Message { get; }

        public string Topic { get; }

        public DateTime SendingDate { get; } = DateTime.Now;

        public bool SentSuccessfully { get; set; }

        public EmailMessageType Type { get; }
    }
}
