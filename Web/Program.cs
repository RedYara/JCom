using Microsoft.AspNetCore.Authentication.Cookies;
using Persistence;
using Web.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        });
builder.Services.AddApplication();
builder.Services.AddPersistence();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options =>
        {
            options.LoginPath = new PathString("/account/login");
            options.AccessDeniedPath = new PathString("/account/accessdenied");
        });

if (builder.Environment.IsDevelopment())
{
    Environment.SetEnvironmentVariable("POSTGRESQL_CONNECTION_STRING", "Host=localhost;Database=JCom;Username=postgres;Password=W321ewqW;IncludeErrorDetail=true;");
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
