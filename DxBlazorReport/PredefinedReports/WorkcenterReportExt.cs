using DevExpress.XtraReports.Expressions;
using DevExpress.XtraReports.UI;
using DxBlazorReport.Code;
using DxBlazorReport.Data;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace DxBlazorReport.PredefinedReports
{
    public partial class WorkcenterReportExt : DevExpress.XtraReports.UI.XtraReport
    {
        public List<string> wcList { get; set; }

        public WorkcenterReportExt()
        {
            InitializeComponent();

            CustomFunctions.Register(new GetDateFromMilliSeconds());

            DevExpress.Utils.DeserializationSettings.RegisterTrustedClass(typeof(DataTableToObjectConverter));
        }
        public WorkcenterReportExt(List<string> workcenterList)
        {
            InitializeComponent();

            wcList = workcenterList;
            CustomFunctions.Register(new GetDateFromMilliSeconds());
        }

        private void WorkcenterReportExt_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            var report = sender as XtraReport;
            //if ((report.Parameters[0] as DevExpress.XtraReports.Parameters.Parameter).Type == typeof(System.DateTime))
            //    (report.Parameters[0] as DevExpress.XtraReports.Parameters.Parameter).Value = DevExpress.XtraReports.Parameters.Range.Create(DateTime.Now.AddDays(-7), DateTime.Now);
            foreach (var param in report.Parameters)
            {
                if ((param as DevExpress.XtraReports.Parameters.Parameter).Type == typeof(System.DateTime))
                {
                    //(param as DevExpress.XtraReports.Parameters.Parameter).Value = DevExpress.XtraReports.Parameters.Range.Create(DateTime.Now.AddDays(-8), DateTime.Now);
                    (param as DevExpress.XtraReports.Parameters.Parameter).Value = DevExpress.XtraReports.Parameters.Range.Create(DateTime.Now.AddDays(-15), DateTime.Now);
                }
            }
        }

        private void WorkcenterReportExt_ParametersRequestSubmit(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            var report = sender as XtraReport;
            int paramIndex = 0;
            //List<string> wcList = new List<string>();

            foreach (var param in report.Parameters)
            {
                if ((param as DevExpress.XtraReports.Parameters.Parameter).Value.GetType() == typeof(String[]))
                {
                    if ((param as DevExpress.XtraReports.Parameters.Parameter).Name == "workCenter")
                    {
                        if ((report.Parameters[1] as DevExpress.XtraReports.Parameters.Parameter).ValueInfo == "")
                        {
                            // Get list of WorkCenters

                            //string fullQuery = "SELECT DISTINCT [Workcenter] FROM [JAM].[dbo].[DepartmentQueueProcessesView] WHERE Workcenter <> 'false' ORDER BY 1";
                            ////string fullQuery = "SELECT DISTINCT [OperatorName] FROM [JAM].[dbo].[DepartmentQueueProcessesView] ORDER BY 1";
                            //DataTableToObjectConverter converter = new DataTableToObjectConverter();

                            //DataTable dt = null;
                            //try
                            //{
                            //    dt = await converter.GetDataTableFromQuery(fullQuery);
                            //}
                            //catch (Exception ex)
                            //{
                            //}

                            //if (dt == null)
                            //{
                            //    //return new List<DepartmentQueueProcessesView>();
                            //}
                            //foreach (DataRow dr in dt.Rows)
                            //{
                            //    wcList.Add(dr["Workcenter"].ToString());
                            //    //wcList.Add(dr["OperatorName"].ToString());
                            //}

                            e.ParametersInformation[paramIndex].Parameter.Value = wcList.ToArray();
                        }
                    }
                }

                paramIndex++;
            }
        }
    }
}
