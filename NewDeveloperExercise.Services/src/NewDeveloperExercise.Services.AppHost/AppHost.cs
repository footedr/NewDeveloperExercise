var builder = DistributedApplication.CreateBuilder(args);

var saPassword = builder.AddParameter("sa-password");

var sql = builder.AddSqlServer("sqlserver", password: saPassword, 56789)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithBindMount("./NewDeveloperExercise.sqlserver.data", "/var/opt/mssql/data")
    .AddDatabase("newdeveloperexercise");

var client = builder.AddJavaScriptApp("newdeveloperexercise-client", "../../../NewDeveloperExercise.Client")
    .WithNpm()
    .WithHttpEndpoint(5250, isProxied: false)
    .WithHttpHealthCheck()
    .WithUrls(context => context.Urls.Clear());

var endpoint = client.GetEndpoint("http");

var web = builder.AddProject<Projects.NewDeveloperExercise_Services_Api>("newdeveloperexercise-services-api")
    .WithEnvironment("ClientEndpoint", endpoint).WaitFor(client)
    .WaitFor(sql)
    .WithUrls(context =>
    {
        var defaultUrl = context.Urls.First();
        context.Urls.Clear();
        context.Urls.Add(new() { Url = defaultUrl.Url, DisplayText = "UI" });
        context.Urls.Add(new() { Url = $"{defaultUrl.Url}/scalar/v1", DisplayText = "API" });
    })
    .WithChildRelationship(client);

builder.AddProject<Projects.NewDeveloperExercise_Services_Web>("newdeveloperexerciseweb")
    .WaitFor(sql);

builder.Build().Run();
