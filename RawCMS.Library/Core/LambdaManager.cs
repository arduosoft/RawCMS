using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Service;
using RawCMS.Library.Core.Interfaces;

namespace RawCMS.Library.Core
{
    public class LambdaManager
    {
        //#region singleton
        //private static LambdaManager _current = null;
        private static ILogger _logger;
        //public static void SetLogger(ILoggerFactory factory)
        //{
        //    logger = factory.CreateLogger(typeof(LambdaManager));
        //}
        //public static LambdaManager Current
        //{
        //    get
        //    {
        //        return _current ?? (_current= new LambdaManager() );
        //    }
        //}
        //#endregion


        public List<Lambda> Lambdas { get; set; } = new List<Lambda>();

        public CRUDService Service { get { return service; } }

        public Lambda this[string name]
        {
            get
            {
                return Lambdas.FirstOrDefault(x => x.Name == name);
            }
        }

      
        
        public LambdaManager(ILoggerFactory loggerFactory,CRUDService service)
        {
            _logger = loggerFactory.CreateLogger(typeof(LambdaManager));
            this.service = service;
            this.service.setLambdaManager(this);//TODO: fix this circular dependemcy
            LoadLambdas();
        }

        private readonly CRUDService service;
        private void LoadLambdas()
        {
           
            DiscoverLambdasInBundle();
        }

        /// <summary>
        /// Find and load all lambas already loaded with main bundle (no dinamycs)
        /// </summary>
        private void DiscoverLambdasInBundle()
        {
            var bundledAssemblies=AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name.StartsWith("RawCMS")).ToList();
            foreach (var assembly in bundledAssemblies)
            {
               _logger.LogInformation("loading from" + assembly.FullName);
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    try
                    {
                        if (typeof(Lambda).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
                        {

                            var instance = Activator.CreateInstance(type) as Lambda;

                            if (instance != null)
                            {
                                if (instance is IRequireLambdas)
                                {
                                    ((IRequireLambdas)instance).setLambdaManager(this);
                                }

                                if (instance is IRequireCrudService)
                                {
                                    ((IRequireCrudService)instance).SetCRUDService(this.Service);
                                }

                                if (instance is IInitable)
                                {

                                    ((IInitable)instance).Init();
                                }

                                Lambdas.Add(instance);

                                _logger.LogInformation("-" + type.Name + " | " + type.GetType().FullName);
                            }
                            else
                            {
                                _logger.LogWarning("- (unable to create an instance, skipped) - " + type.Name + " | " + type.GetType().FullName);
                            }

                        }
                    }
                    catch (Exception err)
                    {
                        _logger.LogError(err, "- (unable to create an instance for EXCEPTION skipped) - " + type.Name + " | " + type.GetType().FullName);
                    }
                    
                }
            }
        }
    }
}
