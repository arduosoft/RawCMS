using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Core
{
    public enum SavePipelineStage
    {
        PreSave = 0x0,
        PostSave = 0x1,
    }

    public abstract class DataProcessLambda : Lambda
    {
        public abstract SavePipelineStage Stage { get; }

        public abstract void Execute(string collection, ref JObject Item);
    }


    public abstract class PostSaveLambda : DataProcessLambda
    {
        public override SavePipelineStage Stage {get  { return SavePipelineStage.PostSave; } }
    }

    public abstract class PreSaveLambda : DataProcessLambda
    {
        public override SavePipelineStage Stage { get { return SavePipelineStage.PreSave; } }
    }
}
