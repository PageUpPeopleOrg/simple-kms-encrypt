using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using static System.Console;

namespace SimpleEncrypt.Console
{
    public class Program
    {

        private readonly CommandLineApplication app = null;

        public Program()
        {
            var configuration = new ConfigurationBuilder()
                                        .AddEnvironmentVariables()
                                        .Build();

            var envAwsKey = configuration["AWS_ACCESS_KEY_ID"];
            var envAwsSecret = configuration["AWS_SECRET_ACCESS_KEY"];
            var envAwsToken = configuration["AWS_SESSION_TOKEN"];

            var optionKey = new CommandOption("--key -k <key>", CommandOptionType.SingleValue)
            {
                Description = "aws kms key"
            };

            var optionsecret = new CommandOption("--secret -s <secret>", CommandOptionType.SingleValue)
            {
                Description = "secret to be encrypted",
            
            };

            var optionRegion = new CommandOption("--region -r <region>", CommandOptionType.SingleValue)
            {
                Description = "aws region where key is located"
            };

            CommandArgument key, secret, region;
            
            app = new CommandLineApplication {Description = "Simple encryption console using kms"};

            app.Command("encrypt", c =>
            {

                key = c.Argument("key", "aws kms key");
                secret = c.Argument("secret", "value to be encrypted");
                region = c.Argument("region", "region where aws kms key is located");
                
                c.OnExecute(async () =>
                {
                    WriteLine($"Received encrypt command with key: {key.Value}, secret: {secret.Value}, region: {region.Value}");

                    await secret.Value.EncryptAsync(key.Value, region.Value, envAwsKey, envAwsSecret, envAwsToken);
                    return 0;
                });
            });

            app.Command("decrypt", c =>
            {
                secret = c.Argument("secret", "value to be encrypted");
                region = c.Argument("region", "region where aws kms key is located");

                c.OnExecute(async () =>
                {
                    WriteLine($"Received decrypt command with secret: {secret.Value}, region: {region.Value}");

                    await secret.Value.DecryptAsync(region.Value, envAwsKey, envAwsSecret, envAwsToken);
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
