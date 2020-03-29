using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using System.Linq;
using RawCMS.Library.Service;
using RawCMS.Plugins.Core.Model;
using RawCMS.Plugins.Core.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace RawCMS.Plugins.Core.Controllers
{
    [Route("api/[controller]")]
    public class UIMetadataController: Controller
    {
        private readonly AppEngine appEngine;
        private readonly UIService uiService;
        private readonly AuthConfig authConfig;
        public UIMetadataController(AppEngine appEngine, UIService uiService, AuthConfig authConfig)
        {
            this.appEngine = appEngine;
            this.uiService = uiService;
            this.authConfig = authConfig;
        }

        [HttpGet("paths")]
        public List<string> PluginPaths()
        {
            var moduleInits = uiService.GetPluginsPaths();
            return moduleInits;
        }

        [HttpGet]
        public UIResult Init()
        {
            var result = new UIResult();
            result.api = new Api();
            result.api.baseUrl = "";
            result.login = new Login();
            result.login.client_id = this.authConfig.RawCMSProvider.ClientId;
            result.login.client_secret = this.authConfig.RawCMSProvider.ClientSecret;
            result.login.grant_type = "password";
            result.login.scope = "openid";

            result.metadata = uiService.Modules;
            return result;
        }

    }
}
