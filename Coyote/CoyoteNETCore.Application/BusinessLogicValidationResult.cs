﻿using CoyoteNETCore.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoyoteNETCore.Application
{
    interface BusinessLogicValidation<T>
    {
        Task<(bool Success, IEnumerable<string> Result)> Verify(T ValidationObject);
    }
}
