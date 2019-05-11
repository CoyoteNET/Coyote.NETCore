using System;

namespace CoyoteNETCore.Shared
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

        public int Id { get; }

        public DateTime CreationDate { get; } = DateTime.Now;

        // shouldn't we track what was previously written in this post?

        public MicroblogEntry Post { get; }

        public User Editor { get; }
    }
}