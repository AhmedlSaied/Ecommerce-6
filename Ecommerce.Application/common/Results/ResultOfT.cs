using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.common.Results
{
    public  class Result<T> : Result
    {
        private readonly T? _value;

        private Result(
            T? value,
            bool isSuccess,
            Error error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        public T Value
        {
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException(
                        "Cannot access the value of a failed result.");
                }

                return _value!;
            }
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(
                value,
                true,
                Error.None);
        }

        public static new Result<T> Failure(Error error)
        {
            return new Result<T>(
                default,
                false,
                error);
        }
    }
}
