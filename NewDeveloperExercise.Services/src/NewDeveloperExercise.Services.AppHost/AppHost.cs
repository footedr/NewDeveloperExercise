var builder = DistributedApplication.CreateBuilder(args);

var saPassword = builder.AddParameter("sa-password");

var sql = builder.AddSqlServer("sqlserver", password: saPassword, 56789)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithBindMount("./NewDeveloperExercise.sqlserver.data", "/var/opt/mssql/data")
    .AddDatabase("newdeveloperexercise");

builder.AddProject<Projects.NewDeveloperExercise_Services_Web>("newdeveloperexerciseweb")
    .WaitFor(sql);

builder.Build().Run();
