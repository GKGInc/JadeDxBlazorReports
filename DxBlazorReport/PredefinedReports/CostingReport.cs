using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;


namespace DxBlazorReport.PredefinedReports
{
    public partial class CostingReport : DevExpress.XtraReports.UI.XtraReport
    {
        public CostingReport()
        {
            InitializeComponent();
            sqlDataSource1.ConnectionOptions.CommandTimeout = 120;  // seconds

            this.BeforePrint += CostingReport_BeforePrint;
         
        }

        private void CostingReport_BeforePrint(object sender, CancelEventArgs e)
        {
            this.Detail.SortFields.Clear();

            //string sortField = this.Parameters["SortBy"].Value != null
            //    ? this.Parameters["SortBy"].Value.ToString()
            //    : "partno";

            //string direction = this.Parameters["SortDirection"].Value != null
            //    ? this.Parameters["SortDirection"].Value.ToString()
            //    : "Ascending";

            string sortField = this.Parameters["sortBy"].Value != null
                ? this.Parameters["sortBy"].Value.ToString()
                : "partno";

            string direction = this.Parameters["sortDirection"].Value != null
                ? this.Parameters["sortDirection"].Value.ToString()
                : "Ascending";

            DevExpress.XtraReports.UI.XRColumnSortOrder sortOrder = XRColumnSortOrder.Ascending;
            if (direction == "Descending")
                sortOrder = XRColumnSortOrder.Descending;

            this.Detail.SortFields.Add(new DevExpress.XtraReports.UI.GroupField(sortField, sortOrder));
        }

    }
}
