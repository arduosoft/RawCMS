using Newtonsoft.Json.Linq;
using RawCMS.Library.Core.Enum;
using RawCMS.Library.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Lambdas.JSLambdas
{
    public class PreDeleteLambda : JsDispatcher
    {
        public override PipelineStage Stage => PipelineStage.PreOperation;

        public override DataOperation Operation => DataOperation.Delete;

        public override string Name => "PreDeleteLambda";

        public override string Description => "PreDeleteLambda";

        public PreDeleteLambda(EntityService entityService) : base(entityService)
        {

        }
    }
}
