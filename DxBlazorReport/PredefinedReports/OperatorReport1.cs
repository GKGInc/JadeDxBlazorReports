using DevExpress.Data.Filtering;
using DevExpress.XtraReports.Expressions;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using DxBlazorReport.Code;
using DxBlazorReport.Data;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace DxBlazorReport.PredefinedReports
{
    public partial class OperatorReport1 : DevExpress.XtraReports.UI.XtraReport
    {
      public  List<string> opList { get; set; }

        public OperatorReport1()
        {
            InitializeComponent();

            CustomFunctions.Register(new GetDateFromMilliSeconds());

            DevExpress.Utils.DeserializationSettings.RegisterTrustedClass(typeof(DataTableToObjectConverter));
        }
        public OperatorReport1(List<string> operatorList)
        {
            InitializeComponent();

            CustomFunctions.Register(new GetDateFromMilliSeconds());

            opList = operatorList;
        }

        private void OperatorReport_ParametersRequestBeforeShow(object sender, ParametersRequestEventArgs e)
        {
            var report = sender as XtraReport;

            foreach (var param in report.Parameters)
            {
                if ((param as DevExpress.XtraReports.Parameters.Parameter).Type == typeof(System.DateTime))
                {
                    //(param as DevExpress.XtraReports.Parameters.Parameter).Value = DevExpress.XtraReports.Parameters.Range.Create(DateTime.Now.AddDays(-8), DateTime.Now);
                    (param as DevExpress.XtraReports.Parameters.Parameter).Value = DevExpress.XtraReports.Parameters.Range.Create(DateTime.Now.AddDays(-15), DateTime.Now);
                }
            }
        }

        private async void OperatorReport_ParametersRequestSubmit(object sender, ParametersRequestEventArgs e)
        {
            //var report = sender as XtraReport;
            //int paramIndex = 0;
            ////List<string> opList = new List<string>();

            //foreach (var param in report.Parameters)
            //{
            //    if ((param as DevExpress.XtraReports.Parameters.Parameter).Value.GetType() == typeof(String[]))
            //    {
            //        if ((param as DevExpress.XtraReports.Parameters.Parameter).Name == "operatorName")
            //        {
            //            if ((param as DevExpress.XtraReports.Parameters.Parameter).ValueInfo == "")
            //            {
            //                //// Get list of OperatorNames

            //                ////string fullQuery = "SELECT DISTINCT [Workcenter] FROM [JAM].[dbo].[DepartmentQueueProcessesView] WHERE Workcenter <> 'false' ORDER BY 1";
            //                //string fullQuery = "SELECT DISTINCT [OperatorName] FROM [JAM].[dbo].[DepartmentQueueProcessesView] ORDER BY 1";
            //                //DataTableToObjectConverter converter = new DataTableToObjectConverter();

            //                //DataTable dt = null;
            //                //try
            //                //{
            //                //    dt = await converter.GetDataTableFromQuery(fullQuery);
            //                //}
            //                //catch (Exception ex)
            //                //{
            //                //}

            //                //if (dt == null)
            //                //{
            //                //    //return new List<DepartmentQueueProcessesView>();
            //                //}
            //                //foreach (DataRow dr in dt.Rows)
            //                //{
            //                //    //wcList.Add(dr["Workcenter"].ToString());
            //                //    opList.Add(dr["OperatorName"].ToString().Trim().PadRight(35, ' '));
            //                //}

            //                e.ParametersInformation[paramIndex].Parameter.Value = opList.ToArray();
            //            }
            //        }
            //    }

            //    paramIndex++;
            //}
        }

        private void OperatorReport_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        //private void OperatorReport_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        //{
        //    var report = sender as XtraReport;

        //    //var source = ((DynamicListLookUpSettings)report.Parameters[0].LookUpSettings).DataSource as UniversalDataSource;
        //    //source.FillList();
        //    //var parameter = report.Parameters[0];

        //    //var dataContext = ((IServiceProvider)report).GetService(typeof(DataContext)) as DataContext;
        //    //var values = LookUpHelper.GetLookUpValues(parameter.LookUpSettings, dataContext).Select(x => (int)x.Value).ToArray();
        //    //parameter.Value = values;
        //}
    }
}
