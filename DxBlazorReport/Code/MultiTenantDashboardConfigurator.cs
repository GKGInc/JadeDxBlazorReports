//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using DxBlazorReport.Authentication;
using DxBlazorReport.Data;
using DxBlazorReport.Model;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.Data.Filtering;
using DevExpress.DataAccess;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using DxBlazorReport.Models;

namespace DxBlazorReport.Code
{
    public class MultiTenantDashboardConfigurator : DashboardConfigurator
    {
        // ----------------------------------------------------------------------------------

        #region Variables

        private readonly IHttpContextAccessor contextAccessor;
        private IFileProvider fileProvider { get; }
        public IConfiguration Configuration { get; }

        #endregion

        // ----------------------------------------------------------------------------------

        #region Public Functions

        public MultiTenantDashboardConfigurator(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            this.contextAccessor = contextAccessor;
            this.fileProvider = hostingEnvironment.ContentRootFileProvider;
            Configuration = configuration;

            //string user = contextAccessor.HttpContext.Request.Headers["User"];
            //string promoCode = contextAccessor.HttpContext.Request.Headers["PromoCode"];

            //SetDashboardStorage(new DashboardFileStorage(fileProvider.GetFileInfo("App_Data/Dashboards").PhysicalPath));
            //SetDataSourceStorage(new CustomDataSourceStorage());

            //////"Data/Dashboards"
            //////"\\gkgfp\shares\CustData\CTC\Dashboards"
            //string physicalPath = fileProvider.GetFileInfo(@"Data/Dashboards").PhysicalPath; 
            //////if (!Directory.Exists(physicalPath)) // If not developing, get path from appsettings.json "DashboardsLocation"
            //////{
            ////    ////physicalPath = @"\\gkgfp\shares\CustData\CTC\Dashboards";
            ////    string physicalPath = Configuration.GetConnectionString("DashboardsLocation").ToString(); // works from local
            //////}

            string physicalPath = fileProvider.GetFileInfo(@"Dashboards").PhysicalPath;

            //DashboardFileStorage dashboardFileStorage = new DashboardFileStorage(fileProvider.GetFileInfo(@"\\gkgfp\shares\CustData\CTC\Dashboards").PhysicalPath);
            DashboardFileStorage dashboardFileStorage = new DashboardFileStorage(physicalPath);
            SetDashboardStorage(dashboardFileStorage);

            DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();
            DashboardSqlDataSource sqlDataSource = new DashboardSqlDataSource("SQL Data Source", "JadeConnection");
            //DashboardSqlDataSource sqlDataSource = new DashboardSqlDataSource("SQL Data Source", "DefaultConnection");
            ////sqlDataSource.DataProcessingMode = DataProcessingMode.Client;
            sqlDataSource.DataProcessingMode = DataProcessingMode.Server;
            dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml());
            //SetDataSourceStorage(dataSourceStorage);

            DashboardObjectDataSource objDataSource1 = new DashboardObjectDataSource("DepartmentQueueProcessesView_all", typeof(DepartmentQueueProcessesView));
            dataSourceStorage.RegisterDataSource("ediAttendantDataObjectDataSource", objDataSource1.SaveToXml());
            DashboardObjectDataSource objDataSource2 = new DashboardObjectDataSource("DepartmentQueueProcessesView_thisyear", typeof(DepartmentQueueProcessesView));
            dataSourceStorage.RegisterDataSource("ediAttendantDataObjectDataSource", objDataSource2.SaveToXml());

            ConfigureDataConnection += DashboardConfigurator_ConfigureDataConnection;
            DataLoading += DashboardConfigurator_DataLoading;
            CustomParameters += DashboardConfigurator_CustomParameters;
            CustomFilterExpression += DashboardConfigurator_CustomFilterExpression;
        }

        #endregion

        // ----------------------------------------------------------------------------------

        #region Private Functions

        // Configure user-specific data caching
        private void DashboardConfigurator_CustomParameters(object sender, CustomParametersWebEventArgs e)
        {
            ////var userId = contextAccessor.HttpContext.Session.GetString("CurrentUser").GetHashCode();
            ////e.Parameters.Add(new Parameter("UserId", typeof(string), userId));

            ////string user = contextAccessor.HttpContext.Session.GetString("User");  // didn't work
            ////string promoCode = contextAccessor.HttpContext.Session.GetString("PromoCode"); // didn't work

            //string user = contextAccessor.HttpContext.Request.Headers["User"];
            //string promoCode = contextAccessor.HttpContext.Request.Headers["PromoCode"];

            //e.Parameters.Add(new Parameter("@promoCode", typeof(string), promoCode));
        }

        // Conditional data loading for ObjectDataSource
        private async void DashboardConfigurator_DataLoading(object sender, DataLoadingWebEventArgs e)
        {
            //string user = contextAccessor.HttpContext.Request.Headers["User"];
            //string promoCode = contextAccessor.HttpContext.Request.Headers["PromoCode"];
            //SqlData sqlData = new SqlData();
            //e.Data = await sqlData.GetDashboardData(e.DashboardId, user, promoCode, e.DataSourceName, e.DataSourceComponentName);

            SqlData sqlData = new SqlData();
            e.Data = await sqlData.GetDashboardData(e.DashboardId);
        }

