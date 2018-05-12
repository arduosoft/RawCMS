using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RawCMS.Library.Service;
using RawCMS.Library.DataModel;
using Newtonsoft.Json.Linq;
using RawCMS.Model;
using RawCMS.Library.Core.Exceptions;

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
        // GET api/CRUD/{collection}
        [HttpGet("{collection}")]
        public RestMessage<ItemList>  Get(string collection, string rawQuery = null, int pageNumber = 1, int pageSize = 20)
        {
           // CRUDService service = new CRUDService(new MongoService(new MongoSettings() { }));
            var result= service.Query(collection, new Library.DataModel.DataQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                RawQuery = rawQuery
            });

            return new RestMessage<ItemList>  (result);
        }

        // GET api/CRUD/{collection}/5
        [HttpGet("{collection}/{id}")]
        public RestMessage<JObject>  Get(string collection, string id)
        {
            var data = service.Get(collection, id);
            return new RestMessage<JObject>(data);
        }

        // POST api/CRUD/{collection}
        [HttpPost("{collection}")]
        public RestMessage<bool> Post(string collection,[FromBody]JObject value)
        {
            var response = new RestMessage<bool>(false);
            try
            {
                service.Insert(collection, value);
                response.Data = true;
                return response;
            }
            catch (ValidationException err)
            {
                response.Errors = err.Errors;
            }
            catch (Exception untrapped)
            {
                //TODO: log here
                response.Errors.Add(new Library.Core.Error()
                {
                    Code = "UNEXPEXTED",
                    Title = $"{collection} produces an unexpexted error",
                    Description = untrapped.Message,
                });
            }
            return response;
        }

        // PUT api/CRUD/{collection}/5
        [HttpPut("{collection}/{id}")]
        public RestMessage<bool> Put(string collection, string id, [FromBody]JObject value)
        {
            var response = new RestMessage<bool>(false);
            try
            {

                value["_id"] = id;
                service.Update(collection, value);
                response.Data = true;
                return response;
            }
            catch (ValidationException err)
            {
                response.Errors = err.Errors;
            }
            catch (Exception untrapped)
            {
                //TODO: log here
                response.Errors.Add(new Library.Core.Error()
                {
                    Code = "UNEXPEXTED",
                    Title = $"{collection} produces an unexpexted error",
                    Description = untrapped.Message,
                });
            }
            return response;
        }

        // DELETE api/CRUD/{collection}/5
        [HttpDelete("{collection}/{id}")]
        public RestMessage<bool> Delete(string collection, string id)
        {
            var response = new RestMessage<bool>(false);
            try
            {
                var result= service.Delete(collection, id);
                response.Data = true;
                return response;
            }
            catch (ValidationException err)
            {
                response.Errors = err.Errors;
            }
            catch (Exception untrapped)
            {
                //TODO: log here
                response.Errors.Add(new Library.Core.Error()
                {
                    Code = "UNEXPEXTED",
                    Title = $"{collection} produces an unexpexted error",
                    Description = untrapped.Message,
                });
            }
            return response;
        }
    }
}
