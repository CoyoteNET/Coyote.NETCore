using System;
using System.Collections.Generic;

namespace CoyoteNET.Shared.Entities
{
    public class MicroblogEntry
    {
        private MicroblogEntry()
        {

        }

        public MicroblogEntry(string content, User author, string tags)
        {
            Content = content;
            Author = author;
            Tags = tags;
        }

        public int Id { get; private set; }

        public string Content { get; set; }

        public User Author { get; private set; }

        public string Tags { get; set; }

        public DateTime CreationDate { get; private set; } = DateTime.Now;

        public ICollection<MicroblogEdit> Editions /* better wording needed */ { get; private set; } = new List<MicroblogEdit>();

        public ICollection<SubscriptionMicroblogEntry> Subscribers { get; private set; } = new List<SubscriptionMicroblogEntry>();
    }
}
