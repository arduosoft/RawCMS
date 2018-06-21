using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Attributes;

namespace RawCMS.Controllers.admin
{
    [Route("system/[controller]")]
    [ParameterValidator("collection","_(.*)",false)]
    public class AdminController : CRUDController
    {
        public AdminController(AppEngine manager) : base(manager)
        {
        }
    }
}