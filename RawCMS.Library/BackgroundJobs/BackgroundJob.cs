using Newtonsoft.Json.Linq;
using RawCMS.Library.Lambdas;

namespace RawCMS.Library.BackgroundJobs
{
    public abstract class BackgroundJobInstance : Lambda
    {
        public abstract string CronExpression { get; }

        public abstract void Execute(JObject data);
    }
}