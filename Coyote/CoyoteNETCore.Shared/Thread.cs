using System;
using System.Collections.Generic;

namespace CoyoteNETCore.Shared
{
    public class Thread
    {
        private Thread()
        {

        }

        public Thread(ThreadCategory category, string tags, string title, User author)
        {
            Category = category;
            Tags = tags;
            Title = title;
            Author = author ?? throw new Exception();
        }

        public int Id { get; private set; }

        public DateTime CreationDate { get; private set; } = DateTime.Now;

        public ThreadCategory Category { get; set; }

        public User Author { get; set; }

        public string Tags { get; set; }

        public string Title { get; set; }

        public ICollection<Post> Posts { get; private set; } = new List<Post>();

        public ICollection<ThreadEdit> ThreadEdits { get; private set; } = new List<ThreadEdit>();

        public ICollection<Post> Posts { get; private set; } = new List<Post>();

        public ICollection<SubscriptionThread> Subscribers { get; private set; } = new List<SubscriptionThread>();
    }
}
