SchemaValidationLambda is the base type to hook validation. In the save pipeline validation is triggered so all derived class will be used to manage data validation.

Basic implementation:
```cs
    public class MySchemaValidationLambda: SchemaValidationLambda
    {
        public abstract List<Error> Validate(JObject input, string collection)
       {
          //check for data and return errors.
       }
    }

```

### Entity Validation
RawCMS already ships a validator that analyzes schemas and reports errors. This is the "EntityValidation".

EntityValidation reads json settings in _schema collection and validate data.

This lets you manage most common validation issues (field required, format validation, lenght, regexp) without writing code, and just with the configuration.
