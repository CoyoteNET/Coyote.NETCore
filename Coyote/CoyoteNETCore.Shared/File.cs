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

        public string PhysicalFileName
        {
            get { return PhysicalFileName; }
            set
            {               
                 PhysicalFileName = System.IO.File.Exists(Path.Combine("path_from_config", value)) ? value : throw new FileNotFoundException(value);  
            }
        }

        public User UploadedBy { get; }

        public ICollection<UserFile> DownloadedBy { get; } = new List<UserFile>();
    }
}