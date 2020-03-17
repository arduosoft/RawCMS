using System;
using System.Collections.Generic;
using System.Text;
using RawCMS.Library.Core;
using RawCMS.Library.UI;
using System.Linq;

namespace RawCMS.Library.Service
{
    public class UIService
    {
        protected AppEngine appEngine;

        public List<UIResourceRequirement> Requirements = new List<UIResourceRequirement>();

       

        public List<UIMetadata> Modules = new List<UIMetadata>();


        public UIService(AppEngine appEngine)
        {
            this.appEngine = appEngine;
            foreach (var plugin in this.appEngine.Plugins)
            {
                var increment=plugin.GetUIMetadata();
                if (increment != null)
                {
                    increment.ModuleUrl = GetPluginPathBase(plugin.Slug); //To be moved
                    increment.ModuleName = plugin.Slug;
                    Requirements.AddRange(increment.Requirements);
                    
                    Modules.Add(increment);
                }
            }
            
        }
        public List<UIMetadata> GetModules()
        {
            return Modules;
        }


            //public List<String> GetPluginsInit()
            //{
            //    var metadataInfo = this.appEngine.Plugins.OrderBy(x => x.Priority).Select(x =>
            //             x.GetUIMetadata()
            //        ).ToList();

            //    List<string> result = new List<string>();
            //    metadataInfo.ForEach(x => {
            //        x.
            //    })
            //}
            public List<String> GetPluginsPaths()
        {
            var slugs = this.appEngine.Plugins.OrderBy(x => x.Priority).Select(x =>
                     x.Slug
                ).ToList();

            List<string> result = new List<string>();
            slugs.ForEach(x => {
                result.Add(GetPluginPathBase(x) );
            });
            return result;
        }


        private const string PluginPathBaseTemplate= "/app/modules/{0}/";

        private string GetPluginPathBase(string x)
        {
            return string.Format(PluginPathBaseTemplate, x);
        }

        public string GetJavascriptHtml()
        {
            var requirements = Requirements.Where(x => x.Type == UIResourceRequirementType.Javascript).ToList();
            StringBuilder sb = new StringBuilder();
            RequirementsToHtml("script", "src", requirements, sb);
            return sb.ToString();
        }

        public string GetCSStHtml()
        {
            
            var requirements = Requirements.Where(x => x.Type == UIResourceRequirementType.CSS).ToList();
            StringBuilder sb = new StringBuilder();
            RequirementsToHtml("link", "href", requirements, sb);
            return sb.ToString();
        }

        private void RequirementsToHtml(string tag, string urlAttribute, List<UIResourceRequirement> requirements, StringBuilder stringBuilder)
        {

            foreach (var iem in requirements)
            {

                stringBuilder.AppendLine();

                stringBuilder.Append("<");
                stringBuilder.Append(tag);
                stringBuilder.Append(" ");

                //TODO: add here attributes

                stringBuilder.Append(urlAttribute);
                stringBuilder.Append("=");
                stringBuilder.Append("'");
                stringBuilder.Append(iem.ResourceUrl);
                stringBuilder.Append("'");


                if (iem.Type == UIResourceRequirementType.CSS)
                {

                    stringBuilder.Append(" rel='stylesheet' ");
                    stringBuilder.Append("/>");
                }
                else
                {

                    stringBuilder.Append(">");

                    stringBuilder.Append("</");
                    stringBuilder.Append(tag);
                    stringBuilder.Append(">");
                }
            }
        }
    }
}

