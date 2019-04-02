using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.Shared
{
    public class Thread : Entity
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public User Author { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
