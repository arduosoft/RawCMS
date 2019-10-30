using Jint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Attributes;
using RawCMS.Library.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.Core.Controllers
{
    [AllowAnonymous]
    [RawAuthentication]
    [Route("api/js")]
    public class JsLambdaController
    {

        private readonly AppEngine lambdaManager;
        private readonly CRUDService crudService;

        public JsLambdaController(AppEngine lambdaManager, CRUDService crudService)
        {
            this.lambdaManager = lambdaManager;
            this.crudService = crudService;
        }

       

        [AllowAnonymous]
        [RawAuthentication]
        [HttpPost("{lambda}")]
        public JObject Post(string lambda,[FromBody] JObject input)
        {
            var result = this.crudService.Query("_js", new Library.DataModel.DataQuery()
            {
                PageNumber = 1,
                PageSize = 1,
                RawQuery = $"{{\"Path\":\"{lambda}\"}}"
            });

            var js = result.Items[0];
            var code = js["Code"].ToString();

            Dictionary<string, object> tmpIn = input.ToObject<Dictionary<string, object>>();
            Dictionary<string, object> tmpOur = new Dictionary<string, object>();
            Engine add = new Engine()
                     .SetValue("input", tmpIn)
                     .SetValue("output", tmpOur)
                    .Execute(code);
            return JObject.FromObject(tmpOur);
        }

    }
}
