using RawCMSClient.BLL.Core;
using RawCMSClient.BLL.Model;
using RestSharp;
using System;
using System.IO;

namespace RawCMSClient.BLL.Helper
{
    public   class TokenHelper
    {

        private static Runner log = LogProvider.Runner;

        public static string getToken(string username, string password, string clientId, string clientSecret)
        {

            string baseUrl = ClientConfig.GetValue<string>("BaseUrl");
            var url = $"{baseUrl}/connect/token";

            log.Debug(url);

            //create RestSharp client and POST request object
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            //add GetToken() API method parameters
            request.Parameters.Clear();
            request.AddParameter("grant_type", "password");

            request.AddParameter("username", username);
            request.AddParameter("password", password);

            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("scoope", "openid");


            //make the API request and get the response
            IRestResponse response = client.Execute(request);
            TokenResponse res = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse>(response.Content);
            if (response.IsSuccessful)
            {
                log.Debug("success response token");          
                return res.access_token;
            }
            else
            {

               log.Warn("unable to get valid token.");
                throw new ExceptionToken(res.error, res.error_description);
            }
            
        }

        public static string SaveTokenToFile(string filePath,string token)
        {
            log.Debug("save token to file");

            

            log.Debug($"filePath: {filePath}");

            
            try
            {
                using (StreamWriter outputFile = new StreamWriter(filePath))
                {                  
                    outputFile.Write(token);
                }
            }
            catch (Exception e)
            {
                log.Error("The file could not be writed:", e );
                
            }

            return filePath;



        }

        public static string getTokenFromFile()
        {
            string token = string.Empty;
            log.Debug("get token from file...");

            string filePath = Environment.GetEnvironmentVariable("RAWCMSCONFIG",EnvironmentVariableTarget.Process);
            log.Debug($"config file: {filePath}");

            /// ***** DEBUG DA VISUALSTUDIO *****
            /// cambiare col path relativo all'ambiente
            filePath = @"C:\Users\fmina\Documents\RawCMS.config";
            ///

            if (string.IsNullOrEmpty(filePath))
            {
                log.Warn("config file not found. Perform login.");
                return "";
            }

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Read the stream to a string, and write the string to the console.
                    token = sr.ReadToEnd();
                    log.Debug($"token: {token}");
                }
            }
            catch (Exception e)
            {
                log.Error("The file could not be read:",e);
                
            }
            return token;
        }
    }
}
