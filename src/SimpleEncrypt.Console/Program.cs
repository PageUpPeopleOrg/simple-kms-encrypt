using System;
using Microsoft.Extensions.CommandLineUtils;
using static System.Console;

namespace SimpleEncrypt.Console
{
    public class Program
    {

        private readonly CommandLineApplication app = null;

        public Program()
        {
            CommandArgument key, secret, region, awskey, awsSecret, awsToken;
            
            app = new CommandLineApplication {Description = "Simple encryption console using kms"};

            app.Command("encrypt", c =>
            {
                key = c.Argument("key", "aws kms key");
                secret = c.Argument("value", "value to be encrypted");
                region = c.Argument("region", "region where aws kms key is located");
                awskey = c.Argument("awskey", "aws access key");
                awsSecret = c.Argument("awsSecret", "aws access secret");
                awsToken = c.Argument("awsToken", "aws access session token");

                c.OnExecute(async () =>
                {
                    await secret.Value.EncryptAsync(key.Value, region.Value, awskey.Value, awsSecret.Value, awsToken.Value);
                    return 0;
                });
            });

            app.Command("decrypt", c =>
            {
                secret = c.Argument("value", "value to be encrypted");
                region = c.Argument("region", "region where aws kms key is located");
                awskey = c.Argument("awskey", "aws access key");
                awsSecret = c.Argument("awsSecret", "aws access secret");
                awsToken = c.Argument("awsToken", "aws access session token");
                

                c.OnExecute(async () =>
                {
                    await secret.Value.DecryptAsync(region.Value, awskey.Value, awsSecret.Value, awsToken.Value);
                    return 0;
                });
            });
        }

        public int Execute(string[] args)
        {
            return app.Execute(args);
        }

        public void ShowHelp()
        {
            app.ShowHelp();
        }

        public static void Main(string[] args)
        {
            var program = new Program();
            try
            {
                program.Execute(args);
            }
            catch (Exception exception)
            {
                WriteLine(exception.Message);
                program.ShowHelp();
            }
        }

//        private static void DecryptAsync(DecryptOptions opts)
//        {
//            WriteLine(opts.Value.DecryptAsync(opts.Region));
//        }
//
//        private static void EncryptAsync(EncryptOptions opts)
//        {
//            WriteLine(opts.Value.EncryptAsync(opts.KeyId, opts.Region));
//        }
    }

//    internal class Options
//    {
//        [Option('r', "region", HelpText = "The {AWSREGION} that contains the key", Required = true)]
//        public string Region { get; set; }
//    }
//
//    [Verb("encrypt", HelpText = "encrypts a value using the key")]
//    internal class EncryptOptions: Options
//    {
//        [Option('k', "keyId", HelpText = "The {KEYID} to encrypt the value against", Required = true)]
//        public string KeyId { get; set; }
//
//        [Option('v', "value", HelpText = "The {VALUE} to encrypt", Required = true)]
//        public string Value { get; set; }
//    }
//
//    [Verb("decrypt", HelpText = "decrypts value")]
//    internal class DecryptOptions: Options
//    {
//        [Option('v', "value", HelpText = "The {VALUE} to encrypt", Required = true)]
//        public string Value { get; set; }
//    }
}
