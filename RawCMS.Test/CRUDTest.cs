using Xunit;
using RestSharp;
using Newtonsoft.Json;
using System;
using System.IO;
using RawCMSClient;
using RawCMSClient.BLL.Helper;
using RawCMSClient.BLL.Core;

namespace RawCMS.Test
{
    

   
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
            string token = TokenHelper.getToken(username, password, clientId, clientSecret);


            Assert.False(string.IsNullOrEmpty(token));

            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileconfigname = ClientConfig.GetValue<string>("ConfigFile");
            fileconfigname = string.Format(fileconfigname, username);
            string filePath = System.IO.Path.Combine(mydocpath, fileconfigname);

            TokenHelper.SaveTokenToFile(filePath,token);
            string tokenfile = TokenHelper.getTokenFromFile();
            Assert.True(tokenfile.Equals(token));

        }

        [Fact]
        public void TestGetToken()
        {
            // get token
            string token = TokenHelper.getToken(username, password, clientId, clientSecret);
            Assert.True(!string.IsNullOrEmpty(token));

            // create new user
            var user = createUser();
            Assert.True(user);

        }

     
       


        private bool createUser()
        {
            string token = TokenHelper.getToken(username, password, clientId, clientSecret);
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