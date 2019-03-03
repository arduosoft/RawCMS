using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMSClient.BLL.Parser
{

    [Verb("insert", HelpText = "insert data inside collection")]
    public class InsertOptions
    {
       
        [Option('v', "verbose", Default = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [Option('p', "pretty", Default = false, HelpText = "Format JSON output.")]
        public bool Pretty { get; set; }

        [Option('c', "collection", Required = true, HelpText = "Collection where to do the operation.")]
        public string Collection { get; set; }    


        [Option('f', "file",  SetName = "file", HelpText = "Fle contains data to insert into collection. it can be a json file well-formatted.")]
        public string FilePath { get; set; }

        [Option('d', "folder",  SetName = "file", HelpText = "Folder contains data to insert into collection. it can be a json file well-formatted.")]
        public string FolderPath { get; set; }

        [Option('r', "recursive", Required = false, HelpText = "Fle path contains data to insert into collection. it can be a json file well-formatted.")]
        public bool Recursive { get; set; }


        [Option('t', "dry-run", Required = false, HelpText = "do not perform any operations, just make a try.")]
        public bool DryRun { get; set; }
    }
}