        // Conditional data loading for other datasource types
        private void DashboardConfigurator_ConfigureDataConnection(object sender, ConfigureDataConnectionWebEventArgs e)
        {
            return;

            //var userName = contextAccessor.HttpContext.Session.GetString("CurrentUser");
            //if (userName == null)
            //    userName = contextAccessor.HttpContext.Request.Headers["User"];

            //if (e.DataSourceName == "CTCsp_GetEdiAttendantData" ||
            //    e.DataSourceName == "CTCsp_GetEditAttendantData") // Misspelled
            //{
            //    string promoCode = contextAccessor.HttpContext.Request.Headers["PromoCode"];
            //    var sqlConnectionParameters = e.ConnectionParameters as CustomStringConnectionParameters;

            //    var xmlFileName = e.DashboardId + ".xml";
            //    var fileUri = new Uri(fileProvider.GetFileInfo("Data/Dashboards/").PhysicalPath + xmlFileName, UriKind.RelativeOrAbsolute);
            //}

            //if (e.ConnectionName == "sqlCategories")
            //{
            //    var sqlConnectionParameters = e.ConnectionParameters as CustomStringConnectionParameters;
            //    if (userName == "Admin")
            //    {
            //        sqlConnectionParameters.ConnectionString = @"XpoProvider=SQLite; Data Source=App_Data/nwind_admin.db;";
            //    }
            //    else if (userName == "User")
            //    {
            //        sqlConnectionParameters.ConnectionString = @"XpoProvider=SQLite; Data Source=App_Data/nwind_user.db;";
            //    }
            //}
            //else if (e.ConnectionName == "jsonCustomers")
            //{
            //    if (e.DashboardId == "JSON")
            //    {
            //        string jsonFileName = "";
            //        if (userName == "Admin")
            //        {
            //            jsonFileName = "customers_admin.json";
            //        }
            //        else if (userName == "User")
            //        {
            //            jsonFileName = "customers_user.json";
            //        }
            //        var fileUri = new Uri(fileProvider.GetFileInfo("App_Data/").PhysicalPath + jsonFileName, UriKind.RelativeOrAbsolute);
            //        ((JsonSourceConnectionParameters)e.ConnectionParameters).JsonSource = new UriJsonSource(fileUri);
            //    }
            //    else if (e.DashboardId == "JSONFilter")
            //    {
            //        var remoteUri = new Uri(GetBaseUrl() + "Home/GetCustomers");
            //        var jsonSource = new UriJsonSource(remoteUri);
            //        if (userName == "User")
            //        {
            //            jsonSource.QueryParameters.AddRange(new[] {
            //                // "CountryPattern" is a dashboard parameter whose value is used for the "CountryStartsWith" query parameter
            //                new QueryParameter("CountryStartsWith", typeof(Expression), new Expression("Parameters.CountryPattern"))
            //            });
            //        }
            //        else if (userName != "Admin")
            //        {
            //            throw new ApplicationException("You are not authorized to access JSON data.");
            //        }
            //      ((JsonSourceConnectionParameters)e.ConnectionParameters).JsonSource = jsonSource;
            //    }
            //}
            //else if (e.ConnectionName == "excelSales")
            //{
            //    var excelConnectionParameters = e.ConnectionParameters as ExcelDataSourceConnectionParameters;
            //    if (userName == "Admin")
            //    {
            //        excelConnectionParameters.FileName = fileProvider.GetFileInfo("App_Data/sales_admin.xlsx").PhysicalPath;
            //    }
            //    else if (userName == "User")
            //    {
            //        excelConnectionParameters.FileName = fileProvider.GetFileInfo("App_Data/sales_user.xlsx").PhysicalPath;
            //    }
            //}
            //else if (e.ConnectionName == "extractSalesPerson")
            //{
            //    if (userName == "Admin")
            //    {
            //        ((ExtractDataSourceConnectionParameters)e.ConnectionParameters).FileName = fileProvider.GetFileInfo("App_Data/SalesPersonExtract.dat").PhysicalPath;
            //    }
            //    else
            //    {
            //        throw new ApplicationException("You are not authorized to access Extract data.");
            //    }
            //}
        }

        // Custom data filtering for SqlDataSource
        private void DashboardConfigurator_CustomFilterExpression(object sender, CustomFilterExpressionWebEventArgs e)
        {
            //var userName = contextAccessor.HttpContext.Session.GetString("CurrentUser");
            //if (userName == null)
            //    userName = contextAccessor.HttpContext.Request.Headers["User"];

            //if (e.DashboardId == "SQLFilter" && e.QueryName == "Categories")
            //{
            //    if (userName == "User")
            //    {
            //        e.FilterExpression = CriteriaOperator.Parse("StartsWith([CategoryName], 'C')");
            //    }
            //}
        }

        private string GetBaseUrl()
        {
            var request = contextAccessor.HttpContext.Request;
            return UriHelper.BuildAbsolute(request.Scheme, request.Host);
        }

        #endregion

        // ----------------------------------------------------------------------------------

    }
}