using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Client.BLL.Core
{
    public class ConfigFile
    {
        public string Token { get; set; }
        public string ServerUrl { get; set; }
        public string User { get; set; }
        public string CreatedTime { get; set; }

        public ConfigFile()
        {
            
        }

        public ConfigFile( string content)
        {

            try
            {
                ConfigFile cf = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigFile>(content);
                Token = cf.Token;
                ServerUrl = cf.ServerUrl;
                User = cf.User;
                CreatedTime = cf.CreatedTime;

            }
            catch(Exception e)
            {
                
            }
            
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

    }
    

}
