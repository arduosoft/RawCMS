using Newtonsoft.Json.Linq;
using RawCMS.Library.Core.Enum;
using RawCMS.Library.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Lambdas.JSLambdas
{
    public class PostDeleteLambda : JsDispatcher
    {
        public override PipelineStage Stage => PipelineStage.PostOperation;

        public override DataOperation Operation => DataOperation.Delete;

        public override string Name => "PostDeleteLambda";

        public override string Description => "PostDeleteLambda";

        public PostDeleteLambda(EntityService entityService) : base(entityService)
        {

        }
    }
}
