using CommandLine;

namespace RawCMSClient.BLL.ClientOptions
{


    public class ClientOptions
    {
        [Option('v', "verbose",  Default = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [Option('p', "prettyJSON", Default = false, HelpText = "Format JSON output.")]
        public bool Pretty { get; set; }

        //[Option('s', "syncronize", Required = false,  HelpText = "File path to synchronize the db.")]
        //public string SincronizationFile { get; set; }

        //[Option('r',"purge", Default = false, HelpText = "Remove data during syncronization. Only with syncronization (-s)")]
        //public bool RemoveData { get; set; }


        //[Option('d', "data", Required = false, HelpText = "file path contains data. using with create, update")]
        //public string DataFile { get; set; }



    }


}


