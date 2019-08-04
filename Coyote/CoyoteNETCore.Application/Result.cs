using System;
using System.Collections.Generic;

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

        public Result(ErrorType errorType, IEnumerable<string> descriptions)
        {
            Error = new Error(errorType, string.Join(Environment.NewLine, descriptions));
        }

        public Result(T value)
        {
            Value = value;
        }
    }
}