using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.Shared.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public FileDTO Avatar { get; set; }
    }
}
