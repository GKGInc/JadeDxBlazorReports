using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DxBlazorReport.Data
{
    public partial class GlobalData
    {
        public string firstLoadScreen { get; set; } = "/"; //"CustomerSoPage"; // 

        public List<string> departmentList { get; set; }
        public List<string> workcenterList { get; set; }
        public List<string> operatorList { get; set; }
    }
}
