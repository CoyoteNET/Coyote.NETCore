namespace CoyoteNETCore.Application
{
    public class Result<T>
    {
        public T Value { get; }
        public Error Error { get; set; }
        public bool IsSucceeded => Error == null;

        public Result(ErrorType errorType, string description)
        {
            Error = new Error(errorType, description);
        }

        public Result(T value)
        {
            Value = value;
        }
    }
}