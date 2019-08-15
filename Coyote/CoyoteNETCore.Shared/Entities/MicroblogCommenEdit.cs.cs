using System;

namespace CoyoteNETCore.Shared.Entities
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

        // shouldn't we track what was previously writen in this comment?

        public MicroblogComment Comment { get; private set; }

        public User Editor { get; private set; }
    }
}