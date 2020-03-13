Data processing Lambdas are used to alter regular save behaviour. Using such lambdas you can:

- compute some value
- add some validation step
- add per-user filters
- hide fields
- everithing else you need

### Data saving pipeline

Data is saved using a save pipeline. Current implemented stages are:

1. Pre-Save
2. Post-Save

### DataProcessLambda

Implementing a DataProcessLambda will let you choose in which phases you want to be triggered. Multiple phases can be binded as in example

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

This class is shortcut to bind presave or postsave event

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

### Javascript Lambdas

For entities is possible define custom lambdas writed on javascript language on this events:

- PreSave
- PostSave
- PreDelete
- PostDelete

![JS Lambdas](assets/jslambdas.png)

this is an example of javascript code for calculate preview book using ISBN code

```js
var client = new RAWCMSRestClient();
var request = new RAWCMSRestClientRequest();
var bibkey = "ISBN:" + item.ISBN13;
request.Url = "https://openlibrary.org/api/books?format=JSON&bibkeys=" + bibkey;
request.Method = "GET";
var response = client.Execute(request);
var data = JSON.parse(response.Data);
//set result propoerty
item.PreviewUrl = data[bibkey].preview_url;
```
