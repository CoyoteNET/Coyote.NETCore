using System.Collections.Generic;

namespace CoyoteNETCore.Shared
{
    public class Post
    {
        private Post()
        {

        }

        public Post(string content, Thread thread, User author)
        {
            Content = content;
            Thread = thread;
            Author = author;
        }

        public int Id { get; set; }

        // public string Title { get; set; } -- should post actually have ``Title``?

        public string Content { get; set; }

        public Thread Thread { get; }

        public User Author { get; }

        public ICollection<PostEdit> Editions /* better wording needed */ { get; } = new List<PostEdit>();

        public ICollection<PostComment> Comments { get; } = new List<PostComment>();
    }
}