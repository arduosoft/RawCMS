using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

namespace RawCMS.Library.Core.Attributes
{

    public class ParameterValidator : ActionFilterAttribute
    {
        string name;
        string regexp;
        bool negate;

        public ParameterValidator(string name, string regexp, bool negate)
        {
            this.name = name;
            this.regexp = regexp;
            this.negate = negate;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            bool match = Regex.IsMatch(context.RouteData.Values[name] as string, regexp);
            if (negate) match = !match;
            if (!match) context.Exception = new Exception("Forbidden");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //NOP
        }
    }
}
