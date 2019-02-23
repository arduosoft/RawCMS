using GraphQL.Resolvers;
using GraphQL.Types;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.GraphQL.Types
{
    public class NameFieldResolver : IFieldResolver
    {
        public object Resolve(ResolveFieldContext context)
        {
            var source = context.Source;
            if (source == null)
            {
                return null;
            }
            var name = Char.ToUpperInvariant(context.FieldAst.Name[0]) + context.FieldAst.Name.Substring(1);
            var value = GetPropValue(source, name);
            if (value == null)
            {
                throw new InvalidOperationException($"Expected to find property {context.FieldAst.Name} on {context.Source.GetType().Name} but it does not exist.");
            }
            return value;
        }
        private static object GetPropValue(object src, string propName)
        {
            var source = src as JObject;
            source.TryGetValue(propName, StringComparison.InvariantCultureIgnoreCase, out JToken value);
            if (value != null)
            {
                return value.Value<object>();
            }
            else
            {
                return null;
            }
        }
    }
}
