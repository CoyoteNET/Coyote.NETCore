using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNET.Shared.Entities
{
    public class SubscriptionThread : Subscription
    {
        private SubscriptionThread()
        {

        }

        public SubscriptionThread(User u) : base(u)
        {

        }

        //public int SubscriptionObjectId { get; set; }

        //public Thread SubscriptionObject { get; set; }
    }
}
