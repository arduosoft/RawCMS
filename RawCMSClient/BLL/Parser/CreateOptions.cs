using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMSClient.BLL.Parser
{

    [Verb("create", HelpText = "create data inside collection")]
    public class CreateOptions
    {
       
        [Option('v', "verbose", Default = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }
        
        [Option('c', "collection", Required = true, HelpText = "Collection where to do the operation.")]
        public string Collection { get; set; }    


        [Option('f', "file",  SetName = "file", HelpText = "Fle contains data to put into collection. it can be a json file well-formatted.")]
        public string FilePath { get; set; }

        [Option('d', "folder",  SetName = "file", HelpText = "Folder contains data to put into collection. it can be a json file well-formatted.")]
        public string FolderPath { get; set; }

        [Option('r', "recursive", Required = false, HelpText = "Fle path contains data to put into collection. it can be a json file well-formatted.")]
        public bool Recursive { get; set; }



    }
}
