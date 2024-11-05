using DxBlazorReport.Authentication;
using DxBlazorReport.Code;
using DxBlazorReport.Model;
using DxBlazorReport.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DxBlazorReport.Data
{
    public class SqlData
    {
        // ----------------------------------------------------------------------------------

        #region Public Functions

        public async Task<object> GetDashboardData(string dashboardId)
        {
            //List<DepartmentQueueProcessesView> data = GetDepartmentQueueProcessesViewData().Result;
            //return data;

            DataTableToObjectConverter converter = new DataTableToObjectConverter();
            DataTable dt = new DataTable();
            string whereStatement = "";

            switch (dashboardId)
            {
                case "HomePageDashboard":
                case "PivotDashboard":
                case "DashboardEdit":
                case "DepartmentQueueProcessesView_all":
                    whereStatement = "";
                    break;

                case "DepartmentQueueProcessesView_thisyear":
                    whereStatement = @"
  WHERE YEAR([EndTime]) = YEAR(GETDATE())";
                    break;
                default:
                    whereStatement = "";
                    break;
            }

            List<DepartmentQueueProcessesView> data = GetDepartmentQueueProcessesViewData(whereStatement).Result;
            return data;
        }

        public async Task<List<DepartmentQueueProcessesView>> GetDepartmentQueueProcessesViewData(string whereStatement = "")
        {
            string query = @"
SELECT [QOID]
      ,[POID]
      ,[LOID]
      ,[DepartmentCode]
      ,[DepartmentName]
	  ,CASE WHEN [DepartmentCode] = 'SNT' THEN CASE WHEN [LocationWorkCenter] = 'false' THEN '' ELSE [LocationWorkCenter] END ELSE [Workcenter] END AS [Workcenter]
      ,CASE WHEN [LocationWorkCenter] = 'false' THEN '' ELSE [LocationWorkCenter] END AS [LocationWorkCenter]
      ,[OperatorId]
      ,[OperatorName]
      ,[sono]
      ,[opno]
      ,[PartNo]
      ,[Location]
      ,[Process]
      ,[ProcessDescrip]
      ,[Qty]
      ,[Reason]
      ,[StartTime]
      ,[EndTime]
      ,[TimeSpent]
      ,[HoursSpent]
      ,CAST([MinutesSpent] AS VARCHAR(50)) AS [MinutesSpent]
      ,[EndTimeUsed]
      ,[ProcessNotes]
      ,[UpdatedDate]
  FROM [JAM].[dbo].[DepartmentQueueProcessesView] {0}";
            string fullQuery = string.Format(query, whereStatement);
            DataTableToObjectConverter converter = new DataTableToObjectConverter();

            DataTable dt = null;
            try
            {
                dt = await converter.GetDataTableFromQuery(fullQuery);
            }
            catch (Exception ex)
            {
                //LogError("GetSqlData", ex, ex.Message);
                return null;
            }

            if (dt == null)
            {
                return new List<DepartmentQueueProcessesView>();
            }

            List<DepartmentQueueProcessesView> data = new List<DepartmentQueueProcessesView>();
            foreach (DataRow dr in dt.Rows)
            {
                data.Add(new DepartmentQueueProcessesView()
                {
                    QOID = int.Parse(dr["QOID"].ToString()),
                    POID = int.Parse(dr["POID"].ToString()),
                    LOID = int.Parse(dr["LOID"].ToString()),
                    DepartmentCode = dr["DepartmentCode"].ToString(),
                    DepartmentName = dr["DepartmentName"].ToString(),
                    Workcenter = dr["Workcenter"].ToString(),
                    LocationWorkCenter = dr["LocationWorkCenter"].ToString(),
                    OperatorId = dr["OperatorId"].ToString(),
                    OperatorName = dr["OperatorName"].ToString().Trim(),
                    sono = dr["sono"].ToString(),
                    opno = int.Parse(dr["opno"].ToString()),
                    PartNo = dr["PartNo"].ToString(),
                    Location = dr["Location"].ToString(),
                    Process = dr["Process"].ToString(),
                    ProcessDescrip = dr["ProcessDescrip"].ToString(),
                    Qty = int.Parse(dr["Qty"].ToString()),
                    Reason = dr["Reason"].ToString(),
                    StartTime = DateTime.Parse(dr["StartTime"].ToString()),
                    EndTime = DateTime.Parse(dr["EndTime"].ToString()),
                    TimeSpent = decimal.Parse(dr["TimeSpent"].ToString()),
                    HoursSpent = dr["HoursSpent"].ToString(),
                    MinutesSpent = decimal.Parse(dr["MinutesSpent"].ToString()),
                    EndTimeUsed = dr["EndTimeUsed"].ToString(),
                    ProcessNotes = dr["ProcessNotes"].ToString(),
                    UpdatedDate = DateTime.Parse(dr["UpdatedDate"].ToString())
                });
            }

            return data;
        }


        public async Task<List<string>> GetDepartmentList()
        {
            string fullQuery = @"
SELECT [Definition] AS [DepartmentName]
FROM [JAM].[dbo].[Definitions] def
  LEFT JOIN (
		SELECT DISTINCT [DepartmentName]
		FROM [JAM].[dbo].[DepartmentQueueProcessesView]
		WHERE Workcenter <> 'false'
		) dept
	ON def.[Definition] = dept.[DepartmentName]
WHERE def.[Type] = 'Department'
ORDER BY def.[SortOrder]";
            DataTableToObjectConverter converter = new DataTableToObjectConverter();

            DataTable dt = null;
            try
            {
                dt = await converter.GetDataTableFromQuery(fullQuery);
            }
            catch (Exception ex)
            {
            }

            if (dt == null)
            {
                return null;
            }
            else
            {
                List<string> deptList = new List<string>();

                foreach (DataRow dr in dt.Rows)
                {
                    deptList.Add(dr["DepartmentName"].ToString());
                }

                return deptList;
            }
        }

        public async Task<List<string>> GetWorkcenterList()
        {
            string fullQuery = "SELECT DISTINCT [Workcenter] FROM [JAM].[dbo].[DepartmentQueueProcessesView] WHERE Workcenter <> 'false' ORDER BY 1";
            DataTableToObjectConverter converter = new DataTableToObjectConverter();

            DataTable dt = null;
            try
            {
                dt = await converter.GetDataTableFromQuery(fullQuery);
            }
            catch (Exception ex)
            {
            }

            if (dt == null)
            {
                return null;
            }
            else
            {
                List<string> wcList = new List<string>();

                foreach (DataRow dr in dt.Rows)
                {
                    wcList.Add(dr["Workcenter"].ToString());
                }

                return wcList;
            }
        }

        public async Task<List<string>> GetOperatorList()
        {
            string fullQuery = "SELECT DISTINCT [OperatorName] FROM [JAM].[dbo].[DepartmentQueueProcessesView] ORDER BY 1";
            DataTableToObjectConverter converter = new DataTableToObjectConverter();

            DataTable dt = null;
            try
            {
                dt = await converter.GetDataTableFromQuery(fullQuery);
            }
            catch (Exception ex)
            {
            }

            if (dt == null)
            {
                return null;
            }
            else
            {
                List<string> opList = new List<string>();

                foreach (DataRow dr in dt.Rows)
                {
                    //opList.Add(dr["OperatorName"].ToString());
                    //opList.Add(dr["OperatorName"].ToString().Trim().PadRight(35, ' '));
                    opList.Add(dr["OperatorName"].ToString().Trim());
                }

                return opList;
            }
        }

        #endregion

        // ----------------------------------------------------------------------------------

        #region Unused EdiAttendantData Functions

        public async Task<object> GetDashboardData(string dashboardId, string user, string promoCode, string dataSourceName, string dataSourceComponentName)
        {
            string query = "";
            string fullQuery = "";
            DataTableToObjectConverter converter = new DataTableToObjectConverter();
            DataTable dt = new DataTable();

            switch (dashboardId)
            {

                case "ProductActivityDashboard":
                    List<EdiAttendantData> data = GetEdiAttendantData(promoCode).Result;
                    return data;

                case "dashboard1":
                case "dashboard2":
                case "HomePageDashboard":
                    List<ShippingData> shippingData = new List<ShippingData>();
                    string shippingRange = "FROMBEGINNINGOFYEAR";

                    switch (dataSourceName)
                    {
                        case "ShippingData_FromBeginningofYear":
                            shippingRange = "FROMBEGINNINGOFYEAR";
                            break;
                        case "ShippingData_FromLastThreeMonths":
                            shippingRange = "LAST3MONTHS";
                            break;
                        case "ShippingData_FromMonth":
                            shippingRange = "MONTH";
                            break;
                        case "ShippingData_FromToday":
                            shippingRange = "TODAY";
                            break;
                    }
                    shippingData = GetShippingData(promoCode, shippingRange).Result;
                    return shippingData;

                case "HomePageDashboardTest":
                    List<ShippingData> shippingTestData = new List<ShippingData>();

                    query = GetQuery("Shipped", promoCode);

                    switch (dataSourceName)
                    {
                        //case "ShippingData_FromBeginningofYear":
                        //                           fullQuery = query + @"
                        //AND SHIP_DATE >= CAST(YEAR(GETDATE()) AS VARCHAR(4)) + '-01-01'";
                        //                           break;
                        //case "ShippingData_FromLastThreeMonths":
                        //                           fullQuery = query + @"
                        //AND SHIP_DATE >= CONVERT(date,DATEADD(month, -3, GETDATE())) ";
                        //                           break;
                        //case "ShippingDataa_FromToday":
                        //                           fullQuery = query + @"
                        //AND SHIP_DATE = CONVERT(date,GETDATE()) ";
                        //                           break;

                        case "ShippingData_FromBeginningofYear":
                            fullQuery = query + @"
	AND SHIP_DATE >= '2022-01-01' AND SHIP_DATE <= '2022-04-15'-- + CAST(DAY(GETDATE()) AS VARCHAR(2)) "; // testing dates
                            break;
                        case "ShippingData_FromLastThreeMonths":
                            fullQuery = query + @"
	AND SHIP_DATE >= '2022-01-15'-- + CAST(DAY(GETDATE()) AS VARCHAR(2)) "; // testing dates
                            break;
                        case "ShippingData_FromToday":
                            fullQuery = query + @"
	AND SHIP_DATE = '2022-04-15' --+ CAST(DAY(GETDATE()) AS VARCHAR(2))  "; // testing dates
                            break;
                    }

                    if (!string.IsNullOrWhiteSpace(fullQuery))
                    {
                        fullQuery += @"
  ORDER BY CAST(DATEPART(MONTH, ISNULL(cms.[SHIP_DATE], '1900-01-01 00:00:00.000')) AS INT) ASC
	,CAST(DATEPART(DAY, ISNULL(cms.[SHIP_DATE], '1900-01-01 00:00:00.000')) AS INT) ASC";
                        dt = await converter.GetDataTableFromQuery(fullQuery);
                        shippingTestData = await converter.GetObjectListFromDataTable<ShippingData>(dt, new Dictionary<string, string>());
                        return shippingTestData;
                    }
                    break;
            }
            return new List<ShippingData>();
        }

        public string GetQuery(string queryName, string promoCode, string databasePrefix = "MOM-")
        {
            string databaseName = databasePrefix + promoCode;
            string query = "";

            switch (queryName)
            {
                case "Shipped":
                    query = @"SELECT CAST(cms.[ORDERNO] AS VARCHAR(20)) AS OrderNo
      ,CAST(cms.[CUSTNUM] AS VARCHAR(20)) AS CustNo
      ,cms.[CL_KEY]		AS SourceKey
      ,cms.[ODR_DATE]	AS OrderDate
      ,cms.[SHIP_DATE]	AS ShipDate
      ,cms.[SHIPLIST]	AS ShipList
      ,cms.[ORD_TOTAL]	AS OrderTotal
      ,CAST(items.[ITEM_ID]	AS VARCHAR(20)) AS ItemId
      ,items.[ITEM]			AS ItemCode
      ,CAST(items.[QUANTS]	AS INT) AS ItemQty
      ,items.[IT_SDATE]		AS ItemShipDate
      ,items.[ITEM_STATE]	AS ItemState
      ,DATEPART(YEAR, ISNULL(cms.[SHIP_DATE], '1900-01-01 00:00:00.000')) AS [Year]
      ,DATEPART(MONTH, ISNULL(cms.[SHIP_DATE], '1900-01-01 00:00:00.000')) AS [M]
      ,DATENAME(MONTH, ISNULL(cms.[SHIP_DATE], '1900-01-01 00:00:00.000')) AS [Month]
      ,DATENAME(DAY, ISNULL(cms.[SHIP_DATE], '1900-01-01 00:00:00.000')) AS [Day]
	  ,CONVERT(char(11),cms.[SHIP_DATE],103) AS DisplayDate 
  FROM [@databaseName].[dbo].[CMS] cms 
  LEFT JOIN [@databaseName].[dbo].[ITEMS] items
	ON cms.[ORDERNO] = items.[ORDERNO]
  WHERE cms.[SHIP_DATE] IS NOT NULL
	AND items.[ITEM_ID] IS NOT NULL";
                    query = query.Replace("@databaseName", databaseName);
                    break;
            }

            return query;
        }

        public async Task<List<EdiAttendantData>> GetEdiAttendantData(string promoCode)
        {
            DataTable dt = await GetSqlEdiAttendantDataTable(promoCode);
            List<EdiAttendantData> data = new List<EdiAttendantData>();
            foreach (DataRow dr in dt.Rows)
            {
                data.Add(new EdiAttendantData()
                {
                    Location_Name = dr["Location_Name"].ToString(),
                    LocationId = dr["LocationId"].ToString(),
                    UOM = dr["UOM"].ToString(),
                    QtySold = decimal.Parse(dr["QtySold"].ToString()),
                    DateStart = dr["DateStart"].ToString(),
                    DateEnd = dr["DateEnd"].ToString(),
                    Item = dr["Item"].ToString(),
                    Price = int.Parse(dr["Price"].ToString()),
                    WeekNo = int.Parse(dr["WeekNo"].ToString()),
                    ExtPrice = decimal.Parse(dr["ExtPrice"].ToString()),
                    UPC = double.Parse(dr["UPC"].ToString()),
                });
            }

            return data;
        }
        public async Task<List<ShippingData>> GetShippingData(string promoCode, string range = "FROMBEGINNINGOFYEAR")
        {
            string query = "EXEC [dbo].[CTCsp_GetShippedData] '{0}', '{1}'";
            string fullQuery = string.Format(query, promoCode, range);
            DataTableToObjectConverter converter = new DataTableToObjectConverter();
            DataTable dt = await converter.GetDataTableFromQuery(fullQuery);
            List<ShippingData> data = new List<ShippingData>();
            if (dt == null)
                return data;

            foreach (DataRow dr in dt.Rows)
            {
                data.Add(new ShippingData()
                {
                    OrderNo = dr["OrderNo"].ToString(),
                    CustNo = dr["CustNo"].ToString(),
                    SourceKey = dr["SourceKey"].ToString(),
                    OrderDate = DateTime.Parse(dr["OrderDate"].ToString()),
                    ShipDate = DateTime.Parse(dr["ShipDate"].ToString()),
                    ShipList = dr["ShipList"].ToString(),
                    OrderTotal = decimal.Parse(dr["OrderTotal"].ToString()),
                    ItemId = dr["ItemId"].ToString(),
                    ItemCode = dr["ItemCode"].ToString(),
                    ItemQty = int.Parse(dr["ItemQty"].ToString()),
                    ItemShipDate = DateTime.Parse(dr["ItemShipDate"].ToString()),
                    ItemState = dr["ItemState"].ToString(),
                    Year = int.Parse(dr["Year"].ToString()),
                    M = int.Parse(dr["M"].ToString()),
                    Month = dr["Month"].ToString(),
                    Day = int.Parse(dr["Day"].ToString()),
                    DisplayDate = dr["DisplayDate"].ToString()
                });
            }

            return data;
        }

        public async Task<DataTable> GetSqlEdiAttendantDataTable(string promoCode)
        {
            if (promoCode == "")
            {
                promoCode = "0000";
            }

            //            string query = @"
            //DECLARE @promoCode		VARCHAR(50) = '{0}'
            //DECLARE @ediAttendantId	VARCHAR(50) = NULL
            //DECLARE @databaseName	VARCHAR(50) = 'ediAttendant_'
            //DECLARE @workQuery		VARCHAR(MAX) = ''	

            //SELECT @ediAttendantId = [ediAttendantId]
            //  FROM [CTCEnterprise].[dbo].[Company]
            //  WHERE [PromoCode] = @promoCode
            //SET @databaseName = @databaseName + RTRIM(@ediAttendantId)

            //--SELECT @promoCode AS promoCode, @ediAttendantId AS ediAttendantId, @databaseName AS [database]

            //SET @workQuery = 'SELECT [Location_Name]
            //      ,[Location_id] AS LocationId
            //      ,[uom]		AS UOM
            //      ,[QtySold]	AS QtySold
            //      ,[date_beg]	AS DateStart
            //      ,[date_end]	AS DateEnd
            //      ,[Item]		AS Item
            //      ,[price]		AS Price
            //      ,[weekno]		AS WeekNo
            //      ,[ExtPrice]	AS ExtPrice
            //      ,[upc]		AS UPC
            //  FROM [@databaseName].[dbo].[PDDtl_bmy]'

            //SET @workQuery = REPLACE(@workQuery, '@databaseName', @databaseName);

            //EXEC(@workQuery)
            //";
            string query = "EXEC CTCsp_GetEdiAttendantData '{0}' ";

            string fullQuery = string.Format(query, promoCode);
            DataTableToObjectConverter converter = new DataTableToObjectConverter();

            DataTable dt = null;
            try
            {
                dt = await converter.GetDataTableFromQuery(fullQuery);
            }
            catch (Exception ex)
            {
                //LogError("GetSqlData", ex, ex.Message);
            }

            return dt;
        }
        public async Task<DataTable> GetSqlEdiAttendantDataTable(UserAccount user)
        {
            //            string query = @"
            //DECLARE @promoCode		VARCHAR(50) = '{0}'
            //DECLARE @ediAttendantId	VARCHAR(50) = NULL
            //DECLARE @databaseName	VARCHAR(50) = 'ediAttendant_'
            //DECLARE @workQuery		VARCHAR(MAX) = ''	

            //SELECT @ediAttendantId = [ediAttendantId]
            //  FROM [CTCEnterprise].[dbo].[Company]
            //  WHERE [PromoCode] = @promoCode
            //SET @databaseName = @databaseName + RTRIM(@ediAttendantId)

            //--SELECT @promoCode AS promoCode, @ediAttendantId AS ediAttendantId, @databaseName AS [database]

            //SET @workQuery = 'SELECT [Location_Name]
            //      ,[Location_id] AS LocationId
            //      ,[uom]		AS UOM
            //      ,[QtySold]	AS QtySold
            //      ,[date_beg]	AS DateStart
            //      ,[date_end]	AS DateEnd
            //      ,[Item]		AS Item
            //      ,[price]		AS Price
            //      ,[weekno]		AS WeekNo
            //      ,[ExtPrice]	AS ExtPrice
            //      ,[upc]		AS UPC
            //  FROM [@databaseName].[dbo].[PDDtl_bmy]'

            //SET @workQuery = REPLACE(@workQuery, '@databaseName', @databaseName);

            //EXEC(@workQuery)
            //";
            string query = "EXEC CTCsp_GetEdiAttendantData '{0}' ";
            string fullQuery = string.Format(query, user.PromoCode);
            DataTableToObjectConverter converter = new DataTableToObjectConverter();

            DataTable dt = null;
            try
            {
                dt = await converter.GetDataTableFromQuery(fullQuery);
            }
            catch (Exception ex)
            {
                //LogError("GetSqlData", ex, ex.Message);
            }

            return dt;
        }

        #endregion

        // ----------------------------------------------------------------------------------
    }
}
