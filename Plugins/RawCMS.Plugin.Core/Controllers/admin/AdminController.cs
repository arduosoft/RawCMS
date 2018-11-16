using Microsoft.AspNetCore.Mvc;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Attributes;

namespace RawCMS.Plugins.Core.Controllers.Controllers.admin
{
    [Route("system/[controller]")]
    [ParameterValidator("collection", "_(.*)", false)]
    public class AdminController : CRUDController
    {
        public AdminController(AppEngine manager) : base(manager)
        {
        }
    }
}