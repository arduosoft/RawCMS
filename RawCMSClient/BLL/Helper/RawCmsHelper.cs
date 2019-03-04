using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RawCMS.Client.BLL.Core;
using RawCMS.Client.BLL.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace RawCMS.Client.BLL.Helper
{
    public class RawCmsHelper
    {
        private static Runner log = LogProvider.Runner;
        private static string baseUrl = ClientConfig.GetValue<string>("BaseUrl");

        public static IRestResponse GetData(ListRequest req)
        {
            var url = $"{baseUrl}/api/CRUD/{req.Collection}";

            log.Debug($"Service url: {url}");

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            //request headers
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");


            //add parameters and token to request
            request.Parameters.Clear();
            request.AddParameter("rawQuery", req.RawQuery);
            request.AddParameter("pageNumber", req.PageNumber);
            request.AddParameter("pageSize", req.PageSize);

            request.AddParameter("Authorization", "Bearer " + req.Token, ParameterType.HttpHeader);

            //make the API request and get a response
            IRestResponse response = client.Execute(request);



            return response;
        }

     


        public static IRestResponse CreateElement(CreateRequest req)
        {
            var url = $"{baseUrl}/api/CRUD/{req.Collection}";

            log.Debug($"Server URL: {url}");
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            //request headers
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");

            //add parameters and token to request
            request.Parameters.Clear();
            request.AddParameter("application/json", req.Data, ParameterType.RequestBody);
            request.AddParameter("Authorization", "Bearer " + req.Token, ParameterType.HttpHeader);

            //make the API request and get a response
            IRestResponse response = client.Execute(request);
            

            return response;


        }

        public static void ProcessDirectory(bool recursive, Dictionary<string, List<string>> fileList, string targetDirectory, string collection = null)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                
                if (!fileList.ContainsKey(collection))
                {
                    fileList.Add(collection, new List<string>());
                }
                fileList[collection].Add(fileName);
                
            }
            
            if (recursive)
            {
                // Recurse into subdirectories of this directory.
                // this is the main level, so take care of name
                // of folder, thus is name of collection...

                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
                foreach (string subdirectory in subdirectoryEntries)
                {
                    ProcessDirectory(recursive,fileList, subdirectory, collection);
                }
            }
          

        }

        //public static void ProcessFile(Dictionary<string,string> fileList, string filePath,string collection)
        //{
        //    fileList.Add(collection, filePath);
        //}

        public static int CheckJSON(string filePath)
        {
            var content = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(content))
            {
                return 1;
            }

            try
            {
                var obj = JObject.Parse(content);
            }
            catch (JsonReaderException jex)
            {
                //Exception in parsing json
                log.Error(jex.Message);
                return 2;
            }
            catch (Exception ex) //some other exception
            {
                log.Error(ex.ToString());
                return 2;
            }
            return 0;

        }

        #region fun
        public static string Message { get {
                Random r = new Random();
                return messages[r.Next(messages.Length)];

            }
        }
        private static string[] messages = new string[] {
@"
MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM
MMMMMMMMMMMMWNMMMMMMMMMMMMMMMMMMMM ______               _____                 _____  _ _            _    M
MWNXWMMMMMMKc,dXMMMMMMMMMMMMMMMMMM | ___ \             /  __ \               /  __ \| (_)          | |   M 
MWXKXNMMMW0;...cKWMMMMMMMMMMMMMMMM | |_/ /__ ___      _| /  \/_ __ ___  ___  | /  \/| |_  ___ _ __ | |_  M
MWXKKXMMMNkoxd,.':o0WMMMMMMMWMWMWM |    // _` \ \ /\ / / |   | '_ ` _ \/ __| | |   || | |/ _ \ '_ \| __| M
MMMMMMMMXkONN0;    .lXMMMMMMKMKMKM | |\ \ (_| |\ V  V /| \__/\ | | | | \__ \ | \__/\| | |  __/ | | | |_  M
MMMMMMMK:.:kd,.   . .dXNWMMMKMKMKM \_| \_\__,_| \_/\_/  \____/_| |_| |_|___/  \____/|_|_|\___|_| |_|\__| M 
MMMMMMMO' ... .   . .ll,dXMMKMKMKM                                                _   _ _                M
MMMMMMM0' ......    ,o' .xWMKMKMKM                                               | | | (_)               M
MMMMMMMX: .;:;,.   .l;. .xWMKMKMKM   ___ ___  _ __ ___  _ __ ___   __ _ _ __   __| | | |_ _ __   ___     M
MMMMMMMWd. .dO, . .:;,' ,0WMKMKMKM  / __/ _ \| '_ ` _ \| '_ ` _ \ / _` | '_ \ / _` | | | | '_ \ / _ \    M
MMMMMMMMK; .dX: .,kl.;o'.lXMKMKMKM | (_| (_) | | | | | | | | | | | (_| | | | | (_| | | | | | | |  __/    M
MMMMMMMMWO,.c0c .cKd,cOc .xMKMKMKM  \___\___/|_| |_| |_|_| |_| |_|\__,_|_| |_|\__,_| |_|_|_| |_|\___|    M
MMMMMMMMMWk,;kd,,l0XXNXdcl0WKWKWKW                                                                       M
MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM
"
        };

      
        #endregion
    }
}
