using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL2EXCEL
{
    internal class DatatableToCSV
    {
        public void write(DataTable dt, String location)
        {
            var lines = new List<string>();

            string[] columnNames = dt.Columns
                .Cast<DataColumn>()
                .Select(column => column.ColumnName)
                .ToArray();

            var header = string.Join(",", columnNames.Select(name => $"\"{name}\""));
            lines.Add(header);

            var valueLines = dt.AsEnumerable()
                .Select(row => string.Join(",", row.ItemArray.Select(val => $"\"{val}\"")));

            lines.AddRange(valueLines);

            

            File.WriteAllLines(location + "melloFIN_" + DateTime.Now.ToString("MMddyyyy_hhmm")  + ".csv", lines);
        }
    }
}

