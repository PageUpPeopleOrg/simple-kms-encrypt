using System;
using NDesk.Options;

namespace SimpleEncrypt.Encryptor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var encryptorArgs = EncryptorArgs.Parse(args);
            if (encryptorArgs == null)
            {
                return;
            }

            Console.WriteLine(encryptorArgs.Value.Encrypt(encryptorArgs.KeyId, encryptorArgs.Region));
        }
    }

    public class EncryptorArgs
    {
        public string Region { get; private set; }
        public string KeyId { get; private set; }
        public string Value { get; private set; }

        public static EncryptorArgs Parse(string[] args)
        {
            string region = null;
            string keyId = null;
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
                    "k|keyId=",
                    "The {KEYID} to encrypt the value against",
                    v => keyId = v
                },
                {
                    "v|value=",
                    "The {VALUE} to encrypt",
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

            if (string.IsNullOrWhiteSpace(keyId))
            {
                DisplayErrorMessage("Must supply an AWS Key ID");
                return null;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                DisplayErrorMessage("Must supply a value to encrypt");
                return null;
            }

            return new EncryptorArgs
            {
                Region = region,
                KeyId = keyId,
                Value = value
            };
        }

        private static void DisplayErrorMessage(string message)
        {
            Console.Write("SimpleEncrypt.Encryptor: ");
            Console.WriteLine(message);
            Console.WriteLine("Try `SimpleEncrypt.Encryptor --help' for more information.");
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
