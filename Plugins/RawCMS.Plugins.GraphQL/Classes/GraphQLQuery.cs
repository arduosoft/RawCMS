using GraphQL.Types;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using RawCMS.Library.Service.Contracts;
using RawCMS.Plugins.GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.GraphQL.Classes
{
    public class GraphQLQuery : ObjectGraphType<JObject>
    {
        public GraphQLQuery(ICollectionMetadata collectionMetadata, GraphQLService graphQLService)
        {
            Name = "Query";
            foreach (var metaColl in collectionMetadata.GetCollectionSchemas())
            {
                var type = new CollectionType(metaColl);
                var listType = new ListGraphType(type);
                AddField(new FieldType
                {
                    Name = metaColl.CollectionName,
                    Type = listType.GetType(),
                    ResolvedType = listType,
                    Resolver = new JObjectFieldResolver(graphQLService),
                    Arguments = new QueryArguments(
                        type.TableArgs
                    )
                });


            }
        }
    }
}
