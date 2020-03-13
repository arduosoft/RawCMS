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

        public List<UIMenuItem> MenuItems = new List<UIMenuItem>();

        public UIService(AppEngine appEngine)
        {
            this.appEngine = appEngine;
            foreach (var plugin in this.appEngine.Plugins)
            {
                var increment=plugin.GetUIMetadata();
                Requirements.AddRange(increment.Requirements);
                MenuItems.AddRange(increment.MenuItems);
            }
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

