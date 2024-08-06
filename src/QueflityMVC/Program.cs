using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using QueflityMVC.Application;
using QueflityMVC.Application.Constants;
using QueflityMVC.Infrastructure;
using QueflityMVC.Infrastructure.Emails;
using QueflityMVC.Persistence;
using QueflityMVC.Persistence.Setup;
using QueflityMVC.Web.Chat;
using QueflityMVC.Web.Common;
using QueflityMVC.Web.Setup;
using QueflityMVC.Web.Setup.Identity;
using QueflityMVC.Web.Setup.Other;
using Serilog;
using JobsOptionsValidator = QueflityMVC.Web.Setup.JobsOptionsValidator;
using SmtpOptionsValidator = QueflityMVC.Web.Setup.SmtpOptionsValidator;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options => options.Limits.MaxRequestHeadersTotalSize = 1048576);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true)
    .AddEnvironmentVariables();
var config = builder.Configuration;

// Add logging
SerilogSetup.SetupLogger();
builder.Host.UseSerilog(Log.Logger);

builder.Services.AddCommonServices();

builder.Services
    .Configure<SmtpOptions>(config.GetSection(SmtpOptions.SECTION_NAME));
builder.Services.AddSingleton<IValidateOptions<SmtpOptions>, SmtpOptionsValidator>();

builder.Services
    .Configure<JobsOptions>(config.GetSection(JobsOptions.SECTION_NAME));
builder.Services.AddSingleton<IValidateOptions<JobsOptions>, JobsOptionsValidator>();

builder.Services.AddTransient<IConfigureOptions<SmtpConfig>, ConfigureSmtp>();
builder.Services.AddTransient<IConfigureOptions<JobsConfig>, ConfigureJobs>();
builder.Services.AddInfrastructure();

builder.Services
    .Configure<DatabaseOptions>(config.GetSection(DatabaseOptions.SECTION_NAME));
builder.Services.TryAddEnumerable(
    ServiceDescriptor.Singleton
        <IValidateOptions<DatabaseOptions>, DatabaseOptionsValidator>());

builder.Services.AddTransient<IConfigureOptions<PersistenceConfig>, ConfigurePersistence>();
builder.AddPersistence();

builder.Services.AddApplication();
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.AddAuthenticationWithOAuths();
builder.Services.AddAuthorization(options =>
    options.AddPolicies());

builder.Services.ConfigureIdentity();

builder.Services.AddSignalR();

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

app.MapHub<MessageHub>("/messageHub", options =>
{
    options.Transports = HttpTransportType.LongPolling | HttpTransportType.ServerSentEvents;
    options.CloseOnAuthenticationExpiration = true;
});

app.MapRazorPages();

if (app.Environment.IsDevelopment()) app.ApplyPendingMigrations();

await app.Services.SeedIdentity(Claims.GetAll().ToArray());

app.Run();