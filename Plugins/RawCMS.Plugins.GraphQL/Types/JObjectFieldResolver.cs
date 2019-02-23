using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RawCMS.Library.Core;
using RawCMS.Library.DataModel;
using RawCMS.Library.Service;
using RawCMS.Plugins.GraphQL.Classes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace RawCMS.Plugins.GraphQL.Types
{
    public class JObjectFieldResolver : IFieldResolver
    {

        private readonly GraphQLService _graphQLService;

        public JObjectFieldResolver(GraphQLService graphQLService)
        {
            _graphQLService = graphQLService;
        }

        public object Resolve(ResolveFieldContext context)
        {
            ItemList result;
            if (context.Arguments != null && context.Arguments.Count > 0)
            {
                var pageNumber = 1;
                var pageSize = 1000;
                if(context.Arguments.ContainsKey("pageNumber"))
                {
                    pageNumber = int.Parse(context.Arguments["pageNumber"].ToString());
                    if(pageNumber < 1)
                    {
                        pageNumber = 1;
                    }
                    context.Arguments.Remove("pageNumber");
                }

                if (context.Arguments.ContainsKey("pageSize"))
                {
                    pageSize = int.Parse(context.Arguments["pageSize"].ToString());
                    context.Arguments.Remove("pageSize");
                }

                result = _graphQLService.service.Query(context.FieldName.ToPascalCase(), new DataQuery()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    RawQuery = BuildMongoQuery(context.Arguments)
                });

            }
            else
            {
                result = _graphQLService.service.Query(context.FieldName.ToPascalCase(), new DataQuery()
                {
                    PageNumber = 1,
                    PageSize = 1000,
                    RawQuery = null
                });
            }

            return result.Items.ToObject<List<JObject>>();
        }

        private string BuildMongoQuery(Dictionary<string,object> arguments)
        {
            string query = null;
            if (arguments != null)
            {
                JsonSerializerSettings jSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };

                jSettings.ContractResolver = new DefaultContractResolver();
                var dictionary = new Dictionary<string, object>();
                foreach (var key in arguments.Keys)
                {
                    if (arguments[key] is string)
                    {
                        var reg = new JObject();
                        reg["$regex"] = $"/*{arguments[key]}/*";
                        reg["$options"] = "si";
                        dictionary[key.ToPascalCase()] = reg;
                    }else
                    {
                        dictionary[key.ToPascalCase()] = arguments[key];
                    }
                }
                query = JsonConvert.SerializeObject(dictionary, jSettings);
            }

            return query;
        }
    }
}
