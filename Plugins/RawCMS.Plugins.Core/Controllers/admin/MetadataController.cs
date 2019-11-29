using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawCMS.Library.Core.Attributes;
using RawCMS.Library.Schema;
using RawCMS.Library.Service;
using System.Collections.Generic;

namespace RawCMS.Plugins.Core.Controllers.admin
{
    [AllowAnonymous]
    [RawAuthentication]
    [Route("system/[controller]")]
    public class MetadataController
    {
        protected EntityService entityService;

        public MetadataController(EntityService entityService)
        {
            this.entityService = entityService;
        }

        [HttpGet()]
        public List<FieldType> GetFieldTypes()
        {
            List<FieldType> types = entityService.GetTypes();
            return types;
        }
    }
}