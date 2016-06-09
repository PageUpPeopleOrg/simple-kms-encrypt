using System;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using static System.Console;

namespace SimpleEncrypt.Console
{
    public class Program
    {

        private readonly CommandLineApplication app;

        public Program()
        {
            var configuration = new ConfigurationBuilder()
                                        .AddEnvironmentVariables()
                                        .Build();

            var envAwsKey = configuration["AWS_ACCESS_KEY_ID"];
            var envAwsSecret = configuration["AWS_SECRET_ACCESS_KEY"];
            var envAwsToken = configuration["AWS_SESSION_TOKEN"];
            
            app = new CommandLineApplication {Description = "Simple encryption console using kms"};

            app.Command("encrypt", c =>
            {

                var keyOption = c.Option("--key -k <key>", "aws kms key", CommandOptionType.SingleValue);
                var secretOption = c.Option("--secret -s <secret>", "secret to be encrypted", CommandOptionType.SingleValue);
                var regionOption = c.Option("--region -r <region>", "aws region where key is located", CommandOptionType.SingleValue);
                
                c.OnExecute(async () =>
                {
                    WriteLine($"Received encrypt command with key: {keyOption.Value()}, secret: {secretOption.Value()}, region: {regionOption.Value()}");

                    await secretOption.Value().EncryptAsync(keyOption.Value(), regionOption.Value(), envAwsKey, envAwsSecret, envAwsToken);
                    return 0;
                });
            });

            app.Command("decrypt", c =>
            {
                var secretOption = c.Option("--secret -s <secret>", "secret to be encrypted", CommandOptionType.SingleValue);
                var regionOption = c.Option("--region -r <region>", "aws region where key is located", CommandOptionType.SingleValue);

                c.OnExecute(async () =>
                {
                    WriteLine($"Received decrypt command with secret: {secretOption.Value()}, region: {regionOption.Value()}");

                    await secretOption.Value().DecryptAsync(regionOption.Value(), envAwsKey, envAwsSecret, envAwsToken);
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
}
