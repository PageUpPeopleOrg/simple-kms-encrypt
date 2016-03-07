using System.Linq;
using CommandLine;
using static System.Console;

namespace SimpleEncrypt.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Error[] errors = null;

            Parser.Default.ParseArguments<EncryptOptions, DecryptOptions>(args)
                .WithParsed<EncryptOptions>(opts => opts.Value.Encrypt(opts.KeyId, opts.Region))
                .WithParsed<DecryptOptions>(opts => opts.Value.Decrypt(opts.Region))
                .WithNotParsed(errs => errors = errs.ToArray());
            
            if (errors != null && errors.Any())
            {
                foreach (var error in errors)
                {
                    WriteLine(error);
                }
            }
        }
    }

    [Verb("encrypt", HelpText = "encrypts a value using the key")]
    internal class EncryptOptions
    {
        [Option('r', "region", HelpText = "The {AWSREGION} that contains the key")]
        public string Region { get; set; }

        [Option('k', "keyId", HelpText = "The {KEYID} to encrypt the value against")]
        public string KeyId { get; set; }

        [Option('v', "value", HelpText = "The {VALUE} to encrypt")]
        public string Value { get; set; }
    }

    [Verb("decrypt", HelpText = "decrypts value")]
    internal class DecryptOptions
    {
        [Option('r', "region", HelpText = "The {AWSREGION} that contains the key")]
        public string Region { get; set; }

        [Option('v', "value", HelpText = "The {VALUE} to encrypt")]
        public string Value { get; set; }
    }
}
