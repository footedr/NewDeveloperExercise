using Yarp.ReverseProxy.Configuration;

namespace NewDeveloperExercise.Services.Api.Infrastructure;

public static class ReverseProxyExtensions
{
    public static void ConfigureReverseProxy(this IHostApplicationBuilder builder)
    {
        var clientEndpoint = builder.Configuration["ClientEndpoint"];

        if (clientEndpoint is null)
        {
            using var loggerFactory = LoggerFactory.Create(logBuilder => logBuilder.AddConsole());
            var logger = loggerFactory.CreateLogger("ReverseProxyExtensions");
            logger.LogError("ClientEndpoint configuration is missing.");
            return;
        }

        builder.Services.AddReverseProxy()
            .LoadFromMemory(
                [
                    new()
                    {
                        RouteId = "default",
                        ClusterId = "NewDeveloperExerciseClient",
                        Match = new() { Path = "{**catch-all}" }
                    }
                ],
                [
                    new()
                    {
                        ClusterId = "NewDeveloperExerciseClient",
                        Destinations = new Dictionary<string, DestinationConfig>()
                        {
                            ["default"] = new() { Address = clientEndpoint }
                        }
                    }
                ]
            );
    }
}
