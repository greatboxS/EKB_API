using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKANBAN_SYS_LIB.OLEDB
{
    public class ExcelHandle
    {
        public static DataTable ReadExcelTable(string _path, int _tableIndex)
        {
            DataTable tb = new DataTable();

            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _path + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
            try
            {
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                    string query = $"SELECT * FROM [{schemaTable.Rows[_tableIndex]["TABLE_NAME"]}]";
                    OleDbDataAdapter olead_adapter = new OleDbDataAdapter(query, conn);
                    olead_adapter.Fill(tb);
                }
            }
            catch (Exception e)
            { return null; }

            return tb;
        }

        public static DataTable GetTableName(string _path)
        {
            DataTable tb = new DataTable();

            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _path + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
            try
            {
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    tb = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                }
            }
            catch (Exception e)
            { return null; }

            return tb;
        }

        public static DataTable ReadExcelTable(string _path, string _tableName)
        {
            DataTable tb = new DataTable();

            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _path + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
            try
            {
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    string query = $"SELECT * FROM [{_tableName}]";
                    OleDbDataAdapter olead_adapter = new OleDbDataAdapter(query, conn);
                    olead_adapter.Fill(tb);
                }
            }
            catch { return null; }

            return tb;
        }

        public static int Excel_GetCellNumber(DataRow _row, int _col)
        {
            string qtyText = Excel_GetCellText(_row, _col);
            if (qtyText == null)
                return 0;

            int dot = qtyText.IndexOf('.');
            if (dot > -1)
                qtyText = qtyText.Remove(dot, qtyText.Length - 1);

            Console.WriteLine("qtyText:" + qtyText);
            return Convert.ToInt32(qtyText);
        }

        public static string Excel_GetCellText(DataRow _row, int _col)
        {
            object CurrentCell = _row[_col];
            string cellText = string.Empty;
            if (CurrentCell != null)
            {
                cellText = CurrentCell.ToString().Trim().ToUpper();
            }
            return cellText;
        }
    }
}
