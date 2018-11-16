using System;
using System.Collections.Generic;

namespace RawCMS.Library.Core.Exceptions
{
    public class ValidationException : ExceptionWithErrors
    {
        public ValidationException(List<Error> errors, Exception source) : base(errors, source)
        {
        }
    }
}