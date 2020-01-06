using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Enum;
using RawCMS.Library.Schema;
using System.Collections.Generic;
using System.Linq;

namespace RawCMS.Library.Service
{
    public class EntityService
    {
        protected readonly CRUDService crudService;
        protected readonly AppEngine appEngine;

        public EntityService(CRUDService crudService, AppEngine appEngine)
        {
            this.crudService = crudService;
            this.appEngine = appEngine;
            InitFields();
            InitSchema();
        }

        private static Dictionary<string, CollectionSchema> entities = new Dictionary<string, CollectionSchema>();
        private static List<FieldTypeValidator> typeValidators = new List<FieldTypeValidator>();
        private static List<FieldType> types = new List<FieldType>();

        private void InitFields()
        {
            typeValidators = appEngine.GetFieldTypeValidators();
            types = appEngine.GetFieldTypes();
        }

        public List<FieldType> GetTypes()
        {
            return types;
        }

        public List<FieldTypeValidator> GetTypeValidators()
        {
            return typeValidators;
        }

        private void InitSchema()
        {
            JArray dbEntities = crudService.Query("_schema", new DataModel.DataQuery()
            {
                PageNumber = 1,
                PageSize = int.MaxValue,
                RawQuery = null
            }).Items;

            foreach (JToken item in dbEntities)
            {
                CollectionSchema schema = item.ToObject<CollectionSchema>();
                if (schema.CollectionName != null && !string.IsNullOrEmpty(schema.CollectionName.ToString()))
                {
                    schema = AddIdSchemaField(schema);
                    entities[schema.CollectionName] = schema;
                }
            }
        }

        private CollectionSchema AddIdSchemaField(CollectionSchema schema)
        {
            bool haveIdField = schema.FieldSettings.Where(x => x.Name.Equals("_id")).Count() > 0;
            if (!haveIdField)
            {
                Field field = new Field
                {
                    Name = "_id",
                    //BaseType = FieldBaseType.String,
                    Type = "ObjectId",
                    Required = true
                };
                schema.FieldSettings.Add(field);
            }

            return schema;
        }

        public List<CollectionSchema> GetCollectionSchemas()
        {
            return entities.Values.ToList();
        }

        public CollectionSchema GetByName(string collectionName)
        {
            if (entities.TryGetValue(collectionName, out CollectionSchema value))
            {
                return value;
            }

            return null;
        }

        public List<FieldTypeValidator> GetTypeValidator(string type)
        {
            return typeValidators.Where(x => x.Type == type).ToList();
        }

        internal void AddOrReplaceEntity(string entityName, CollectionSchema schema, DataOperation operation)
        {
            var clone = new Dictionary<string, CollectionSchema>(entities);
            if (clone.ContainsKey(entityName))
            {
                clone.Remove(entityName);
            }

            if (operation == DataOperation.Write)
            {
                schema = AddIdSchemaField(schema);
                clone.Add(entityName, schema);
            }
            entities = clone;
        }
    }
}