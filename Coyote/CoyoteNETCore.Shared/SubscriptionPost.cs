using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.Shared
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
