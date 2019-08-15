namespace CoyoteNETCore.Shared.Entities
{
    public class ForumSection
    {
        private ForumSection()
        {

        }

        public ForumSection(string name)
        {
            Name = name;
        }

        public int Id { get; private set; }

        public string Name { get; set; }
    }
}