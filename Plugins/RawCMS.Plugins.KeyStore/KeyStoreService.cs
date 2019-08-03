using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.KeyStore
{
    public class KeyStoreService
    {
        Dictionary<string, object> db = new Dictionary<string, object>();

        public object Get(string key)
        {
            return db[key];
        }


        public void Set(string key, object value)
        {
            db[key] = value;
        }
    }
}
