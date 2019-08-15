using System;
using System.Collections.Generic;

namespace CoyoteNETCore.Shared.ResultHandling
{
    public class Error
    {
        public ErrorType ErrorType { get; set; }

        public string ErrorMessage => string.Join(Environment.NewLine, ErrorMessages);

        public IEnumerable<string> ErrorMessages { get; set; } = new List<string>();

        public Error(ErrorType errorType, string description)
        {
            ErrorType = errorType;
            (ErrorMessages as List<string>).Add(description);
        }

        public Error(ErrorType errorType, IEnumerable<string> errorMessages)
        {
            ErrorType = errorType;
            ErrorMessages = errorMessages;
        }

        public Error(string description)
        {
            ErrorType = ErrorType.BadRequest;
            (ErrorMessages as List<string>).Add(description);
        }

        public Error(IEnumerable<string> errorMessages)
        {
            ErrorType = ErrorType.BadRequest;
            ErrorMessages = errorMessages;
        }

        // JsonConstructor
        public Error()
        {

        }
    }
}