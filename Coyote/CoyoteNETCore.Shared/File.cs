using System;
using System.Collections.Generic;
using System.IO;

namespace CoyoteNETCore.Shared
{
    public class File
    {
        private File()
        {

        }

        public File(string userFileName, string physicalFileName, User uploadedBy)
        {
            UserFileName = userFileName;
            PhysicalFileName = physicalFileName;
            UploadedBy = uploadedBy;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public string UserFileName { get; set; }

        public string PhysicalFileName { get; set; }

        public int UploadedById { get; private set; }

        public User UploadedBy { get; private set; }

        // public ICollection<UserFile> DownloadedBy { get; private set; } = new List<UserFile>();
    }
}