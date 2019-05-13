using System;

namespace CoyoteNETCore.Shared
{
    public class PostComment
    {
        private PostComment()
        {

        }

        public PostComment(string content, Post post, User author)
        {
            Content = content;
            Post = post;
            Author = author;
        }

        public int Id { get; private set; }

        public DateTime CreationDate { get; private set; } = DateTime.Now;

        public string Content { get; set; }

        public Post Post { get; private set; }

        public User Author { get; private set; }
    }
}