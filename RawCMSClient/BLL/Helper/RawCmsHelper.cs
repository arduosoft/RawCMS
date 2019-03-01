using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RawCMSClient.BLL.Core;
using RawCMSClient.BLL.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace RawCMSClient.BLL.Helper
{
    public class RawCmsHelper
    {
        private static Runner log = LogProvider.Runner;
        private static string baseUrl = ClientConfig.GetValue<string>("BaseUrl");

        public static string GetData(ListRequest req)
        {
            var url = $"{baseUrl}/api/CRUD/{req.Collection}";
            log.Debug(url);

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



            return response.Content;
        }

     


        public static string CreateElement(CreateRequest req)
        {
            var url = $"{baseUrl}/api/CRUD/{req.Collection}";

            log.Debug(url);
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

            return response.Content;


        }

        public static void ProcessDirectory(bool recursive, Dictionary<string, string> fileList, string targetDirectory, string collection = null)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                fileList.Add( collection,fileName);
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
                return 0;
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
@"/***
 *        _     ______               _____                 _____ _ _            _        _    
 *     /\| |/\  | ___ \             /  __ \               /  __ \ (_)          | |    /\| |/\ 
 *     \ ` ' /  | |_/ /__ ___      _| /  \/_ __ ___  ___  | /  \/ |_  ___ _ __ | |_   \ ` ' / 
 *    |_     _| |    // _` \ \ /\ / / |   | '_ ` _ \/ __| | |   | | |/ _ \ '_ \| __| |_     _|
 *     / , . \  | |\ \ (_| |\ V  V /| \__/\ | | | | \__ \ | \__/\ | |  __/ | | | |_   / , . \ 
 *     \/|_|\/  \_| \_\__,_| \_/\_/  \____/_| |_| |_|___/  \____/_|_|\___|_| |_|\__|  \/|_|\/ 
 *                                                   _   _ _              _              _    
 *                                                  | | | (_)            | |            | |   
 *      ___ ___  _ __ ___  _ __ ___   __ _ _ __   __| | | |_ _ __   ___  | |_ ___   ___ | |   
 *     / __/ _ \| '_ ` _ \| '_ ` _ \ / _` | '_ \ / _` | | | | '_ \ / _ \ | __/ _ \ / _ \| |   
 *    | (_| (_) | | | | | | | | | | | (_| | | | | (_| | | | | | | |  __/ | || (_) | (_) | |   
 *     \___\___/|_| |_| |_|_| |_| |_|\__,_|_| |_|\__,_| |_|_|_| |_|\___|  \__\___/ \___/|_|   
 *                                                                                            
 *                                                                                            
 */"
            ,
@"
/***
 *               __________              _________                  _________ .__  .__               __              
 *      /\|\/\   \______   \____ __  _  _\_   ___ \  _____   ______ \_   ___ \|  | |__| ____   _____/  |_   /\|\/\   
 *     _)    (__  |       _|__  \\ \/ \/ /    \  \/ /     \ /  ___/ /    \  \/|  | |  |/ __ \ /    \   __\ _)    (__ 
 *     \_     _/  |    |   \/ __ \\     /\     \___|  Y Y  \\___ \  \     \___|  |_|  \  ___/|   |  \  |   \_     _/ 
 *       )    \   |____|_  (____  /\/\_/  \______  /__|_|  /____  >  \______  /____/__|\___  >___|  /__|     )    \  
 *       \/\|\/          \/     \/               \/      \/     \/          \/             \/     \/         \/\|\/  
 *                                                    .___ .__  .__                  __                .__           
 *      ____  ____   _____   _____ _____    ____    __| _/ |  | |__| ____   ____   _/  |_  ____   ____ |  |          
 *    _/ ___\/  _ \ /     \ /     \\__  \  /    \  / __ |  |  | |  |/    \_/ __ \  \   __\/  _ \ /  _ \|  |          
 *    \  \__(  <_> )  Y Y  \  Y Y  \/ __ \|   |  \/ /_/ |  |  |_|  |   |  \  ___/   |  | (  <_> |  <_> )  |__        
 *     \___  >____/|__|_|  /__|_|  (____  /___|  /\____ |  |____/__|___|  /\___  >  |__|  \____/ \____/|____/        
 *         \/            \/      \/     \/     \/      \/               \/     \/                                    
 */
"
            
        };

      
        #endregion
    }
}
