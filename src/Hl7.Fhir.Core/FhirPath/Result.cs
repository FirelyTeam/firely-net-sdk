using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FhirPath
{
    public class Result
    {
        public static Result<T> ForValue<T>(T value)
        {
            return new Result<T>(value);
        }
    }

    public class Result<T>
    {
        public Result(T initial)
        {
            Value = initial;
        }

        public T Value { get; private set; }
    }

    public interface IResult<T>
    {
        T Value { get; }
    }
}
