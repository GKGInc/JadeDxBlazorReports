
//// -- =========================================================================

//var builder = WebApplication.CreateBuilder(args);
//// Add services to the container.

//builder.Services.AddRazorPages();
//builder.Services.AddServerSideBlazor();
//builder.Services.AddDevExpressBlazor();
//builder.Services.AddDevExpressServerSideBlazorReportViewer();
//builder.Services.Configure<DevExpress.Blazor.Configuration.GlobalOptions>(options => {
//    options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
//});
//builder.WebHost.UseStaticWebAssets();

//var app = builder.Build();


//// Configure the HTTP request pipeline.
//if(app.Environment.IsDevelopment()) {
//    app.UseDeveloperExceptionPage();
//} else {
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();

//app.MapControllers();
//app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");

//string contentPath = app.Environment.ContentRootPath;
//AppDomain.CurrentDomain.SetData("DataDirectory", contentPath);
//AppDomain.CurrentDomain.SetData("DXResourceDirectory", contentPath);

//app.Run();

//// -- =========================================================================

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Net;

//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Web;
//using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.HttpsPolicy;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.FileProviders;
//using Microsoft.AspNetCore.Routing;

//using DevExpress.AspNetCore;
//using DevExpress.DashboardAspNetCore;
//using DevExpress.DashboardWeb;
//using DevExpress.DashboardCommon;
//using DevExpress.Xpo;
//using DevExpress.Xpo.DB;
//using DevExpress.XtraCharts;
//using DevExpress.DataAccess.Json;
//using DevExpress.Blazor.Reporting;

//using DxBlazorReport.Authentication;
//using DxBlazorReport.Areas.Identity;
//using DxBlazorReport.Data;
//using DxBlazorReport.Code;
//using DxBlazorReport.Models;
//using JadeBlazorDashboard;

// -- =========================================================================

using DevExpress.AspNetCore;
using DevExpress.CodeParser;
using DevExpress.DashboardAspNetCore;
using DevExpress.DashboardWeb;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.XtraCharts;
using DxBlazorReport.Authentication;
using DxBlazorReport.Code;
using DxBlazorReport.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

IFileProvider fileProvider = builder.Environment.ContentRootFileProvider;
IConfiguration configuration = builder.Configuration;

// -- =========================================================================

////// Logging: using Microsoft.AspNetCore.Identity custom
////services.AddIdentity<IdentityUser, IdentityRole>(options =>
////{
////    options.Password.RequireDigit = false;
////    options.Password.RequiredLength = 5;
////    options.Password.RequireLowercase = false;
////    options.Password.RequireUppercase = false;
////    options.Password.RequireNonAlphanumeric = false;
////    options.SignIn.RequireConfirmedEmail = false;
////})
////    .AddRoles<IdentityRole>()
////    .AddEntityFrameworkStores<DataContext>();

//// Logging: using custom
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie();
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdministratorOnly", policy => policy.RequireRole("Administrator"));
//});
//// For CustomAuthenticationStateProvider process
//builder.Services.AddAuthenticationCore();

// -- =========================================================================

// Add services to the container.
//DevExpress Reporting
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDevExpressBlazor();
builder.Services.AddDevExpressBlazor(options =>
{
    options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
    options.SizeMode = DevExpress.Blazor.SizeMode.Medium;
});
//builder.Services.AddDevExpressBlazorReporting();
builder.Services.AddDevExpressServerSideBlazorReportViewer();
// If you use Bootstrap 5, specify the Bootstrap version explicitly
builder.Services.Configure<DevExpress.Blazor.Configuration.GlobalOptions>(options => {
    options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDbContextFactory<NorthwindContext>((sp, options) =>
//{
//    var env = sp.GetRequiredService<IWebHostEnvironment>();
//    var dbPath = Path.Combine(env.ContentRootPath, "Northwind.mdf");
//    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=" + dbPath);
//});

builder.Services.AddDbContextFactory<DbContext>((sp, options) =>
{
    var sqlDefaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(sqlDefaultConnection);
    //var sqlLAGemConnection = builder.Configuration.GetConnectionString("LAGemConnection");
    //options.UseSqlServer(sqlLAGemConnection);
    var sqlJadeConnection = builder.Configuration.GetConnectionString("JadeConnection");
    options.UseSqlServer(sqlJadeConnection);
});

// -- =========================================================================

//// For CustomAuthenticationStateProvider process
//builder.Services.AddAuthenticationCore();

// For CustomAuthenticationStateProvider process
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddSingleton<UserAccountService>();

//// Logging: using Microsoft.AspNetCore.Identity custom
//builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

//// Logging: using custom
//builder.Services.AddSingleton<CustomAuthService>();

//// Logging: using custom
//builder.Services.AddSingleton<CustomAuthService>();
//builder.Services.AddSingleton<WeatherForecastService>();

//DevExpress Dashboard
builder.Services.AddDevExpressControls();

// from https://github.com/DevExpress-Examples/DashboardDifferentUserDataAspNetCore/blob/23.2.2%2B/CS/Startup.cs
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<DashboardConfigurator>((IServiceProvider serviceProvider) =>
{
    return DashboardUtils.CreateDashboardConfigurator(configuration, fileProvider);
});

builder.Services.AddSingleton<DashboardConfigurator, MultiTenantDashboardConfigurator>(); // WORKS!!!

// -- =========================================================================

// initialize XPO DAL
var store = XpoDefault.GetConnectionProvider(ConnectionHelper.ConnectionString, AutoCreateOption.SchemaAlreadyExists);
//var store = XpoDefault.GetConnectionProvider(sqlConnection, AutoCreateOption.SchemaAlreadyExists);

// Initialize the XPO dictionary.
DevExpress.Xpo.Metadata.XPDictionary dict = new DevExpress.Xpo.Metadata.ReflectionDictionary();
dict.GetDataStoreSchema(ConnectionHelper.GetPersistentTypes());

DevExpress.Xpo.XpoDefault.DataLayer = new DevExpress.Xpo.ThreadSafeDataLayer(dict, store);
DevExpress.Xpo.XpoDefault.Session = null;

// -- =========================================================================

DevExpress.Utils.DeserializationSettings.RegisterTrustedClass(typeof(DataTableToObjectConverter));

builder.Services.AddScoped<GlobalData>();

builder.WebHost.UseWebRoot("wwwroot");
builder.WebHost.UseStaticWebAssets();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    //app.UseDatabaseErrorPage();
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseSession(); // from https://github.com/DevExpress-Examples/DashboardDifferentUserDataAspNetCore/blob/23.2.2%2B/CS/Startup.cs
app.UseAuthorization();

//app.MapControllers();
//app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");
//app.MapDashboardRoute("api/dashboard", "DefaultDashboard");

app.UseEndpoints(endpoints =>
{
    //DevExpress Dashboard
    //EndpointRouteBuilderExtension.MapDashboardRoute(endpoints, "api/dashboard", "DefaultDashboard"); 
    //'EndpointRouteBuilderExtension.MapDashboardRoute(IEndpointRouteBuilder, string, string)' is obsolete: 'Call the IEndpointRouteBuilder.MapDashboardRoute method instead.'	
    endpoints.MapDashboardRoute("api/dashboard", "DefaultDashboard");
    endpoints.MapControllers();
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});

string contentPath = app.Environment.ContentRootPath;
AppDomain.CurrentDomain.SetData("DataDirectory", contentPath);
AppDomain.CurrentDomain.SetData("DXResourceDirectory", contentPath);

app.Run();

// -- =========================================================================
