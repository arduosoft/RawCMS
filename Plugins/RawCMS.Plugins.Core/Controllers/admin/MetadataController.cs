using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawCMS.Library.Core.Attributes;
using RawCMS.Library.Schema;
using RawCMS.Library.Schema.Validation;
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

        public class FieldClientValidation
        {
            public string Name { get; set; }
            public string Function { get; set; }
        }

        public class FieldInfo
        {
            public FieldType Type { get; set; }
            public List<FieldClientValidation> Validations { get; set; }
        }

        [HttpGet("fieldinfo")]
        public List<FieldInfo> GetFieldTypes()
        {
            List<FieldType> types = entityService.GetTypes();
            List<FieldInfo> result = new List<FieldInfo>();
            foreach (FieldType type in types)
            {
                FieldInfo field = new FieldInfo
                {
                    Type = type,
                    Validations = new List<FieldClientValidation>()
                };

                List<FieldTypeValidator> validators = entityService.GetTypeValidator(type.TypeName);
                validators.ForEach(x =>
                {
                    if (x is BaseJavascriptValidator validator)
                    {
                        field.Validations.Add(new FieldClientValidation()
                        {
                            Function = validator.Javascript,
                            Name = validator.GetType().Name
                        });
                    }
                });
                result.Add(field);
            }
            return result;
        }
    }
}