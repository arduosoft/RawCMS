using CommandLine;

namespace RawCMSClient.BLL.Parser
{

    [Verb("client", HelpText = "set client configuration")]

    public class ClientOptions
    {
         //[Option('s', "syncronize", Required = false,  HelpText = "File path to synchronize the db.")]
        //public string SincronizationFile { get; set; }

        //[Option('r',"purge", Default = false, HelpText = "Remove data during syncronization. Only with syncronization (-s)")]
        //public bool RemoveData { get; set; }


        //[Option('d', "data", Required = false, HelpText = "file path contains data. using with create, update")]
        //public string DataFile { get; set; }



    }


}


