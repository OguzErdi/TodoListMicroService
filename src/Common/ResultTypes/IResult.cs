using System;
using System.Collections.Generic;
using System.Text;

namespace ResultTypes
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
