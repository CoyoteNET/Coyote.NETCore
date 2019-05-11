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

        public int Id { get; }

        public DateTime CreationDate { get;} = DateTime.Now;

        public string Content { get; set; }

        public Post Post { get; }

        public User Author { get; }
    }
}