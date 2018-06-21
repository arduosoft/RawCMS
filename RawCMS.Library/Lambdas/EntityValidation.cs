using RawCMS.Library.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Service;
using Newtonsoft.Json;
using RawCMS.Library.Schema;
using RawCMS.Library.Core.Interfaces;
using System.Linq;

namespace RawCMS.Library.Lambdas
{
    public class EntityValidation : SchemaValidationLambda, IRequireCrudService, IInitable,IRequireApp
    {
        public override string Name => "Entity Validation";

        public override string Description => "Provide generic entity validation, based on configuration";

        private static Dictionary<string, CollectionSchema> entities = new Dictionary<string, CollectionSchema>();
        private static List<FieldTypeValidator> typeValidators = new List<FieldTypeValidator>();


        private CRUDService service;

        public EntityValidation()
        {
           

        }

        public void Init()
        {
            InitSchema();
            InitValidators();

        }

        private void InitValidators()
        {
            typeValidators=this.manager.GetAssignablesInstances<FieldTypeValidator>();
            
        }

        private void InitSchema()
        {
            var dbEntities = service.Query("_schema", new DataModel.DataQuery()
            {
                PageNumber = 1,
                PageSize = int.MaxValue,
                RawQuery = null

            }).Items;

            foreach (var item in dbEntities)
            {

                var schema = item.ToObject<CollectionSchema>();
                if (schema.CollectionName != null && !string.IsNullOrEmpty(schema.CollectionName.ToString()))
                {
                    entities[schema.CollectionName] = schema;
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

                if (!settings.AllowNonMappedFields)
                {

                    
                    foreach (var field in input.Properties())
                    {
                        if (!settings.FieldSettings.Any(x => x.Name==field.Name))
                        {
                            errors.Add(new Error()
                            {
                                Code="Forbidden Field",
                                Title= $"Field {field.Name} not in allowed field list",
                            });
                        }
                    }
                }

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

            var typeValidator = typeValidators.FirstOrDefault(x => x.Type == field.Type);
            if (typeValidator!=null)
            {
                errors.AddRange(typeValidator.Validate(input, field));
            }
            return errors;
        }

        public void SetCRUDService(CRUDService service)
        {
            this.service = service;
        }

        AppEngine manager;
        public void setLambdaManager(AppEngine manager)
        {
            this.manager = manager;
        }
    }
}