using QueflityMVC.Application;
using QueflityMVC.Infrastructure;
using QueflityMVC.Web.Setup.Database;
using QueflityMVC.Web.Setup.Identity;
using QueflityMVC.Web.Setup.OAuth;
using QueflityMVC.Web.Setup.Other;
using QueflityMVC.Web.Setup.Secrets;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
// provider for secrets, connection string etc.
IVariablesProvider variablesProvider = new EnviromentCredentialsProvider();

// Add logging
SerilogSetup.SetupLogger();
builder.Host.UseSerilog(Log.Logger);

// Add services to the container.
builder.Services.ConfigureDbContext<Context>(variablesProvider);
builder.Services.ConfigureIdentity();

builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddControllersWithViews();

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.ApplyPendingMigrations<Context>();

app.Run();