using System;
using System.Collections.Generic;
using System.Text;

namespace ResultTypes
{
    public interface IDataResult<out T>:IResult
    {
        T Data { get; }
    }
}
