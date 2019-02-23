using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMSClient.BLL.ClientOptions
{

    [Verb("create", HelpText = "create data into collection")]
    public class CreateOptions
    {

        [Option('c', "collection", Required = true, HelpText = "Collection where to do the operation.")]
        public string Collection { get; set; }
        
        [Option('d', "data", Required = false, HelpText = "file contains element to put into.")]
        public string FilePath { get; set; }



    }
}
