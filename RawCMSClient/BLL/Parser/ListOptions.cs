using CommandLine;

namespace RawCMSClient.BLL.Parser
{
    [Verb("list", HelpText = "list data from collection: with -id params, get element by id")]
    public class ListOptions
    {

        [Verb("users", HelpText = "users collections")]
        public class UsersOptions
        {
            [Option( "id", Required =false, HelpText = "get user by id")]
            public string Id { get; set; }

        }

        [Option('v', "verbose", Default = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [Option('p', "pretty", Default = false, HelpText = "Format JSON output.")]
        public bool Pretty { get; set; }


        [Option('c', "collection", Required = true, HelpText = "Collection where to do the operation.")]
        public string Collection { get; set; }


        [Option('i', "id", Required = false, HelpText = "Element id to get.")]
        public string Id { get; set; }



    }
}
