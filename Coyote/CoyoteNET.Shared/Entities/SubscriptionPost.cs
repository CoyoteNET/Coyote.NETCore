using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNET.Shared.Entities
{
    public class SubscriptionPost : Subscription
    {
        private SubscriptionPost()
        {

        }

        public SubscriptionPost(User u) : base(u)
        {

        }

        public int SubscriptionObjectId { get; set; }

        public Post SubscriptionObject { get; set; }
    }
}
