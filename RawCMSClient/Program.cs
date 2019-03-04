using CommandLine;
using RawCMS.Client.BLL.Parser;
using RawCMS.Client.BLL.Core;
using RawCMS.Client.BLL.Helper;
using RawCMS.Client.BLL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RawCMS.Client
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

            log.Info("Done.");
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
            var DryRun = opts.DryRun;
            var Pretty = opts.Pretty;
            var collection = opts.Collection;
            var filePath = opts.FilePath;
            var folderPath = opts.FolderPath;

            // setting log/console Output 
            log.SetVerbose(Verbose);
            log.SetPretty(Pretty);

          
            // check token befare action..
            var token = TokenHelper.getTokenFromFile();


            if (string.IsNullOrEmpty(token))
            {
                log.Warn("No token found. Please login before continue.");
                log.Warn("Program aborted.");
                return 0;

            };



            log.Debug($"Working into collection: {collection}");
          

            Dictionary<string, List<string>> listFile = new Dictionary<string, List<string>>();


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
                    log.Warn("Json is not well-formatted. Skip file.");
                    return 0;
                }
                List<string> filelist = new List<string>();
                filelist.Add(filePath);
                listFile.Add(collection, filelist);


            }
            else if (!string.IsNullOrEmpty(folderPath))
            {
                string cwd = Directory.GetCurrentDirectory();
                log.Info($"Current working directory: {cwd}");

                // get all file from folder
                if (!Directory.Exists(folderPath))
                {
                    log.Warn($"File not found: {filePath}");
                    return 0;
                 
                }

                // This path is a directory
                // get first level path, 
                // folder => collection
                DirectoryInfo dInfo = new DirectoryInfo(folderPath);
                DirectoryInfo[] subdirs = dInfo.GetDirectories();

                foreach (DirectoryInfo subDir in subdirs)
                {
                    RawCmsHelper.ProcessDirectory(Recursive, listFile, subDir.FullName,subDir.Name);
                }
            }
            else
            {
                log.Warn("At least one of the two options -f (file) or -d (folder) is mandatory.");
                return 0;
            }

            elaborateQueue(listFile,token,Pretty);
            

            log.Info($"Processing file complete.");
            return 0;
        }

        private static void elaborateQueue(Dictionary<string, List<string>> listFile,string token,bool pretty)
        {
            int totalfile = listFile.Sum(x => x.Value.Count);
            int partialOfTotal = 0;

            foreach (var c in listFile)
            {
                int progress = 0;
            
                foreach (var item in c.Value)
                {
                    log.Info($"Processing file {++progress} of {c.Value.Count} in collection: {c.Key}");
                    
                    var contentFile = File.ReadAllText(item);
                    
                    
                    log.Request(contentFile);

                    var responseRawCMS = RawCmsHelper.CreateElement(new CreateRequest
                    {
                        Collection = c.Key,
                        Data = contentFile,
                        Token = token
                    });

                    log.Debug($"RawCMS response code: {responseRawCMS.StatusCode}");

                    if (!responseRawCMS.IsSuccessful)
                    {
                        //log.Error($"Error occurred: \n{responseRawCMS.Content}");
                        log.Error($"Error: {responseRawCMS.ErrorMessage}");
                    }
                    else
                    {
                        log.Response(responseRawCMS.Content);
                    }

                    //switch (responseRawCMS.ResponseStatus)
                    //{

                    //    case RestSharp.ResponseStatus.Completed:
                    //        log.Response(responseRawCMS.Content);

                    //        break;

                    //    case RestSharp.ResponseStatus.None:
                    //    case RestSharp.ResponseStatus.Error:
                    //    case RestSharp.ResponseStatus.TimedOut:
                    //    case RestSharp.ResponseStatus.Aborted:

                    //    default:
                    //        log.Error($"Error response: {responseRawCMS.ErrorMessage}");
                    //        break;
                    //}


                    log.Info($"File processed\n\tCollection progress: {progress} of {c.Value.Count}\n\tTotal progress: {++partialOfTotal} of {totalfile}\n\tFile: {item}\n\tCollection: {c.Key}");
                }

            }
        }

        private static int RunListOptionsCode(ListOptions opts)
        {
           
            var Verbose = opts.Verbose;
            log.SetVerbose(Verbose);

            var Pretty = opts.Pretty;
            log.SetPretty(Pretty);

            var PageSize = opts.PageSize;
            var PageNumber = opts.PageNumber;
            var RawQuery = opts.RawQuery;

            var id = opts.Id;
            var collection = opts.Collection;
            
            

            // check token befare action..
            var token = TokenHelper.getTokenFromFile();

            if (string.IsNullOrEmpty(token))
            {
                log.Warn("No token found. Please login.");
                return 0;

            };
            
           
            log.Debug($"Perform action in collection: {collection}");


            ListRequest req = new ListRequest
            {
                Collection = collection,
                Token = token,
                PageNumber = PageNumber < 1 ? 1 : PageNumber,
                PageSize = PageSize < 1 ? 10 : PageSize,
                RawQuery = RawQuery,
            };

            if (!string.IsNullOrEmpty(id))
            {
                req.Id = id;
            }

            var responseRawCMS = RawCmsHelper.GetData(req);
           
            log.Debug($"RawCMS response code: {responseRawCMS.StatusCode}");

            if (!responseRawCMS.IsSuccessful)
            {
                //log.Error($"Error occurred: \n{responseRawCMS.Content}");
                log.Error($"Error: {responseRawCMS.ErrorMessage}");
            }
            else
            {
                log.Response(responseRawCMS.Content);
            }

            //switch (responseRawCMS.ResponseStatus)
            //{

            //    case RestSharp.ResponseStatus.Completed:
            
            //        break;

            //    case RestSharp.ResponseStatus.None:
            //    case RestSharp.ResponseStatus.Error:
            //    case RestSharp.ResponseStatus.TimedOut:
            //    case RestSharp.ResponseStatus.Aborted:

            //    default:
            //        log.Error($"Error response: {responseRawCMS.ErrorMessage}");
            //        break;
            //}
    
            
            return 0;
        }

        private static int RunLoginOptionsCode(LoginOptions opts)
        {
            bool Verbose = false;
            log.SetVerbose(Verbose);

            var token = string.Empty;
            
           
            try
            {
                token = TokenHelper.getToken(opts);
                log.Debug($"\n---- TOKEN ------\n{token}\n-----------------");

            }
            catch (ExceptionToken e)
            {
                log.Error($"token error:");
                log.Error($"\t{e.Code}, {e.Message}");
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
                return 2;

            }


            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileconfigname = ClientConfig.GetValue<string>("ConfigFile");
            fileconfigname = string.Format(fileconfigname, opts.Username);
            string filePath = System.IO.Path.Combine(mydocpath, fileconfigname);

            ConfigFile cf = new ConfigFile
            {
                Token = token,
                CreatedTime = DateTime.Now.ToShortDateString(),
                ServerUrl = opts.ServerUrl,
                User = opts.Username

            };

            TokenHelper.SaveTokenToFile(filePath, cf);

            log.Info($"set enviroinment configuration: (copy, paste and hit return in console):\nSET RAWCMSCONFIG={filePath}");
            
            return 0;
        }

        private static int RunClientOptionsCode(ClientOptions opts)
        {
            return 0;
        }
    }
}
