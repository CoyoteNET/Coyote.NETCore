using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNET.Shared.DTOs
{
    public class ThreadDTO
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public ThreadCategoryDTO Category { get; set; }

        public UserDTO Author { get; set; }

        public string Tags { get; set; }

        public string Title { get; set; }

        public List<PostDTO> Posts { get; set; } = new List<PostDTO>();
    }
}
