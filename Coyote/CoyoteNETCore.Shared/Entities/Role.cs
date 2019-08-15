namespace CoyoteNETCore.Shared.Entities
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

        public int Id { get; private set; }

        public string Name { get; set; }
    }
}