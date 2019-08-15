using System;

namespace CoyoteNETCore.Shared.Entities
{
    public class UserFile
    {
        private UserFile()
        {

        }

        public UserFile(User user, File file)
        {
            this.User = user;
            this.UserId = user.Id;

            this.File = file;
            this.UserId = user.Id;
        }

        public int UserId { get; set; }

        public User User { get; set; }

        public Guid FileId { get; set; }

        public File File { get; set; }
    }
}