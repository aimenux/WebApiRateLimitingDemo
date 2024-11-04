using Example02.Application;
using Example02.Infrastructure;
using Example02.Presentation;
using Example02.Presentation.Extensions;

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

app.MapEndpoints(requireRateLimiting: true);

await app.RunAsync();