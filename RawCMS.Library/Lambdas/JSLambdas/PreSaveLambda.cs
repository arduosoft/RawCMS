using Newtonsoft.Json.Linq;
using RawCMS.Library.Core.Enum;
using RawCMS.Library.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Lambdas.JSLambdas
{
    public class PreSaveLambda : JsDispatcher
    {
        public override PipelineStage Stage => PipelineStage.PreOperation;

        public override DataOperation Operation => DataOperation.Write;

        public override string Name => "PreSaveLambda";

        public override string Description => "PreSaveLambda";

        public PreSaveLambda(EntityService entityService) : base(entityService)
        {

        }
    }
}
