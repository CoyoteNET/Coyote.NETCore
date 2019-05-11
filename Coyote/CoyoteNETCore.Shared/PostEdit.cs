using System;

namespace CoyoteNETCore.Shared
{
    public class PostEdit
    {
        private PostEdit()
        {

        }

        public PostEdit(Post post, User editor)
        {
            Post = post;
            Editor = editor;
        }

        public int Id { get; }

        public DateTime CreationDate { get; } = DateTime.Now;

        // shouldn't we track what was previously written in this post?

        public Post Post { get; }

        public User Editor { get; }
    }
}