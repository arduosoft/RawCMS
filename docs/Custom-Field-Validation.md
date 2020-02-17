To manage _Entity Validation_ there are many FieldValidator used to check if a field value is valid for such schema definition.

Standard validator are shipped with RawCMS:

- Number
- Integer
- DateTime
- Text

you can add your own type, by just implementing the below class:

```cs
    public class MyTypeValidator: FieldTypeValidator
    {
        public string Type =>"MyTypeValidator";
        public  List<Error> Validate(JObject input, Field field)
        {
           // DO CHECK HERE
        }
    }
```

the value of _Type_ should match with the field type in schema validation.
