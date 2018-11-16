namespace RawCMS.Library.Core.Extension
{
    public class PluginManager
    {
        private static PluginManager pm;
        internal static PluginManager Current => pm ?? (pm = new PluginManager());

        public PluginManager()
        {
        }
    }
}