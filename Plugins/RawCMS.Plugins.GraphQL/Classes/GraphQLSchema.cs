using GraphQL;
using GraphQL.Types;
using RawCMS.Library.Core;
using RawCMS.Library.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using SchemaQL = GraphQL.Types.Schema;

namespace RawCMS.Plugins.GraphQL.Classes
{
    public class GraphQLSchema : SchemaQL
    {
        public GraphQLSchema(IDependencyResolver dependencyResolver, ICollectionMetadata collectionMetadata, GraphQLService manager) : base(dependencyResolver)
        {
            Query = new GraphQLQuery(collectionMetadata, manager);
        }
    }
}
