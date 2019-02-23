using GraphQL;
using GraphQL.Types;
using RawCMS.Library.Schema;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.GraphQL.Types
{
    public class CollectionType : ObjectGraphType<object>
    {
        public QueryArguments TableArgs
        {
            get; set;
        }

        private IDictionary<FieldBaseType, Type> _fieldTypeToSystemType;
        protected IDictionary<FieldBaseType, Type> FieldTypeToSystemType
        {
            get
            {
                if (_fieldTypeToSystemType == null)
                {
                    _fieldTypeToSystemType = new Dictionary<FieldBaseType, Type>();
                    _fieldTypeToSystemType.Add(FieldBaseType.Boolean, typeof(bool));
                    _fieldTypeToSystemType.Add(FieldBaseType.Date, typeof(DateTime));
                    _fieldTypeToSystemType.Add(FieldBaseType.Float, typeof(float));
                    _fieldTypeToSystemType.Add(FieldBaseType.ID, typeof(Guid));
                    _fieldTypeToSystemType.Add(FieldBaseType.Int, typeof(int));
                    _fieldTypeToSystemType.Add(FieldBaseType.String, typeof(string));
                }

                return _fieldTypeToSystemType;
            }
        }

        private Type ResolveFieldMetaType(FieldBaseType type)
        {
            if (FieldTypeToSystemType.ContainsKey(type))
                return FieldTypeToSystemType[type];
            return typeof(string);
        }

        public CollectionType(CollectionSchema collectionSchema)
        {
            Name = collectionSchema.CollectionName;
            foreach (var field in collectionSchema.FieldSettings)
            {
                InitGraphField(field);
            }
        }
        private void InitGraphField(Field field)
        {
            var graphQLType = (ResolveFieldMetaType(field.BaseType)).GetGraphTypeFromType(!field.Required);
            var columnField = Field(
                graphQLType,
                field.Name
            );

            columnField.Resolver = new NameFieldResolver();
            FillArgs(field.Name, graphQLType);
        }
        private void FillArgs(string name, Type graphType)
        {
            if (TableArgs == null)
            {
                TableArgs = new QueryArguments(
                    new QueryArgument(graphType)
                    {
                        Name = name
                    }
                );
            }
            else
            {
                TableArgs.Add(new QueryArgument(graphType) { Name = name });
            }

            TableArgs.Add(new QueryArgument<IntGraphType> { Name = "pageNumber" });
            TableArgs.Add(new QueryArgument<IntGraphType> { Name = "pageSize" });
        }
    }
}
