using System;

namespace CoyoteNETCore.Shared
{
    public class MicroblogCommentEdit
    {
        private MicroblogCommentEdit()
        {

        }

        public MicroblogCommentEdit(MicroblogComment comment, User editor)
        {
            Comment = comment;
            Editor = editor;
        }

        public int Id { get; private set; }

        public DateTime CreationDate { get; private set; } = DateTime.Now;

        // shouldn't we track what was previously written in this comment?

        public MicroblogComment Comment { get; private set; }

        public User Editor { get; private set; }
    }
}