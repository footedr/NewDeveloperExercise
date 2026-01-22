using Microsoft.AspNetCore.Http.Json;
using NewDeveloperExercise.Services.Api.Infrastructure;
using NewDeveloperExercise.Services.Api.Infrastructure.OpenApi;
using NewDeveloperExercise.Services.Api.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Transient);

builder.ConfigureOpenApi();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ConfigureSerializerOptions();
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddProblemDetails();
builder.ConfigureReverseProxy();
builder.AddServiceDefaults();

var app = builder.Build();

app.UseExceptionHandler();
app.UseRouting();
app.MapOpenApiEndpoints();
app.MapDefaultEndpoints();
app.MapReverseProxy();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("api/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithTags("weather");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
