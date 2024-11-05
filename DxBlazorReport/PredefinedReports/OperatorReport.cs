using DevExpress.Data.Filtering;
using DevExpress.XtraReports.Expressions;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using DxBlazorReport.Code;
using DxBlazorReport.Data;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace DxBlazorReport.PredefinedReports
{
    public partial class OperatorReport : DevExpress.XtraReports.UI.XtraReport
    {
        public List<string> deptList { get; set; }
        public List<string> opList { get; set; }


        public OperatorReport()
        {
            InitializeComponent();

            CustomFunctions.Register(new GetDateFromMilliSeconds());

            DevExpress.Utils.DeserializationSettings.RegisterTrustedClass(typeof(DataTableToObjectConverter));
        }
        public OperatorReport(List<string> departmentList, List<string> operatorList)
        {
            InitializeComponent();

            CustomFunctions.Register(new GetDateFromMilliSeconds());

            deptList = departmentList;
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
            var report = sender as XtraReport;
            int paramIndex = 0;

            if (deptList == null || opList == null) return;

            foreach (var param in report.Parameters)
            {
                if ((param as DevExpress.XtraReports.Parameters.Parameter).Value.GetType() == typeof(String[]))
                {
                    if ((param as DevExpress.XtraReports.Parameters.Parameter).Name == "departmentName")
                    {
                        if ((report.Parameters[paramIndex] as DevExpress.XtraReports.Parameters.Parameter).ValueInfo == "")
                        {
                            List<string> tempList = deptList.ToArray().ToList<string>();
                            tempList.Add("");
                            e.ParametersInformation[paramIndex].Parameter.Value = tempList.ToArray();
                        }
                        else
                        {
                            List<string> tempList = new List<string>() { (report.Parameters[paramIndex] as DevExpress.XtraReports.Parameters.Parameter).ValueInfo };
                            tempList.Add("");
                            e.ParametersInformation[paramIndex].Parameter.Value = tempList.ToArray();
                        }
                    }
                    if ((param as DevExpress.XtraReports.Parameters.Parameter).Name == "operatorName")
                    {
                        if ((param as DevExpress.XtraReports.Parameters.Parameter).ValueInfo == "")
                        {
                            e.ParametersInformation[paramIndex].Parameter.Value = opList.ToArray();
                            //e.ParametersInformation[paramIndex].Parameter.Value = string.Join("|", opList);
                        }
                    }
                }
                
                paramIndex++;
            }
        }
    }
}
