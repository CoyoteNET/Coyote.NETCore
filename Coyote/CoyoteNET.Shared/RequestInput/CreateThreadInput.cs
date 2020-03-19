using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNET.Shared.RequestInput
{
    public class CreateThreadInput
    {
        public CreateThreadInput(string body, string title, string tags, int threadCategoryId)
        {
            Body = body;
            Title = title;
            Tags = tags;
            ThreadCategoryId = threadCategoryId;
        }

        public string Body { get; set; }

        public string Title { get; set; }

        public string Tags { get; set; } = "";

        public int? ThreadCategoryId { get; set; }
    }
}
