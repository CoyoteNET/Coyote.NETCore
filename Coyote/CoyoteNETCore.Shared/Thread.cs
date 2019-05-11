using System;
using System.Collections.Generic;

namespace CoyoteNETCore.Shared
{
    public class Thread
    {
        private Thread()
        {

        }

        public Thread(ThreadCategory category, string tags, string title)
        {
            Category = category;
            Tags = tags;
            Title = title;
        }

        public int Id { get; }

        public DateTime CreationDate { get; } = DateTime.Now;

        public ThreadCategory Category { get; set; }

        public string Tags { get; set; }

        public string Title { get; set; }

        public ICollection<ThreadEdit> ThreadEdits { get; } = new List<ThreadEdit>();
    }
}