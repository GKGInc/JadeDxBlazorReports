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
    public partial class WorkcenterReport : DevExpress.XtraReports.UI.XtraReport
    {
        private List<string> _departmentList { get; set; }
        private List<string> _worcenterList { get; set; }

        public WorkcenterReport()
        {
            InitializeComponent();

            CustomFunctions.Register(new GetDateFromMilliSeconds());

            DevExpress.Utils.DeserializationSettings.RegisterTrustedClass(typeof(DataTableToObjectConverter));
        }
        public WorkcenterReport(List<string> departmentList, List<string> workcenterList)
        {
            InitializeComponent();

            _departmentList = departmentList;
            _worcenterList = workcenterList;
            CustomFunctions.Register(new GetDateFromMilliSeconds());
        }

        private void WorkcenterReport_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
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

        private void WorkcenterReport_ParametersRequestSubmit(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            var report = sender as XtraReport;
            int paramIndex = 0;

            if (_departmentList == null || _worcenterList == null) return;

            foreach (var param in report.Parameters)
            {
                if ((param as DevExpress.XtraReports.Parameters.Parameter).Value.GetType() == typeof(String[]))
                {
                    if ((param as DevExpress.XtraReports.Parameters.Parameter).Name == "departmentName")
                    {
                        if ((report.Parameters[paramIndex] as DevExpress.XtraReports.Parameters.Parameter).ValueInfo == "")
                        {
                            e.ParametersInformation[paramIndex].Parameter.Value = _departmentList.ToArray();
                        }
                    }
                    if ((param as DevExpress.XtraReports.Parameters.Parameter).Name == "workCenter")
                    {
                        if ((report.Parameters[paramIndex] as DevExpress.XtraReports.Parameters.Parameter).ValueInfo == "")
                        {
                            e.ParametersInformation[paramIndex].Parameter.Value = _worcenterList.ToArray();
                        }
                    }                   
                }

                paramIndex++;
            }
        }
    }
}
