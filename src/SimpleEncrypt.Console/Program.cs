using System;
using CommandLine;
using static System.Console;

namespace SimpleEncrypt.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Parser.Default.ParseArguments<EncryptOptions, DecryptOptions>(args)
                    .WithParsed<EncryptOptions>(Encrypt)
                    .WithParsed<DecryptOptions>(Decrypt)
                    .WithNotParsed(errs => { });
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }

        private static void Decrypt(DecryptOptions opts)
        {
            WriteLine(opts.Value.Decrypt(opts.Region));
        }

        private static void Encrypt(EncryptOptions opts)
        {
            WriteLine(opts.Value.Encrypt(opts.KeyId, opts.Region));
        }
    }

    internal class Options
    {
        [Option('r', "region", HelpText = "The {AWSREGION} that contains the key", Required = true)]
        public string Region { get; set; }
    }

    [Verb("encrypt", HelpText = "encrypts a value using the key")]
    internal class EncryptOptions: Options
    {
        [Option('k', "keyId", HelpText = "The {KEYID} to encrypt the value against", Required = true)]
        public string KeyId { get; set; }

        [Option('v', "value", HelpText = "The {VALUE} to encrypt", Required = true)]
        public string Value { get; set; }
    }

    [Verb("decrypt", HelpText = "decrypts value")]
    internal class DecryptOptions: Options
    {
        [Option('v', "value", HelpText = "The {VALUE} to encrypt", Required = true)]
        public string Value { get; set; }
    }
}
