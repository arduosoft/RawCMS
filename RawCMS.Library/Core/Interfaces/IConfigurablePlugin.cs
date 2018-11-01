using RawCMS.Library.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Core.Interfaces
{
    public interface IConfigurablePlugin<T> 
    {
        T GetDefaultConfig();
         void SetActualConfig(T config);
    }
}
