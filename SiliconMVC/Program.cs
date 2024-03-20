

using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Helpers.Middlewares;
using Infrastructure.Repositories;
using Infrastructure.Secretes;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<FeatureItemRepository>();
builder.Services.AddScoped<FeatureRepository>();


builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<FeatureService>();

builder.Services.AddDefaultIdentity<UserEntity>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 8;

}).AddEntityFrameworkStores<DataContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/signin";
    options.LogoutPath = "/signout";
    options.AccessDeniedPath = "/accessdenied";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
});


//builder.Services.AddAuthentication().AddFacebook(x =>
//{
//    x.AppId = "1199558597850441";
//    x.AppSecret = "6f9d5152da480538f78747ecad646629";
//    x.Fields.Add("first_name");
//    x.Fields.Add("last_name");
//});

var secretes = new ThirdPartySecretes();

builder.Services.AddAuthentication()
    .AddFacebook(x =>
{
    x.AppId = secretes.FacebookAppId;
    x.AppSecret = secretes.FacebookAppSecret;
    x.Fields.Add("first_name");
    x.Fields.Add("last_name");
})
    .AddGoogle(x =>
    {
        x.ClientId = secretes.GoogleClientId;
        x.ClientSecret = secretes.GoogleClientSecret;
    });

var app = builder.Build();



app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseUserSessionValidator();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
