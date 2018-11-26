using Xunit;
using RestSharp;
using Newtonsoft.Json;
using System;

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

        public string GetToken()
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



        public bool CreateUser()
        {
            var token = GetToken();
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

        public bool DeleteUser()
        {
            return false;
        }

        public bool UpdateUser()
        {
            return false;
        }

        public bool GetUser()
        {
            return false;
        }


        [Fact]
        public void TestGetToken()
        {
            // get token
            var token = GetToken();
            Assert.True(!string.IsNullOrEmpty(token));

            // create new user
            var user = CreateUser();
            Assert.True(user);



        }



    }


}