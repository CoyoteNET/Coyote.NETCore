using System;

namespace CoyoteNETCore.Shared
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

        public int Id { get; }

        public DateTime CreationDate { get; } = DateTime.Now;

        // shouldn't we track what was previously written in this comment?

        public PostComment Comment { get; }

        public User Editor { get; }
    }
}