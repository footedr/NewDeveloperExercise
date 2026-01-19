using Mediator;
using NewDeveloperExercise.Services.Core.AddressBook;
using NewDeveloperExercise.Services.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediator(opt =>
{
	opt.ServiceLifetime = ServiceLifetime.Transient;
	opt.Assemblies = new List<AssemblyReference>()
	{
		typeof(AddressBookEntry).Assembly,
		typeof(NewDeveloperExerciseContext).Assembly
	};
});

builder.ConfigureDatabase();

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

await app.InitializeDatabase();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
