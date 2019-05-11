﻿namespace CoyoteNETCore.Shared
{
    public class EmailMessageType
    {
        private EmailMessageType()
        {

        }

        public EmailMessageType(string type)
        {
            Type = type;
        }

        public int Id { get; }

        public string Type { get; set; }
    }
}