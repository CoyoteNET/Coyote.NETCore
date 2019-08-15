using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.Shared.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int ThreadId { get; set; }

        public UserDTO Author { get; set; }
    }
}
