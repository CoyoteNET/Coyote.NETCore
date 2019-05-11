using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.Shared
{
    public class Notification
    {
        public Notification()
        {

        }

        public Notification(string name, string description, User user)
        {
            Name = name;
            Description = description;
            Receiver = user;
        }

        public int Id { get; }

        public string Name { get; }

        public string Description { get; }
        
        public DateTime BroadcastedAt { get; } = DateTime.Now;

        public User Receiver { get; }
    }
}
