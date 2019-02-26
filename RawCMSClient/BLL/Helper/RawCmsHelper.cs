using Newtonsoft.Json;
using RawCMSClient.BLL.Core;
using RawCMSClient.BLL.Log;
using RawCMSClient.BLL.Model;
using RestSharp;
using System;

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

        private static string ParseDataFile(string dataFile)
        {
            throw new NotImplementedException();
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
