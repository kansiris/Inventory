using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;


namespace Inventory.Repository
{
    public class Payment
    {
        private static string ConnectionString;
        #region PaymentSelectAll
        public static SqlDataReader PaymentSelectAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, "PaymentSelectAllRows");
        }
        #endregion
        #region PaymentSelectRow
        public static SqlDataReader PaymentSelectRow(string Payment_Id)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "PaymentSelectRow", Payment_Id);
        }
        #endregion
        #region PaymentInsertRow
        public static void PaymentInsertRow(string Payment_Method, string Payment_Amount, string Description, string Remarks, string Payment_Date, string Payment_Id)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "PaymentInsertRow", Payment_Method, Payment_Amount, Description, Remarks, Payment_Date, Payment_Id);
        }
        #endregion
        #region PaymentUpdateRow
        public static void PaymentUpdateRow(string Payment_Method, string Payment_Amount, string Description, string Remarks, string Payment_Date, string Payment_Id)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "PaymentUpdateRow", Payment_Method, Payment_Amount, Description, Remarks, Payment_Date, Payment_Id);
        }
        #endregion
        #region PaymentDeleteRow
        public static void PaymentDeleteRow(string Payment_Id)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "PaymentDeleteRow", Payment_Id);
        }
        #endregion

    }
}