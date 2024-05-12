using QueflityMVC.Application;
using QueflityMVC.Infrastructure;
using QueflityMVC.Persistence;
using QueflityMVC.Web.Setup.Database;
using QueflityMVC.Web.Setup.Identity;
using QueflityMVC.Web.Setup.OAuth;
using QueflityMVC.Web.Setup.Other;
using QueflityMVC.Web.Setup.Secrets;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options => options.Limits.MaxRequestHeadersTotalSize = 1048576);

// provider for secrets, connection string etc.
IVariablesProvider variablesProvider = new EnvironmentCredentialsProvider();

// Add logging
SerilogSetup.SetupLogger();
builder.Host.UseSerilog(Log.Logger);

// Add services to the container.
builder.Services.ConfigureDbContext<Context>(variablesProvider);
builder.Services.ConfigureIdentity();

builder.Services.AddInfrastructure(variablesProvider.GetConnectionString());
builder.Services.AddPersistence();
builder.Services.AddApplication();
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddAuthentication()
    .AddOAuths(variablesProvider);
builder.Services.AddAuthorization(options =>
    options.AddPolicies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.ApplyPendingMigrations<Context>();
await app.Services.SeedIdentity();

app.Run();