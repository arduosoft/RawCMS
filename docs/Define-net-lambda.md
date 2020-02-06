To create a Lambda just implement a class. Class derived from Lambda will be activated and added to lambda bucket.

This example shows how to implemeent a simple REST Lambda

```cs
  public class DummyRest : RestLambda
    {
        public override string Name => "DummyRest";
        public override string Description => "I'm a dumb dummy request";
        public override JObject Rest(JObject input)
        {
            var result = new JObject();
            result["input"] = input;
            result["now"] = DateTime.Now;
            return result;
        }
    }
```

This can be reached at /api/lambda/dummyrest with body:
```json
{
   "textfield":"text to get back",
}
```
and will return

```json
{
   "input": 
      {
        "textfield":"text to get back",
      },
   "now":"20108-05-05 22:22:22"
}
```

