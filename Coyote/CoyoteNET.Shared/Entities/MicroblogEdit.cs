using System;

namespace CoyoteNET.Shared.Entities
{
    public class MicroblogEdit
    {
        private MicroblogEdit()
        {

        }

        public MicroblogEdit(MicroblogEntry post, User editor)
        {
            Post = post;
            Editor = editor;
        }

        public int Id { get; private set; }

        public DateTime CreationDate { get; private set; } = DateTime.Now;

        // shouldn't we track what was previously writen in this post?

        public MicroblogEntry Post { get; private set; }

        public User Editor { get; private set; }
    }
}