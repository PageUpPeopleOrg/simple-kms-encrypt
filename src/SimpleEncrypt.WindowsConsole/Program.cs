using System;
using System.Diagnostics;
using CommandLine;
using static System.Console;

namespace SimpleEncrypt.WindowsConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Debugger.Launch();
                var options = new Options();
                Parser.Default.ParseArguments(args, options, (verb, suboptions) =>
                {
                    if (suboptions is EncryptOptions)
                    {
                        Encrypt(suboptions as EncryptOptions);
                    }
                    else if(suboptions is DecryptOptions)
                    {
                        Decrypt(suboptions as DecryptOptions);
                    }
                });
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }

        private static void Decrypt(DecryptOptions opts)
        {
            try
            {
                WriteLine(opts.Value.DecryptAsync(opts.Region).Result);
            }
            catch (Exception)
            {   
                throw;
            }
        }

        private static void Encrypt(EncryptOptions opts)
        {
            try
            {
                WriteLine(opts.Value.EncryptAsync(opts.KeyId, opts.Region).Result);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }

    internal class Options
    {
        [Option('r', "region", HelpText = "The {AWSREGION} that contains the key", Required = true)]
        public string Region { get; set; }

        [VerbOption("decrypt", HelpText = "decrypts value")]
        public DecryptOptions DecryptVerb { get; set; }

        [VerbOption("encrypt", HelpText = "encrypts a value using the key")]
        public EncryptOptions EncryptVerb { get; set; }
    }
    
    internal class EncryptOptions : Options
    {
        [Option('k', "keyId", HelpText = "The {KEYID} to encrypt the value against", Required = true)]
        public string KeyId { get; set; }

        [Option('v', "value", HelpText = "The {VALUE} to encrypt", Required = true)]
        public string Value { get; set; }
    }
    
    internal class DecryptOptions : Options
    {
        [Option('v', "value", HelpText = "The {VALUE} to encrypt", Required = true)]
        public string Value { get; set; }
    }
}