using System.Data;
using System.Text;

/// <summary>Static helper for exporting DataSet contents to an HTML table (compatible with Excel).</summary>
public static class ReportHelper
{
    /// <summary>Converts the first table of a DataSet into an HTML table string for Excel export.</summary>
    /// <param name="ds">The data source.</param>
    /// <param name="sheetName">Sheet name (unused in HTML, kept for future XLSX support).</param>
    public static string DataSetToExcel(DataSet ds, string sheetName)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table border='1'>");

        if (ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            sb.Append("<tr>");
            foreach (DataColumn col in dt.Columns)
                sb.Append("<th>").Append(col.ColumnName).Append("</th>");
            sb.Append("</tr>");

            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<tr>");
                foreach (DataColumn col in dt.Columns)
                    sb.Append("<td>").Append(row[col]).Append("</td>");
                sb.Append("</tr>");
            }
        }

        sb.Append("</table>");
        return sb.ToString();
    }
}
