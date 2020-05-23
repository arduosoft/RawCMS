using RawCMS.Library.UI;
using System.Collections.Generic;

namespace RawCMS.Plugins.Core.Model
{
    public class UIResult
    {
        public Api api { get; set; }
        public Login login { get; set; }

        public List<UIMetadata> metadata { get; set; }
    }

    public class Api
    {
        public string baseUrl { get; set; }
    }

    public class Login
    {
        public string grant_type { get; set; }
        public string scope { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
    }
}