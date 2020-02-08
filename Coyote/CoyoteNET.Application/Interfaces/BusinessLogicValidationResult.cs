using CoyoteNET.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoyoteNET.Application.Interfaces
{
    interface IBusinessLogicValidation<T>
    {
        Task<(bool Success, IEnumerable<string> Result)> Verify(T ValidationObject);
    }
}
