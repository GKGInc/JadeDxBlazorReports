using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using DevExpress.Blazor.Reporting.Controllers;
using DevExpress.Blazor.Reporting.Internal.Services;

namespace DxBlazorReport
{
    public class DownloadExportResultController : DownloadExportResultControllerBase
    {
        public DownloadExportResultController(ExportResultStorage exportResultStorage) : base(exportResultStorage)
        {
        }
    }
}