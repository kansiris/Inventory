using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;


namespace DAL
{
    public class Items
    {
        private static string ConnectionString;
        #region ItemsSelectAll
        public static SqlDataReader ItemsSelectAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, "ItemsSelectAllRows");
        }
        #endregion
        #region ItemsSelectRow
        public static SqlDataReader ItemsSelectRow(string SKU)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "ItemsSelectRow", SKU);
        }
        #endregion
        #region ItemsInsertRow
        public static void ItemsInsertRow(string SKU, string Item_Name, string Short_Description, string Long_Description, string Quantity, string UnitOfMeasure_Id, int Perishable, string StockIn_Hand, string Reoredr_Level, string Item_Image, string Cost_Price, string Selling_Price, string Bar_Code, int MinimumBeforeOrder, string Remarks)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "ItemsInsertRow", SKU, Item_Name, Short_Description, Long_Description, Quantity, UnitOfMeasure_Id, Perishable, StockIn_Hand, Reoredr_Level, Item_Image, Cost_Price, Selling_Price, Bar_Code, MinimumBeforeOrder, Remarks);
        }
        #endregion
        #region ItemsUpdateRow
        public static void ItemsUpdateRow(string SKU, string Item_Name, string Short_Description, string Long_Description, string Quantity, string UnitOfMeasure_Id, int Perishable, string StockIn_Hand, string Reoredr_Level, string Item_Image, string Cost_Price, string Selling_Price, string Bar_Code, int MinimumBeforeOrder, string Remarks)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "ItemsUpdateRow", SKU, Item_Name, Short_Description, Long_Description, Quantity, UnitOfMeasure_Id, Perishable, StockIn_Hand, Reoredr_Level, Item_Image, Cost_Price, Selling_Price, Bar_Code, MinimumBeforeOrder, Remarks);
        }
        #endregion
        #region ItemsDeleteRow
        public static void ItemsDeleteRow(string SKU)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "ItemsDeleteRow", SKU);
        }
        #endregion

    }
}
