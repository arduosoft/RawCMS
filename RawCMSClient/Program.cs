using CommandLine;
using Newtonsoft.Json;
using RawCMSClient.BLL.ClientOptions;
using RawCMSClient.BLL.Core;
using RawCMSClient.BLL.Helper;
using RawCMSClient.BLL.Log;
using RawCMSClient.BLL.Model;
using System;

namespace RawCMSClient
{
    class Program
    {

        private static Runner log = LogProvider.Runner;


        static void Main(string[] args)
        {

            bool Verbose = false;
            string token = string.Empty;
            bool Pretty = false;

            Parser.Default.ParseArguments<ClientOptions, LoginOptions, ListOptions>(args)
                .WithParsed<ClientOptions>(opts =>
                {


                    if (opts.Verbose)
                    {
                        log.Info("Verbose mode enabled.");
                        Verbose = true;
                    }
                    if (opts.Pretty)
                    {
                        Pretty = true;
                    }


                })
                .WithParsed<LoginOptions>(opts =>
                {

                    if (false
                    || string.IsNullOrEmpty(opts.Username)
                    || string.IsNullOrEmpty(opts.Pasword)
                    || string.IsNullOrEmpty(opts.ClientId)
                    || string.IsNullOrEmpty(opts.ClientSecret))
                    {
                        log.Warn("Login (-l) params reqire other parameters:\n(-u) username\n(-p) password\n(-i) clientid\n(-t) clientsecret");
                        log.Warn("Program aborted.");
                        return;
                    }

                    try
                    {
                        token = TokenHelper.getToken(opts.Username, opts.Pasword, opts.ClientId, opts.ClientSecret);

                    }
                    catch (ExceptionToken e)
                    {
                        log.Error($"token error: {e.Code}, {e.Message}");
                        return;
                    }
                    catch (Exception e)
                    {
                        log.Error("token error", e);
                        return;
                    }


                    if (string.IsNullOrEmpty(token))
                    {
                        log.Warn("Unable to get token. check if data are correct and retry.");
                        log.Warn("Program aborted.");
                        return;

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

                })
                .WithParsed<ListOptions>(opts =>
                {

                    // get token from file
                    // check token befare action..

                    token = TokenHelper.getTokenFromFile();

                    if (string.IsNullOrEmpty(token))
                    {
                        log.Warn("No token found. Please login before continue.");
                        log.Warn("Program aborted.");
                        return;

                    };

                    var collection = opts.Collection;
                    var id = opts.Id;
                    var ret = string.Empty;

                    if (string.IsNullOrEmpty(opts.Collection))
                    {
                        log.Warn("Collection name is rquired for list command.");
                        log.Warn("Program aborted.");
                        return;
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
                  

                  

                })
                .WithNotParsed(errs =>
                {
                    log.Error("no valid arguments..");

                });

                log.Info("done.");

        }




    }
}
