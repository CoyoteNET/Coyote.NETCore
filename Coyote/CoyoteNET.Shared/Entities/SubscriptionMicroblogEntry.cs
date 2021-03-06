﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNET.Shared.Entities
{
    public class SubscriptionMicroblogEntry : Subscription
    {

        private SubscriptionMicroblogEntry()
        {

        }

        public SubscriptionMicroblogEntry(User u) : base(u)
        {

        }

        public int SubscriptionObjectId { get; set; }

        public MicroblogEntry SubscriptionObject { get; set; }
    }
}
