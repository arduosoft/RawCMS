using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace RawCMS.Library.Core
{
    public abstract class RestLambda : HttpLambda
    {
        public HttpContext Request;

        public override object Execute(HttpContext request)
        {
            Request = request;
            using (StreamReader reader = new StreamReader(request.Request.Body))
            {
                string body = reader.ReadToEnd();
                JObject input = new JObject();
                try
                {
                    input = JObject.Parse(body);
                }
                catch (Exception)
                {
                    //TODO: add log here
                }

                return Rest(input);
            }
        }

        public abstract JObject Rest(JObject input);
    }
}