using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.Shared.Entities
{
    public class Subscription
    {
        protected Subscription()
        {

        }

        protected Subscription(User u)
        {
            Subscriber = u;
        }

        public int Id { get; set; }

        public User Subscriber { get; set; }
    }
}
