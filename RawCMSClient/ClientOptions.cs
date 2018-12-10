using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMSClient
{
    public class ClientOptions
    {
        [Option('v', "verbose", Default = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [Option('l', "login", Default = false, HelpText = "Login")]
        public bool Login { get; set; }

        [Option('u', "username", Required = false, HelpText = "Username for login")]
        public string Username { get; set; }

        [Option('p', "password", Required = false, HelpText = "Password for login")]
        public string Pasword { get; set; }

        [Option('i', "clientid", Required = false, HelpText = "client id for getting token")]
        public string ClientId { get; set; }

        [Option('t', "clientsecret", Required = false, HelpText = "client secret for getting token")]
        public string ClientSecret { get; set; }


        [Option('s', "syncronize", Required = false,  HelpText = "file path to synchronize the db.")]
        public string SincronizationFile { get; set; }
  
        [Option('r',"purge", Default = false, HelpText = "Remove data during syncronization. Only with syncronization (-s)")]
        public bool RemoveData { get; set; }

        [Option('a', "action", Default = CommandType.none, Required = false, HelpText = "Specifies the operation that you want to perform on one or more resources, for example create, get, delete, update")]
        public CommandType Command { get; set; }

        [Option('c',"collection", Required = false,  HelpText = "Collection where to do the operation.")]
        public string Collection { get; set; }

        [Option('d', "data", Required = false, HelpText = "file path contains data. using with create, update")]
        public string DataFile { get; set; }




    }
}


