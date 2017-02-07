using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;


namespace Inventory.Repository
{
    public class Invoice
    {
        private static string ConnectionString;
        #region InvoiceSelectAll
        public static SqlDataReader InvoiceSelectAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, "InvoiceSelectAllRows");
        }
        #endregion
        #region InvoiceSelectRow
        public static SqlDataReader InvoiceSelectRow(string Invoice_Number)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "InvoiceSelectRow", Invoice_Number);
        }
        #endregion
        #region InvoiceInsertRow
        public static void InvoiceInsertRow(string Invoice_Number, string Amount, string Staff_Id, string Vendor_Id, string Invoice_Date, string Invoice_copy, int Payment_Id, string Remarks)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "InvoiceInsertRow", Invoice_Number, Amount, Staff_Id, Vendor_Id, Invoice_Date, Invoice_copy, Payment_Id, Remarks);
        }
        #endregion
        #region InvoiceUpdateRow
        public static void InvoiceUpdateRow(string Invoice_Number, string Amount, string Staff_Id, string Vendor_Id, string Invoice_Date, string Invoice_copy, int Payment_Id, string Remarks)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "InvoiceUpdateRow", Invoice_Number, Amount, Staff_Id, Vendor_Id, Invoice_Date, Invoice_copy, Payment_Id, Remarks);
        }
        #endregion
        #region InvoiceDeleteRow
        public static void InvoiceDeleteRow(string Invoice_Number)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "InvoiceDeleteRow", Invoice_Number);
        }
        #endregion

    }
}