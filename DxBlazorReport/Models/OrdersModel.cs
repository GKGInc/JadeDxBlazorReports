using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DxBlazorReport.Models
{
    public class OrdersModel
    {
        [Range(1, 100000, ErrorMessage = "The OrderID value should be a number between 1 and 100,000.")]
        public int OrderId { get; set; } = 11077;

        public List<int> OrdersData { get; set; } = new List<int> { 11064, 11065, 11070, 11075, 11076, 11077 };
    }
}
