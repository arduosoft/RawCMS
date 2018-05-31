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
    public class AppEngine
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

      
        
        public AppEngine(ILoggerFactory loggerFactory,CRUDService service)
        {
            _logger = loggerFactory.CreateLogger(typeof(AppEngine));
            this.service = service;
            this.service.setLambdaManager(this);//TODO: fix this circular dependemcy
            LoadLambdas();
        }

        private readonly CRUDService service;
        private void LoadLambdas()
        {
           
            DiscoverLambdasInBundle();
        }

        public T GetInstance<T>(params object[] args) where T:class
        {
            return Activator.CreateInstance(typeof(T), args) as T;
        }

        public T GetInstance<T>(Type type,params object[] args) where T : class
        {
            return Activator.CreateInstance(type, args) as T ;
        }

        public List<Type> GetAnnotatedBy<T>() where T : Attribute
        {
            List<Type> result = new List<Type>();
            List<Assembly> bundledAssemblies = GetAssemblyInScope<T>();
            foreach (var assembly in bundledAssemblies)
            {
                _logger.LogInformation("loading from" + assembly.FullName);
                var types = assembly.GetTypes();


                foreach (var type in types)
                {
                    if (type.GetCustomAttributes(typeof(T), true).Length > 0)
                    {
                        result.Add(type);
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// Get  instances of all classes assignable from T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAssignablesInstances<T>() where T : class
        {
            List<Type> types = GetImplementors<T>();
            return GetInstancesFromTypes<T>(types);
        }

        /// <summary>
        /// Get instanced of all classes annotated by T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAnnotatedInstances<T>() where T : class
        {
            List<Type> types = GetImplementors<T>();
            return GetInstancesFromTypes<T>(types);
        }

        /// <summary>
        /// Get all types that implements T or inherit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<Type> GetImplementors<T>() where T : class
        {
            List<Type> result = new List<Type>();
            List<Assembly> bundledAssemblies = GetAssemblyInScope<T>();
            foreach (var assembly in bundledAssemblies)
            {
                _logger.LogInformation("loading from" + assembly.FullName);
                var types = assembly.GetTypes();


                foreach (var type in types)
                {
                    try
                    {
                        if (typeof(T).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
                        {
                            result.Add(type);
                        }
                    }
                    catch (Exception err)
                    {
                        _logger.LogError(err, "- (unable to create an instance for EXCEPTION skipped) - " + type.Name + " | " + type.GetType().FullName);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Get all assemblies that may contains T instances
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<Assembly> GetAssemblyInScope<T>() where T : class
        {
            //TODO: use configuration to define assembly map or regexp to define where to lookup
            return AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name.StartsWith("RawCMS")).ToList();
        }

        /// <summary>
        /// give instances of a list of types
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="types"></param>
        /// <returns></returns>
        public List<T> GetInstancesFromTypes<T>(List<Type> types) where T : class
        {
            List<T> result = new List<T>();
            
            types.ForEach(x =>
            {
                result.Add(this.GetInstance<T>(x));
            });           

            return result;
        }
        /// <summary>
        /// Find and load all lambas already loaded with main bundle (no dinamycs)
        /// </summary>
        private void DiscoverLambdasInBundle()
        {

            List<Lambda> lambdas = GetAnnotatedInstances<Lambda>();

            foreach (Lambda instance in lambdas)
            {
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

                    _logger.LogInformation("-" + instance.Name + " | " + instance.GetType().FullName);
                }

            }
        }
                    
               
            
        
    }
}
