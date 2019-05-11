using System;
using System.Collections.Generic;
using System.Text;

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

        public int Id { get; private set; }

        public string To { get; private set; }

        public string Message { get; private set; }

        public string Topic { get; private set; }

        public DateTime SendingDate { get; private set; } = DateTime.Now;

        public bool SentSuccessfully { get; set; }

        public EmailMessageType Type { get; private set; }
    }
}
