using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.common.Results
{
    public sealed class Error
    {
        public static readonly Error None = new Error(
            string.Empty,
            string.Empty,
            ErrorType.None);

        public Error(
            string code,
            string message,
            ErrorType type)
        {
            Code = code;
            Message = message;
            Type = type;
        }

        public string Code { get; }

        public string Message { get; }

        public ErrorType Type { get; }
    }
}
