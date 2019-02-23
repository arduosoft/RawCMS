using CommandLine;

namespace RawCMSClient.BLL.ClientOptions
{
    [Verb("list", HelpText = "list data from collection: with -id params, get element by id")]
    public class ListOptions
    {

        [Option('c', "collection", Required = true, HelpText = "Collection where to do the operation.")]
        public string Collection { get; set; }


        [Option('i', "id", Required = false, HelpText = "Element id to get.")]
        public string Id { get; set; }



    }
}
