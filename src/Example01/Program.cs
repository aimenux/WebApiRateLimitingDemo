using Example01.Application;
using Example01.Infrastructure;
using Example01.Presentation;
using Example01.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();

app.UseSwaggerDoc();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRateLimiter();

app
    .MapControllers()
    .RequireRateLimiting(builder.Configuration.GetRateLimitingPolicyType().ToString());

await app.RunAsync();
