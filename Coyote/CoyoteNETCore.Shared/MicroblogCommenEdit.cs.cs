using System;

namespace CoyoteNETCore.Shared
{
    public class MicroblogCommenEdit
    {
        private MicroblogCommenEdit()
        {

        }

        public MicroblogCommenEdit(MicroblogComment comment, User editor)
        {
            Comment = comment;
            Editor = editor;
        }

        public int Id { get; }

        public DateTime CreationDate { get; } = DateTime.Now;

        // shouldn't we track what was previously written in this comment?

        public MicroblogComment Comment { get; }

        public User Editor { get; }
    }
}