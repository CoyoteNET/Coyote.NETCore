using System;

namespace CoyoteNETCore.Shared.Entities
{
    public class PostEdit
    {
        private PostEdit()
        {

        }

        public PostEdit(Post post, User editor)
        {
            Post = post; // reference loop?????????????
            Editor = editor;
        }

        public int Id { get; private set; }

        public DateTime CreationDate { get; private set; } = DateTime.Now;

        // shouldn't we track what was previously written in this post?

        public Post Post { get; private set; }

        public User Editor { get; private set; }
    }
}