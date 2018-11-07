using Microsoft.Extensions.Logging;

namespace RawCMS.Library.Core.Interfaces
{
    public interface IRequireLog
    {
        void SetLogger(ILogger logger);
    }
}