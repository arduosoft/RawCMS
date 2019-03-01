using CommandLine;
using Newtonsoft.Json;
using RawCMSClient.BLL.Parser;
using RawCMSClient.BLL.Core;
using RawCMSClient.BLL.Helper;
using RawCMSClient.BLL.Core;
using RawCMSClient.BLL.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace RawCMSClient
{
    class Program
    {

        private static Runner log = LogProvider.Runner;


        static int Main(string[] args)
        {

            Console.WriteLine(RawCmsHelper.Message);

            var ret = Parser.Default.ParseArguments<ClientOptions, LoginOptions, ListOptions, InsertOptions>(args)
                    .MapResult(
                      (ClientOptions opts) => RunClientOptionsCode(opts),
                      (LoginOptions opts) => RunLoginOptionsCode(opts),
                      (ListOptions opts) => RunListOptionsCode(opts),
                      (InsertOptions opts) => RunInsertOptionsCode(opts),
                      (ReplaceOptions opts) => RunReplacetOptionsCode(opts),
                      (DeleteOptions opts) => RunDeleteOptionsCode(opts),
                      (PatchOptions opts) => RunPatchOptionsCode(opts),


                      errs => RunErrorCode(errs));

            log.Info("done.");
            return ret;

        }

        private static int RunPatchOptionsCode(PatchOptions opts)
        {
            throw new NotImplementedException();
        }

        private static int RunDeleteOptionsCode(DeleteOptions opts)
        {
            throw new NotImplementedException();
        }

        private static int RunReplacetOptionsCode(ReplaceOptions opts)
        {
            throw new NotImplementedException();
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

        private static int RunInsertOptionsCode(InsertOptions opts)
        {
            var Verbose = opts.Verbose;
            var Recursive = opts.Recursive;
            var DryRun = opts.Recursive;
            var collection = opts.Collection;
            var filePath = opts.FilePath;
            var folderPath = opts.FolderPath;


            if (opts.Verbose)
            {
                log.Info("Verbose mode enabled.");
                Verbose = true;
            }

            // check token befare action..
            log.Debug("get token from file...");
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
                log.Info($"workin into collection: {collection}");
            }


            Dictionary<string, string> listFile = new Dictionary<string, string>();

            // pass a file to options
            if (!string.IsNullOrEmpty(filePath) )
            {
                // check if file exists
                if ( !File.Exists(filePath))
                {
                    log.Warn($"File not found: {filePath}");
                    return 0;
                }

                // check if file is valid json

                var check = RawCmsHelper.CheckJSON(filePath);

                if (check != 0)
                {
                    log.Warn("son is not well-formatted. Skip file.");
                    return 0;
                }

                listFile.Add(collection, filePath);


            }
            else if (!string.IsNullOrEmpty(folderPath))
            {
                // get all file from folder
                if (!Directory.Exists(folderPath))
                {
                    log.Warn($"File not found: {filePath}");
                    return 0;
                 
                }

                // This path is a directory
                // get first level path, 
                // folder => collection
                string[] subdirectoryEntries = Directory.GetDirectories(folderPath);
                foreach (string  subDir in subdirectoryEntries)
                {
                    RawCmsHelper.ProcessDirectory(Recursive, listFile, subDir, subDir);
                }
            }
            else
            {
                log.Warn("At least one of the two options -f (file) or -d (folder) is mandatory.");
                return 0;
            }


               

            foreach (var item in listFile)
            {
                RawCmsHelper.CreateElement(new CreateRequest
                {
                    Collection = item.Key,
                    Data = item.Value,
                    Token = token
                });
            }

           

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


            // check token befare action..
            log.Debug("get token from file...");
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
