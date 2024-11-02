namespace Example02.Presentation.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwaggerDoc(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsProduction())
        {
            return;
        }
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    public static void UseSwaggerDoc(this WebApplication app)
    {
        if (app.Environment.IsProduction())
        {
            return;
        }
        
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DisplayRequestDuration();
        });
    }
}