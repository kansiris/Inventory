using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;


namespace Inventory.Repository
{
    public class InvoiceDetails
    {
        private static string ConnectionString;
        #region InvoiceDetailsSelectAll
        public static SqlDataReader InvoiceDetailsSelectAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, "InvoiceDetailsSelectAllRows");
        }
        #endregion
        #region InvoiceDetailsSelectRow
        public static SqlDataReader InvoiceDetailsSelectRow()
        {
            return SqlHelper.ExecuteReader(ConnectionString, "InvoiceDetailsSelectRow");
        }
        #endregion
        #region InvoiceDetailsInsertRow
        public static void InvoiceDetailsInsertRow(string Invoice_Number, string SKU, string Quantity, string Price, string Remarks)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "InvoiceDetailsInsertRow", Invoice_Number, SKU, Quantity, Price, Remarks);
        }
        #endregion
        #region InvoiceDetailsUpdateRow
        public static void InvoiceDetailsUpdateRow(string Invoice_Number, string SKU, string Quantity, string Price, string Remarks)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "InvoiceDetailsUpdateRow", Invoice_Number, SKU, Quantity, Price, Remarks);
        }
        #endregion
        #region InvoiceDetailsDeleteRow
        public static void InvoiceDetailsDeleteRow()
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "InvoiceDetailsDeleteRow");
        }
        #endregion

    }
}