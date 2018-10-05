using Microsoft.Extensions.Logging;
using RawCMS.Library.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Core.Interfaces
{
   public  interface IRequireLog
    {
        void SetLogger(ILogger logger);
    }
}
