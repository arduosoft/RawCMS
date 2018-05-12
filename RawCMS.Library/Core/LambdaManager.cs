using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace RawCMS.Library.Core
{
    public class LambdaManager
    {
        #region singleton
        private static LambdaManager _current = null;
        private static ILogger logger;
        public static void SetLogger(ILoggerFactory factory)
        {
            logger = factory.CreateLogger(typeof(LambdaManager));
        }
        public static LambdaManager Current
        {
            get
            {
                return _current ?? (_current= new LambdaManager() );
            }
        }
        #endregion


        public List<Lambda> Lambdas { get; set; } = new List<Lambda>();


        public Lambda this[string name]
        {
            get
            {
                return Lambdas.FirstOrDefault(x => x.Name == name);
            }
        }

      
        public LambdaManager()
        {
           
            LoadLambdas();
        }


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
               logger.LogInformation("loading from" + assembly.FullName);
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if ( typeof(Lambda).IsAssignableFrom(type) && ! type.IsAbstract && ! type.IsInterface)
                    {
                        var instance = Activator.CreateInstance(type) as Lambda;

                        if (instance != null)
                        {
                            Lambdas.Add(instance);
                            logger.LogInformation("-" + type.Name + " | " + type.GetType().FullName);
                        }
                        else
                        {
                            logger.LogWarning("- (unable to create an instance, skipped) - " + type.Name + " | " + type.GetType().FullName);
                        }
                    }
                }
            }
        }
    }
}
