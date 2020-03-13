using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.UI
{

    public enum UIResourceRequirementType
    {
        Javascript,
        CSS
    }

    public class UIResourceRequirement
    {
        public UIResourceRequirementType Type { get; set; }
        public string ResourceUrl { get; set; }

        public Dictionary<string, string> CustomAttributes { get; set; } = new Dictionary<string, string>();

        public int Order { get; set; }

    }

    public class  UIMenuItem
    {
        public string IconUrl { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
    }
    public class UIMetadata
    {
        public List<UIResourceRequirement> Requirements = new List<UIResourceRequirement>();

        public List<UIMenuItem> MenuItems = new List<UIMenuItem>();  
        
        public string ModuleUrl { get; set; }

    }
}
