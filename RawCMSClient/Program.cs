using CommandLine;
using System;

namespace RawCMSClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientOptions o = new ClientOptions();


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
                            Console.WriteLine("login params (-l) reqire: username (-u), password (-p), clientid (-i) and clientsecret (-t).");
                            return;
                        }


                        return;
                    }

                    switch (opts.Command)
                    {
                        case CommandType.none:
                            break;
                        case CommandType.create:
                            string data = string.Empty;
                            try
                            {
                                if (o.Verbose) Console.WriteLine($"parsing file: {opts.DataFile} ...");
                                data = ParseDataFile(opts.DataFile);

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("data file error: {0}", e.Message);
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
