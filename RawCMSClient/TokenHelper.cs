using RestSharp;
using System;
using System.IO;

namespace RawCMSClient
{
    public   class TokenHelper
    {
        public static string baseUrl = "http://localhost:49439";



        public static string getToken(string username, string password, string clientId, string clientSecret)
        {
            
            var url = $"{baseUrl}/connect/token";

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

            if (response.IsSuccessful)
            {
                TokenResponse res = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse>(response.Content);
                return res.access_token;
            }
            else
            {
               // Logger.LogWarning("unable to get valid token.");
            }
            return "";
        }

        public static void saveTokenToFile(string token)
        {

            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string filePath = System.IO.Path.Combine(mydocpath, "RawCMSToken.token");

            //File.SetAttributes(filePath, FileAttributes.Hidden);

            try
            {
                using (StreamWriter outputFile = new StreamWriter(filePath))
                {
                    outputFile.Write(token);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be writed:");
                Console.WriteLine(e.Message);
            }



        }

        public static string getTokenFromFile()
        {
            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string filePath = System.IO.Path.Combine(mydocpath, "RawCMSToken.token");

            string token = string.Empty;
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Read the stream to a string, and write the string to the console.
                    token = sr.ReadToEnd();
                    Console.WriteLine(token);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            Console.WriteLine(token);
            return token;
        }
    }
}
