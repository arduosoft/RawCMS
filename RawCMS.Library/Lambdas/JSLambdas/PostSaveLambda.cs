using Newtonsoft.Json.Linq;
using RawCMS.Library.Core.Enum;
using RawCMS.Library.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Lambdas.JSLambdas
{
    public class PostSaveLambda : JsDispatcher
    {
        public override PipelineStage Stage => PipelineStage.PostOperation;

        public override DataOperation Operation => DataOperation.Write;

        public override string Name => "PostSaveLambda";

        public override string Description => "PostSaveLambda";

        public PostSaveLambda(EntityService entityService) : base(entityService)
        {

        }
    }
}
