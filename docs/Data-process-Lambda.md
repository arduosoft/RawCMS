Data process Lambda are used to alter regular save behaviour. Using such lambdas you can:
* compute some value
* add some validation step
* add per-user filters
* hide fields
* everithing else you need

### Data saving pipeline
Data is saved using a save pipeline. Current implemented stage are:

1. Pre-Save
2. Post-Save

### DataProcessLambda
Implementing a DataProcessLambda will let you in which phases you want to be triggered. Multiple phases can be binded as in example

```cs
 public class AuditLambda : DataProcessLambda 
    {
        public override string Name => "LogLambda";
        public override string Description => "Log all";
        public override SavePipelineStage Stage {get  { return SavePipelineStage.PreSave |SavePipelineStage.PostSave; } }
        public override void Execute(string collection, ref JObject Item)
        {
           //Log all
        }
    }
```

### PostSaveLambda, PreaveLambda
This classes are shortcut to bing presave or postsave event

Audit Lambda is a sample. It is triggered on before saving data and it set audit details.

```cs 
 public class AuditLambda : PreSaveLambda
    {
        public override string Name => "AuditLambda";

        public override string Description => "Add audit settings";

        public override void Execute(string collection, ref JObject Item)
        {
            if (!Item.ContainsKey("_id") || string.IsNullOrEmpty(Item["_id"].ToString()))
            {
                Item["_createdon"] = DateTime.Now;
            }

            Item["_modifiedon"] = DateTime.Now;

        }
    }
```

