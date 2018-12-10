using Xunit;
using RestSharp;
using Newtonsoft.Json;
using System;
using System.IO;

namespace RawCMS.Test
{
    

    public class TokenResponse
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }

    public class CRUDTest
    {
        string baseUrl = "http://localhost:49439";
        string clientId = "raw.client";
        string clientSecret = "raw.secret";
        string username = "bob";
        string password = "XYZ";

        [Fact]
        public void CRUD()
        {
        }

        /* tests */

        [Fact]
        public void GetTokenFromFileTest()
        {
            string token = getToken();
            Assert.False(string.IsNullOrEmpty(token));
                
            saveTokenToFile(token);
            string tokenfile = getTokenFromFile();
            Assert.True(tokenfile.Equals(token));

        }

        [Fact]
        public void TestGetToken()
        {
            // get token
            var token = getToken();
            Assert.True(!string.IsNullOrEmpty(token));

            // create new user
            var user = createUser();
            Assert.True(user);

        }

        /* private stuff */


        private void saveTokenToFile(string token)
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

        private string getTokenFromFile()
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



        private string getToken()
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
            return "";
        }



        private bool createUser()
        {
            var token = getToken();
            //create RestSharp client and POST request object

            var url = $"{baseUrl}/api/CRUD/test";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            //request headers
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");

            //object containing input parameter data for DoStuff() API method
            var apiInput = new {
                UserName = "Matt",
                NormalizedUserName = "Mattius",
                Email = "info@matt.com",
                PasswordHash = "asd",
            };

            //add parameters and token to request
            request.Parameters.Clear();
            request.AddParameter("application/json", JsonConvert.SerializeObject(apiInput), ParameterType.RequestBody);
            request.AddParameter("Authorization", "Bearer " + token, ParameterType.HttpHeader);

            //make the API request and get a response
            IRestResponse response = client.Execute(request);

            //ApiResponse is a class to model the data we want from the API response
            if (!response.IsSuccessful)
            {
                return false;
            }

            return true;


        }



        private bool deleteUser()
        {
            return false;
        }

        private bool updateUser()
        {
            return false;
        }

        private bool getUser()
        {
            return false;
        }


       
    }


}