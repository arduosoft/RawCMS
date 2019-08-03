using System;
using System.Collections.Generic;
using System.Text;
using RawCMS.Plugins.KeyStore.Model;

namespace RawCMS.Plugins.KeyStore
{
    public class KeyStoreService
    {
       static Dictionary<string, object> db = new Dictionary<string, object>();

        public object Get(string key)
        {
            return db[key];
        }


       

        internal void Set(KeyStoreInsertModel insert)
        {
            db[insert.Key] = insert.Value;
        }
    }
}
