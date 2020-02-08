using System;

namespace CoyoteNET.Shared.Entities
{
    public class ThreadEdit
    {
        private ThreadEdit()
        {

        }

        public ThreadEdit(Thread thread, User editor)
        {
            Thread = thread;
            Editor = editor;
        }

        public int Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        // shouldn't we track what was previously written in this post?

        public Thread Thread { get; private set; }

        public User Editor { get; private set; }
    }
}