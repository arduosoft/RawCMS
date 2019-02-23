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
    }
}
