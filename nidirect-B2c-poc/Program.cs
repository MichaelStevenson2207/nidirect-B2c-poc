using System;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace nidirect_B2c_poc
{
    public static class Program
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