using Application.ATM;
using Application.ATM.port.In;
using EasyArchitect.OutsideApiControllerBase;
using EasyArchitect.OutsideManaged.AuthExtensions.Models;
using EasyArchitect.OutsideManaged.AuthExtensions.Services;
using EasyArchitect.OutsideManaged.Configuration;
using EasyArchitect.OutsideManaged.JWTAuthMiddlewares;
using EasyArchitectCore.Core;
using Infrastructure.ATM;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllersWithViews()
    .ConfigureApiBehaviorOptions(apiOption =>
    {
        // 關閉驗證失敗時會自動回應 HTTP 400
        apiOption.SuppressModelStateInvalidFilter = true;
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(configre =>
{
    configre.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = new PathString("/Account/Login");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.Cookie.HttpOnly = true;
    options.Events = new CookieAuthenticationEvents()
    {
        OnRedirectToReturnUrl = async (context) =>
        {
            context.HttpContext.Response.Cookies.Delete(UserInfo.LOGIN_USER_INFO);
        }
    };

});

// 註冊 AppSettings Configuration 類型，可在類別中注入 IOptions<AppSettings>
IConfigurationSection appSettingRoot = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingRoot);
builder.Services.AddDbContext<ModelContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OutsideDbContext"), oracleOptions => oracleOptions.UseOracleSQLCompatibility("11"));
});

builder.Services.AddScoped<IUserService, UserService>(x => new UserService(
    new AppSettings()
    {
        Secret = appSettingRoot.GetSection("Secret").Value,
        TimeoutMinutes = Convert.ToInt32(appSettingRoot.GetSection("TimeoutMinutes").Value)
    }, x.GetRequiredService<ModelContext>()));

builder.Services.AddScoped<IUriExtensions, UriExtensions>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginServiceRepository, LoginServiceRepository>();
builder.Services.AddScoped<ATMService>();

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

app.UseRouting();

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthorization();
app.UseJwtAuthenticate();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
