using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace RawCMS.Library.Core
{
    public abstract class HttpLambda : Lambda
    {
        public abstract object Execute(HttpContext request);
    }
}
