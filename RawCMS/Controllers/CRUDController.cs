using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RawCMS.Library.Service;
using RawCMS.Library.DataModel;
using Newtonsoft.Json.Linq;

namespace RawCMS.Controllers
{
    [Route("api/[controller]")]
    public class CRUDController : Controller
    {
        private readonly CRUDService service;
        public CRUDController(CRUDService service)
        {
            this.service = service;
        }
        // GET api/values
        [HttpGet("{collection}")]
        public ItemList Get(string collection, string rawQuery = null, int pageNumber = 1, int pageSize = 20)
        {
           // CRUDService service = new CRUDService(new MongoService(new MongoSettings() { }));
            return service.Query(collection, new Library.DataModel.DataQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                RawQuery = rawQuery
            });
            //return null;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost("{collection}")]
        public void Post(string collection,[FromBody]JObject value)
        {
            service.Insert(collection, value);
        }

        // PUT api/values/5
        [HttpPut("{collection}/{id}")]
        public void Put(string collection, string id, [FromBody]JObject value)
        {
            value["_id"] = id;
            service.Update(collection, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
