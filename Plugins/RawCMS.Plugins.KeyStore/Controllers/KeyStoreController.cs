using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RawCMS.Library.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.KeyStore.Controllers
{
    [AllowAnonymous]
    [RawAuthentication]
    [Route("api/[controller]")]
    [ParameterValidator("collection", "_(.*)", true)]
    public class KeyStoreController : Controller
    {
        KeyStoreService service;

        public KeyStoreController(KeyStoreService service)
        {
            this.service = service;
        }

        // GET api/CRUD/{collection}
        [HttpHead("{key}")]
        public IActionResult Get(string key)
        {
            var content = new OkResult();
            var result = new StringValues(new string[] { this.service.Get(key) as string });
            Response.Headers.Add("r", result);
            return content;
        }
    }
}
