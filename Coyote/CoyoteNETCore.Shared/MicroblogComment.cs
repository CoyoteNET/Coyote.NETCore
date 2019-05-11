using System;

namespace CoyoteNETCore.Shared
{
    public class MicroblogComment
    {
        private MicroblogComment()
        {

        }

        public MicroblogComment(string content, DateTime creationDate, MicroblogEntry microblogEntry, User author)
        {
            Content = content;
            CreationDate = creationDate;
            MicroblogEntry = microblogEntry;
            Author = author;
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public MicroblogEntry MicroblogEntry { get; private set; }

        public User Author { get; private set; }
    }
}