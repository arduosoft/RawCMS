using CommandLine;
using Newtonsoft.Json;
using RawCMSClient.BLL.Parser;
using RawCMSClient.BLL.Core;
using RawCMSClient.BLL.Helper;
using RawCMSClient.BLL.Log;
using RawCMSClient.BLL.Model;
using System;
using System.Collections.Generic;

namespace RawCMSClient
{
    class Program
    {

        private static Runner log = LogProvider.Runner;


        static int Main(string[] args)
        {

            Console.WriteLine(RawCmsHelper.Message);

            var ret = Parser.Default.ParseArguments<ClientOptions, LoginOptions, ListOptions, CreateOptions>(args)
                    .MapResult(
                      (ClientOptions opts) => RunClientOptionsCode(opts),
                      (LoginOptions opts) => RunLoginOptionsCode(opts),
                      (ListOptions opts) => RunListOptionsCode(opts),
                      (CreateOptions opts) => RunCreateOptionsCode(opts),
                      errs => RunErrorCode(errs));

            log.Info("done.");
            return ret;

        }

        private static int RunErrorCode(IEnumerable<Error> errs)
        {
            //log.Warn("Some parameters are missing:");
            //foreach (MissingRequiredOptionError item in errs)
            //{

            //    log.Warn($"Missing: {item.NameInfo.NameText}");

            //}
            return 1;
        }

        private static int RunCreateOptionsCode(CreateOptions opts)
        {
            var collection = opts.Collection;
            var filePath = opts.FilePath;

            if (opts.Verbose)
            {

                log.Info($"workin into collection: {collection}");
                log.Info($"filedata path: {filePath}");
            }

            // check if file exists
            if (!System.IO.File.Exists(filePath))
            {
                log.Warn($"File not found: {filePath}");
                return 0;
            }

            // check if file is valid json

            var check = RawCmsHelper.CheckJSON(filePath);

            if (check!=0)
            {
                log.Warn("skip file.");
                return 0;
            }

            // TODO: save data to collection....

            return 0;
        }

        private static int RunListOptionsCode(ListOptions opts)
        {
            bool Verbose = false;
            bool Pretty = false;

            if (opts.Verbose)
            {
                log.Info("Verbose mode enabled.");
                Verbose = true;
            }
            if (opts.Pretty)
            {
                Pretty = true;
            }

            // get token from file
            // check token befare action..

            var token = TokenHelper.getTokenFromFile();

            if (string.IsNullOrEmpty(token))
            {
                log.Warn("No token found. Please login before continue.");
                log.Warn("Program aborted.");
                return 0;

            };
            if (Verbose)
            {
                log.Info($"Token: {token}");
            }

            var collection = opts.Collection;
            var id = opts.Id;
            var ret = string.Empty;

            if (string.IsNullOrEmpty(opts.Collection))
            {
                log.Warn("Collection name is rquired for list command.");
                log.Warn("Program aborted.");
                return 0;
            }

            log.Debug($"perform action in collection: {collection}");


            ListRequest req = new ListRequest
            {
                Collection = collection,
                Token = token
            };

            if (!string.IsNullOrEmpty(id))
            {
                req.Id = id;
            }

            ret = RawCmsHelper.GetData(req);
            if (string.IsNullOrEmpty(ret))
            {
                log.Info("Response has no data.");
            }
            else
            {
                if (Pretty)
                {
                    var obj = JsonConvert.DeserializeObject(ret);
                    ret = JsonConvert.SerializeObject(obj, Formatting.Indented);
                    log.Info($"Result query:\n------------- RESULT -------------\n\n{ret}\n\n-------------------------------------\n");

                }
            }
            return 0;
        }

        private static int RunLoginOptionsCode(LoginOptions opts)
        {
            bool Verbose = false;
            var token = string.Empty;

            if (opts.Verbose)
            {
                log.Info("Verbose mode enabled.");
                Verbose = true;
            }

            if (false
            || string.IsNullOrEmpty(opts.Username)
            || string.IsNullOrEmpty(opts.Pasword)
            || string.IsNullOrEmpty(opts.ClientId)
            || string.IsNullOrEmpty(opts.ClientSecret))
            {
                log.Warn("Login (-l) params reqire other parameters:\n(-u) username\n(-p) password\n(-i) clientid\n(-t) clientsecret");
                log.Warn("Program aborted.");
                return 0;
            }

            try
            {
                token = TokenHelper.getToken(opts.Username, opts.Pasword, opts.ClientId, opts.ClientSecret);

            }
            catch (ExceptionToken e)
            {
                log.Error($"token error: {e.Code}, {e.Message}");
                return 2;
            }
            catch (Exception e)
            {
                log.Error("token error", e);
                return 2;
            }


            if (string.IsNullOrEmpty(token))
            {
                log.Warn("Unable to get token. check if data are correct and retry.");
                log.Warn("Program aborted.");
                return 2;

            }


            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileconfigname = ClientConfig.GetValue<string>("ConfigFile");
            fileconfigname = string.Format(fileconfigname, opts.Username);
            string filePath = System.IO.Path.Combine(mydocpath, fileconfigname);


            var f = TokenHelper.SaveTokenToFile(filePath, token);
            log.Info("set enviroinment configuration: (copy, paste and hit return in console)");
            log.Info($"\n\nSET RAWCMSCONFIG={f}");


            if (Verbose)
            {
                log.Info($"\n---- TOKEN ------\n{token}\n-----------------");

            }
            return 0;
        }

        private static int RunClientOptionsCode(ClientOptions opts)
        {
            return 0;
        }
    }
}
