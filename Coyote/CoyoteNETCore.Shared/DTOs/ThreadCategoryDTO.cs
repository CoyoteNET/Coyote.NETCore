﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.Shared.DTOs
{
    public class ThreadCategoryDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ForumSectionDTO Section { get; set; }
    }
}
