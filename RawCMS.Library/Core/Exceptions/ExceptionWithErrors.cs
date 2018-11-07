using System;
using System.Collections.Generic;

namespace RawCMS.Library.Core.Exceptions
{
    public class ExceptionWithErrors : Exception
    {
        public List<Error> Errors { get; set; } = new List<Error>();
        public List<Error> Warnings { get; set; } = new List<Error>();
        public List<Error> Infos { get; set; } = new List<Error>();

        public ExceptionWithErrors()
        {
        }

        public ExceptionWithErrors(List<Error> Errors) : this(Errors, null)
        {
        }

        public ExceptionWithErrors(List<Error> Errors, Exception source) : base("", source)
        {
            this.Errors = Errors;
        }
    }
}