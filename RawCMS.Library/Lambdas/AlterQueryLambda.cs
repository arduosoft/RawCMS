using MongoDB.Bson;
using MongoDB.Driver;
using RawCMS.Library.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Lambdas
{
    public abstract class AlterQueryLambda : Lambda
    {

        public abstract void Alter(string collection, FilterDefinition<BsonDocument> query);
    }

    public abstract class CollectionAlterQueryLambda : Lambda
    {
        public abstract string Collection { get; set; }
        public abstract void Alter(FilterDefinition<BsonDocument> query);
    }
}
