using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.Shared.RequestInput
{
    public class EditPostInput
    {
        public int? PostId { get; set; }

        public string Body { get; set; }
    }
}
