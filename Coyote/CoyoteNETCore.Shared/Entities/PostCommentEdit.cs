using System;

namespace CoyoteNETCore.Shared.Entities
{
    public class PostCommentEdit
    {
        private PostCommentEdit()
        {

        }

        public PostCommentEdit(PostComment comment, User editor)
        {
            Comment = comment;
            Editor = editor;
        }

        public int Id { get; private set; }

        public DateTime CreationDate { get; private set; } = DateTime.Now;

        // shouldn't we track what was previously written in this comment?

        public PostComment Comment { get; private set; }

        public User Editor { get; private set; }
    }
}