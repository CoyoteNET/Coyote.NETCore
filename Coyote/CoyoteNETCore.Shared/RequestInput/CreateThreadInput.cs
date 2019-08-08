using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.Shared.RequestInput
{
    public class CreateThreadInput
    {
        public string Body { get; set; }

        public string Title { get; set; }

        public string Tags { get; set; }

        public int? ThreadCategoryId { get; set; }
    }
}
