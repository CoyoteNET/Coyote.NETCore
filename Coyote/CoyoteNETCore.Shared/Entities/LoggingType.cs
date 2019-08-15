namespace CoyoteNETCore.Shared.Entities
{
    public class LoggingType
    {
        private LoggingType()
        {

        }

        public LoggingType(string name)
        {
            Name = name;
        }

        public int Id { get; private set; }

        public string Name { get; set; }
    }
}