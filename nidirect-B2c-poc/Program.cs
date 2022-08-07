using System;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace nidirect_B2c_poc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, builder) =>
            {
                if (hostingContext.HostingEnvironment.IsProduction())
                {
                    var builtConfig = builder.Build();

                    // Setup token provider
                    //var azureServiceTokenProvider = new AzureServiceTokenProvider();

                    //// Get auth info to get credentials from keyvault
                    //var keyVaultClient = new KeyVaultClient(
                    //new KeyVaultClient.AuthenticationCallback(
                    //    azureServiceTokenProvider.KeyVaultTokenCallback));

                    //builder.AddAzureKeyVault($"https://{builtConfig["keyVaultName"]}.vault.azure.net/",
                    //    keyVaultClient, new DefaultKeyVaultSecretManager());

                    builder.AddAzureKeyVault(
                        new Uri($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/"),
                        new DefaultAzureCredential());

                }
            }).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
        }
    }
}