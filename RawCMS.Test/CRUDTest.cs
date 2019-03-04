using Xunit;
using RestSharp;
using Newtonsoft.Json;
using System;
using System.IO;
using RawCMS.Client;
using RawCMS.Client.BLL.Helper;
using RawCMS.Client.BLL.Core;
using System.Collections.Generic;
using RawCMS.Client.BLL.Parser;

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
            LoginOptions lo = new LoginOptions
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                Password = password,
                ServerUrl = baseUrl,
                Username = username,
                
            };
            string token = TokenHelper.getToken(lo);


            Assert.False(string.IsNullOrEmpty(token));

            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileconfigname = ClientConfig.GetValue<string>("ConfigFile");
            fileconfigname = string.Format(fileconfigname, username);
            string filePath = System.IO.Path.Combine(mydocpath, fileconfigname);

            ConfigFile cf = new ConfigFile
            {
                Token = token,
                CreatedTime = DateTime.Now.ToShortDateString(),
                ServerUrl = baseUrl,
                User = username

            };

            TokenHelper.SaveTokenToFile(filePath, cf);
            string tokenfile = TokenHelper.getTokenFromFile();
            Assert.True(tokenfile.Equals(token));

        }

        [Fact]
        public void TestGetToken()
        {
            LoginOptions lo = new LoginOptions
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                Password = password,
                ServerUrl = baseUrl,
                Username = username,

            };
            // get token
            string token = TokenHelper.getToken(lo);
            Assert.True(!string.IsNullOrEmpty(token));

            // create new user
            var user = createUser();
            Assert.True(user);

        }

        
     


        private bool createUser()
        {
            LoginOptions lo = new LoginOptions
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                Password = password,
                ServerUrl = baseUrl,
                Username = username,

            };
            string token = TokenHelper.getToken(lo);
            //create RestSharp client and POST request object
            var collection = "_users";

            var url = $"{baseUrl}/api/CRUD/{collection}";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            //request headers
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");

            //object containing input parameter data for DoStuff() API method
            var apiInput = new
            {
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