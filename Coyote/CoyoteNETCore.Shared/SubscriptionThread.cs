using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.Shared
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
