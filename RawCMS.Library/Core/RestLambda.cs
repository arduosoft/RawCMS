using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.IO;

namespace RawCMS.Library.Core
{
    public abstract class RestLambda : HttpLambda
    {
        public HttpContext Request;
        public override object Execute(HttpContext request)
        {
            this.Request = request;
            using (var reader = new StreamReader(request.Request.Body))
            {
                var body = reader.ReadToEnd();
                var input = new JObject();
                try
                {
                    input = JObject.Parse(body);
                }
                catch (Exception er)
                {
                    //TODO: add log here
                }

                return Rest(input);            
            }

           
        }

        public abstract JObject Rest(JObject input);
    }
}
