using CommandLine;
using RestSharp;
using System;

namespace RawCMSClient
{
    class Program
    {

        static void Main(string[] args)
        {
            ClientOptions o = new ClientOptions();

            string token = string.Empty;

            Parser.Default.ParseArguments<ClientOptions>(args)
                .WithParsed<ClientOptions>(opts => {
                    if (opts.Verbose)
                    {
                        Console.WriteLine("Verbose mode enabled.");
                    }

                    if (opts.Login)
                    {
                        if ( false
                        || string.IsNullOrEmpty(opts.Username) 
                        || string.IsNullOrEmpty(opts.Pasword)
                        || string.IsNullOrEmpty(opts.ClientId)
                        || string.IsNullOrEmpty(opts.ClientSecret) )
                        {
                            Console.WriteLine("login (-l) params reqire other parameters: (-u) username, (-p) password, (-i) clientid, (-t) clientsecret.");
                            Console.WriteLine("Program aborted."); return;
                        }

                        token = TokenHelper.getToken(opts.Username, opts.Pasword, opts.ClientId, opts.ClientSecret);

                        if (string.IsNullOrEmpty(token))
                        {
                            Console.WriteLine("Unable to get token. check if data are correct and retry.");
                            Console.WriteLine("Program aborted."); return;
                        }

                        if (opts.Verbose)
                        {
                            Console.WriteLine("New token:");
                            Console.WriteLine(token);

                        }
                        
                    }

                    // check token befare action..
                    if (string.IsNullOrEmpty(token))
                    {
                        token = TokenHelper.getTokenFromFile();
                        if (string.IsNullOrEmpty(token))
                        {
                            Console.WriteLine("No token found. Please login before continue.");
                            Console.WriteLine("Program aborted."); return;
                        }
                    }


                    switch (opts.Command)
                    {
                        case CommandType.none:
                            break;
                        case CommandType.create:
                           
                            string data = string.Empty;
                            try
                            {
                                if (o.Verbose)
                                {
                                    Console.WriteLine($"parsing file: {opts.DataFile} ...");
                                }
                                data = ParseDataFile(opts.DataFile);

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("data file error: {0}", e.Message);
                                Console.WriteLine("Program aborted.");return;
                            }
                            return;

                
                        case CommandType.delete:
                            return;
                        case CommandType.get:
                            
                            return;
                        case CommandType.update:
                            return;
                        default:
                            return;
                    }

                  

                });


        }

      
    

        private static string ParseDataFile(string dataFile)
        {
            throw new NotImplementedException();
        }
    }
}
