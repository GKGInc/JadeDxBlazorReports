using DxBlazorReport.Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DxBlazorReport.Model
{
    public class EdiAttendantData
    {
        public string Location_Name { get; set; }
        public string LocationId { get; set; }
        public string UOM { get; set; }
        public decimal QtySold { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string Item { get; set; }
        public int Price { get; set; }
        public int WeekNo { get; set; }
        public decimal ExtPrice { get; set; }
        public double UPC { get; set; }
    }
}