namespace RawCMS.Plugins.Core.Configuration
{
    public class UIPluginConfig
    {
        public string Template { get; set; }

        public string GetFolder(string plugin, string filename)
        {
            return string.Format(this.Template, plugin, filename);
        }
    }
}