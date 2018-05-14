using RawCMS.Library.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Service;
using Newtonsoft.Json;
using RawCMS.Library.Schema;

namespace RawCMS.Library.Lambdas
{
    public class EntityValidation : SchemaValidationLambda
    {
        public override string Name => "Entity Validation";

        public override string Description => "Provide generic entity validation, based on configuration";

        private static Dictionary<string, CollectionSchema> entities = new Dictionary<string, CollectionSchema>();

        public EntityValidation(CRUDService service)
        {
            var dbEntities = service.Query("_schema", null).Items;

            foreach (var item in dbEntities)
            {
                if (item["name"] != null && !string.IsNullOrEmpty(item["name"].ToString()))
                {
                    entities[item["name"].ToString()] = item.ToObject<CollectionSchema>();
                }
            }

        }

        public override List<Error> Validate(JObject input, string collection)
        {
            CollectionSchema settings;
            List<Error> errors = new List<Error>();
            if (entities.TryGetValue(collection, out settings))
            {
                //do validation!

                foreach (var field in settings.FieldSettings)
                {
                    errors.AddRange(ValidateField(field, input, collection));
                }

            }

            return errors;
        }

        private IEnumerable<Error> ValidateField(Field field, JObject input, string collection)
        {
            List<Error> errors = new List<Error>();

            if (field.Required && input[field.Name] == null)
            {
                errors.Add(new Error()
                {
                    Code = "REQUIRED",
                    Title = "Field " + field.Name + " is required"
                });
            }


            if (field.Type == FieldType.text)
            {
                if (field.Options != null)
                {
                    if (field.Options["maxlength"] != null)
                    {
                        int maxlenght;
                        if (int.TryParse(field.Options["maxlength"].ToString(), out maxlenght))
                        {
                            if (input[field.Name] != null && maxlenght < input[field.Name].ToString().Length)
                            {
                                errors.Add(new Error()
                                {
                                    Code = "REQUIRED",
                                    Title = "Field " + field.Name + " is required"
                                });
                            }
                        }
                    }

                }
            }
            return errors;
        }
    }
}