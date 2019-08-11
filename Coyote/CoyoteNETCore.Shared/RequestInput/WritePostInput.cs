using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.Shared.RequestInput
{
    public class WritePostInput
    {
        public int? ThreadId { get; set; }

        public string Body { get; set; }
    }
}
