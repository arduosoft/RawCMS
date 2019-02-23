using Microsoft.Extensions.Logging;
using RawCMS.Library.Core;
using RawCMS.Library.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.GraphQL.Classes
{
    public class GraphQLService
    {
        private ILogger logger;
        public CRUDService service { get; private set; }
        private AppEngine appEngine;

        public GraphQLSettings Settings { get; private set; }

        public void SetAppEngine(AppEngine manager)
        {
            appEngine = manager;
        }

        public void SetCRUDService(CRUDService service)
        {
            this.service = service;
        }

        public void SetLogger(ILogger logger)
        {
            this.logger = logger;
        }

        public void SetSettings(GraphQLSettings settings)
        {
            this.Settings = settings;
        }
    }
}
