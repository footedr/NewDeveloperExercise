using Scalar.AspNetCore;

namespace NewDeveloperExercise.Services.Api.Infrastructure.OpenApi;

public static class OpenApiExtensions
{
    public static void ConfigureOpenApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                var httpContext = context.ApplicationServices.GetRequiredService<IHttpContextAccessor>().HttpContext;
                if (httpContext is null)
                {
                    return Task.CompletedTask;
                }

                document.Servers.Clear();
                document.Servers.Add(new()
                {
                    Url = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}"
                });

                return Task.CompletedTask;
            });

            //options.AddExamplesTransformer();
        });
    }

    public static void MapOpenApiEndpoints(this WebApplication app)
    {
        app.MapOpenApi()
            //.RequireAuthorization()
        ;

        app.MapScalarApiReference(options =>
        {
            options.EnabledClients =
            [
                ScalarClient.Http11
            ];

            options.Theme = ScalarTheme.Mars;
        })
       //.RequireAuthorization()
       ;
    }
}