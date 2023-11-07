using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        var host = new HostBuilder()
        .ConfigureAppConfiguration(builder =>
        {
            builder.AddAzureAppConfiguration(options =>
            {
                string tenant = Environment.GetEnvironmentVariable("tenant");
                string clientId = Environment.GetEnvironmentVariable("clientId");
                string secret = Environment.GetEnvironmentVariable("secret");

                options.Connect(new Uri(Environment.GetEnvironmentVariable("Endpoint")), new ClientSecretCredential(tenant, clientId, secret)) // ManagedIdentityCredential())
                        // Load all keys that start with `TestApp:` and have no label
                        .Select("TestApp:*")
                        // Configure to reload configuration if the registered sentinel key is modified
                        .ConfigureRefresh(refreshOptions =>
                            refreshOptions.Register("TestApp:Settings:Sentinel", refreshAll: true).SetCacheExpiration(TimeSpan.FromSeconds(1)));
            });
        })
        .ConfigureServices(services =>
        {
            // Make Azure App Configuration services available through dependency injection.
            services.AddAzureAppConfiguration();
        })
        .ConfigureFunctionsWorkerDefaults(app =>
        {
            // Use Azure App Configuration middleware for data refresh.
            app.UseAzureAppConfiguration();
        })
        .Build();

        host.Run();
    }
}