using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SaleWebsite;
using SaleWebsite.Hubs;
using SaleWebsite.Interfaces;
using SaleWebsite.PasswordHasher;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});


builder.Services.AddDbContext<DataContext>(opt =>
    {
        /*
        
        var DbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
        var DbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "SaleWebsiteDB";
        var DbPassword = Environment.GetEnvironmentVariable("SA_PASSWORD") ?? "Vusal@12345";
        var ConStr = $"Server={DbHost}, 1433;Database={DbName};User ID=SA;Password={DbPassword};TrustServerCertificate=True";
        
        opt.UseSqlServer(ConStr);
        */
        //opt.UseSqlServer(builder.Configuration.GetConnectionString("SaleWebsiteDB"));

        opt.UseSqlite(builder.Configuration.GetConnectionString("SaleWebsiteDB"));
    });

//builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

//app.UseAuthentication();

app.UseAuthorization();


app.UseRouting();

app.MapHub<ChatHub>("/ChatHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
