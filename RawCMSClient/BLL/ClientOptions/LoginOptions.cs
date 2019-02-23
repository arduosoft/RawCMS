using CommandLine;

namespace RawCMSClient.BLL.ClientOptions
{
    [Verb("login", HelpText = "Login. Required also (-u) username, (-p) password, (-i) client id, (-t) client secret")]
    public class LoginOptions
    {

        [Option('u', "username", Required = false, HelpText = "Username")]
        public string Username { get; set; }

        [Option('p', "password", Required = false, HelpText = "Password")]
        public string Pasword { get; set; }

        [Option('i', "clientid", Required = false, HelpText = "Client id")]
        public string ClientId { get; set; }

        [Option('t', "clientsecret", Required = false, HelpText = "Client secret")]
        public string ClientSecret { get; set; }



    }
}
