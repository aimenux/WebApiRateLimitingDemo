using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using Serilog.Debugging;

namespace Example01.Presentation.Extensions;

public static class LoggingExtensions
{
    public static void AddLogging(this WebApplicationBuilder builder)
    {
        builder.AddHttpLogging();
        builder.AddSerilog();
    }
    
    private static void AddHttpLogging(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All;
            logging.CombineLogs = true;
        });
    }
    
    private static void AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        
        builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            SelfLog.Enable(Console.Error);

            loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext();
        });
    }
}