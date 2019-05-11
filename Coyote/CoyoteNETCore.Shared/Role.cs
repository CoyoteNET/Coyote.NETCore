namespace CoyoteNETCore.Shared
{
    public class Role
    {
        public Role()
        {

        }

        public Role(string name)
        {
            Name = name;
        }

        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; set; }
    }
}