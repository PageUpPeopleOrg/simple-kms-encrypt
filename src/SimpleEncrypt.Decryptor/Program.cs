using System;
using NDesk.Options;

namespace SimpleEncrypt.Decryptor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var decryptorArgs = DecryptorArgs.Parse(args);
            if (decryptorArgs == null)
            {
                return;
            }

            Console.WriteLine(decryptorArgs.Value.Decrypt(decryptorArgs.Region));
        }
    }

    public class DecryptorArgs
    {
        public string Region { get; private set; }
        public string Value { get; private set; }

        public static DecryptorArgs Parse(string[] args)
        {
            string region = null;
            string value = null;
            var displayHelp = false;

            var options = new OptionSet
            {
                {
                    "r|region=",
                    "The {AWSREGION} that contains the key",
                    v => region = v
                },
                {
                    "v|value=",
                    "The {VALUE} to decrypt",
                    v => value = v
                },
                {
                    "help",
                    "Show this message and exit.",
                    v => displayHelp = true
                }
            };

            try
            {
                options.Parse(args);
            }
            catch (OptionException ex)
            {
                DisplayErrorMessage(ex.Message);
                return null;
            }

            if (displayHelp)
            {
                DisplayHelp(options);
                return null;
            }

            if (string.IsNullOrWhiteSpace(region))
            {
                DisplayErrorMessage("Must supply an AWS Region");
                return null;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                DisplayErrorMessage("Must supply a value to decrypt");
                return null;
            }

            return new DecryptorArgs
            {
                Region = region,
                Value = value
            };
        }

        private static void DisplayErrorMessage(string message)
        {
            Console.Write("SimpleEncrypt.Decryptor: ");
            Console.WriteLine(message);
            Console.WriteLine("Try `SimpleEncrypt.Decryptor --help' for more information.");
        }

        private static void DisplayHelp(OptionSet p)
        {
            Console.WriteLine("Usage: SimpleEncrypt.Encryptor [OPTIONS]");
            Console.WriteLine("Encrypt a value against an AWS key.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}
