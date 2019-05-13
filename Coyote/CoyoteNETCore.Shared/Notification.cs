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

        // Sender in most cases will be User called "System" which will be first user in database and he'll be used for System purposes.

        public Notification(string name, string description, User sender, User receiver)
        {
            Name = name;
            Description = description;
            Receiver = receiver;
            Sender = sender;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }
        
        public DateTime BroadcastedAt { get; private set; } = DateTime.Now;

        public User Sender { get; private set; }
        public User Receiver { get; private set; }
    }
}
