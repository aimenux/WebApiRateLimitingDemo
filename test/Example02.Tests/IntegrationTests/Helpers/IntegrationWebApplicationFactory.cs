using Example02.Presentation.Endpoints;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Example02.Tests.IntegrationTests.Helpers;

public class IntegrationWebApplicationFactory : WebApplicationFactory<BaseEndpoint>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        
        builder.ConfigureLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddDebug().AddConsole();
        });
        
        builder.ConfigureAppConfiguration((_, config) =>
        {
            var settings = new Dictionary<string, string?>
            {
                { "RateLimiting:PolicyName", "Fixed" }
            };
            config.AddInMemoryCollection(settings);
        });

        builder.ConfigureTestServices(services =>
        {
        });
    }
}