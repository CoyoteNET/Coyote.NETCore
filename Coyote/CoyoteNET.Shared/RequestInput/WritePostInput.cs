using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNET.Shared.RequestInput
{
    public class WritePostInput
    {
        public int? ThreadId { get; set; }

        public string Body { get; set; }
    }
}
