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

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(requireRateLimiting: true);

await app.RunAsync();
