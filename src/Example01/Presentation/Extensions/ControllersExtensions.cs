namespace Example01.Presentation.Extensions;

public static class ControllersExtensions
{
    public static void MapControllers(this WebApplication app, bool requireRateLimiting)
    {
        var builder = app.MapControllers();
        if (requireRateLimiting)
        {
            builder.RequireRateLimiting(app.Configuration);
        }
    }
}