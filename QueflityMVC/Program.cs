using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure;
using QueflityMVC.Web.Common;
using QueflityMVC.Web.Integrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<QueflityMVC.Infrastructure.Context>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<QueflityMVC.Infrastructure.Context>()
    .AddSignInManager<MySignInManager>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequiredUniqueChars = 0;
    options.SignIn.RequireConfirmedEmail = false;
    options.User.RequireUniqueEmail = false;
});


builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication()
    .AddGoogle(options => options.Setup(new AppSecretsCredentialProvider(builder.Configuration)));
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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

app.Run();
