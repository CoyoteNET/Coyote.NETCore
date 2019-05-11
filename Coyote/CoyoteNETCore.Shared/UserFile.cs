using System;

namespace CoyoteNETCore.Shared
{
    public class UserFile
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public Guid FileId { get; set; }

        public File File { get; set; }
    }
}