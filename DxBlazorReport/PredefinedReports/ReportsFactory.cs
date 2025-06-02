using DevExpress.XtraReports.UI;

namespace DxBlazorReport.PredefinedReports
{
    public static class ReportsFactory
    {
        public static Dictionary<string, Func<XtraReport>> Reports = new Dictionary<string, Func<XtraReport>>()
        {
            ["DepartmentQueueCountRecordViewReport"] = () => new DepartmentQueueCountRecordViewReport(),
            ["DepartmentQueueProcessesViewReport"] = () => new DepartmentQueueProcessesViewReport(),
            ["EmployeeReport"] = () => new EmployeeReport(),
            ["CostReport"] = () => new CostReport()
        };
    }
}