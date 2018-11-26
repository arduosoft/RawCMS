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

                    switch (opts.Command)
                    {
                        case CommandType.none:
                            break;
                        case CommandType.create:
                            string data = string.Empty;
                            try
                            {
                                data = ParseDataFile(opts.DataFile);

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("data file error: {0}", e.Message);
                            }
                            

                            break;
                        case CommandType.delete:
                            break;
                        case CommandType.get:
                            break;
                        case CommandType.update:
                            break;
                        default:
                            break;
                    }

                  

                });


        }

        private static string ParseDataFile(string dataFile)
        {
            throw new NotImplementedException();
        }
    }
}
