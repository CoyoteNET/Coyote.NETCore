namespace CoyoteNETCore.Application
{
    public class Error
    {
        public ErrorType ErrorType { get; }
        public string Description { get; }

        public Error(ErrorType errorType, string description)
        {
            ErrorType = errorType;
            Description = description;
        }

    }
}