using Example01.Presentation.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;

namespace Example01.Tests.IntegrationTests.Helpers;

public class IntegrationWebApplicationFactory : WebApplicationFactory<BaseController>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddDebug().AddConsole();
        });
        
        builder.ConfigureAppConfiguration((_, _) =>
        {
        });

        builder.ConfigureTestServices(services =>
        {
        });
    }
}