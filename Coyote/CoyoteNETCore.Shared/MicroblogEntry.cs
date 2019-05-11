using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.Shared
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

        public int Id { get; }

        public string Content { get; set; }

        public User Author { get; }

        public string Tags { get; set; }

        public DateTime CreationDate { get; } = DateTime.Now;

        public ICollection<MicroblogEdit> Editions /* better wording needed */ { get; } = new List<MicroblogEdit>();
    }
}
