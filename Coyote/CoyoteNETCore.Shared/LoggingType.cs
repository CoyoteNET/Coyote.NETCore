namespace CoyoteNETCore.Shared
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

        public int Id { get; }

        public string Name { get; set; }
    }
}