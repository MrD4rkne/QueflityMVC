using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application;
using QueflityMVC.Infrastructure;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<QueflityMVC.Infrastructure.Context>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<QueflityMVC.Infrastructure.Context>();
builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase= true;
    options.Password.RequiredUniqueChars = 0;

    options.SignIn.RequireConfirmedEmail = false;
    options.User.RequireUniqueEmail = false;
});

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    IConfigurationSection googleOAuthSection = builder.Configuration.GetSection("Authentication:Google");
    googleOptions.ClientId = googleOAuthSection["ClientId"];
    googleOptions.ClientSecret = googleOAuthSection["ClientSecret"];
});

builder.Services.AddControllersWithViews();

builder.Services.AddInfrastructure();

builder.Services.AddApplication();


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
