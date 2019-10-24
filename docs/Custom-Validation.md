SchemaValidationLambda is the base type to hook validation. In save pipeline validation is triggered so all derived class will be used to manage data validation.

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
RawCMS ship already a validator that analyze schema and report errors. This is the "EntityValidation".

EntityValidation read json settings in _schema collection and validate data.

This let you manage most common validation issues (field required, format validation, lenght, regexp) without writing code, just configuration.
