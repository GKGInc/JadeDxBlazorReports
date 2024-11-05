using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.Expressions;
using System;
using System.Linq;

namespace DxBlazorReport.Data
{
    [VSDesignerCustomFunction]
    public class GetDateFromSeconds : ReportCustomFunctionOperatorBase
    {
        public override string FunctionCategory => "Date & Time";
        public override int MinOperandCount => 1;
        public override int MaxOperandCount => 1;
        public override object Evaluate(params object[] operands)
        {
            //var ints = operands.Cast<int>().ToArray();
            //return new DateTime(ints[0], ints[1], ints[2]);
            //return new DateTime(ints[0], ints[1], ints[2]);

            if (operands.Count() > 0 && operands[0] == null)
            {

            }

            List<long> values = new List<long>();
            foreach (object v in operands)
                values.Add(Convert.ToInt64(v));

            var ints = values.ToArray();
            TimeSpan time = TimeSpan.FromSeconds(ints[0]);
            DateTime dateTime = DateTime.Today.Add(time);
            string displayTime = dateTime.ToString("hh:mm:ss");
            string str = time.ToString(@"hh\:mm\:ss");

            return str;
            //return dateTime;
        }
        public override bool IsValidOperandType(int operandIndex, int operandCount, Type type)
        {
            if (operandIndex >= operandCount)
                return false;
            return type == typeof(int);
        }
        public override string Description => "GetDateFromSeconds(Seconds)\r\nCreates Datetime instance based on Seconds value";

        public override string Name => "GetDateFromSeconds";
    }
}
