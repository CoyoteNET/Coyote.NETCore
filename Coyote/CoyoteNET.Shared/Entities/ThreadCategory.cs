﻿namespace CoyoteNET.Shared.Entities
{
    public class ThreadCategory
    {
        private ThreadCategory()
        {

        }

        public ThreadCategory(string name, string description, ForumSection section)
        {
            Name = name;
            Description = description;
            Section = section;
        }

        public int Id { get; private set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ForumSection Section { get; set; }
    }
}