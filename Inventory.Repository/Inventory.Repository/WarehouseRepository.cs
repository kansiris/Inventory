using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;


namespace Inventory.Repository
{
   public class WarehouseRepository
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();
        private static string ConnectionString1 = ConfigurationManager.ConnectionStrings["DbConnection1"].ToString();

        public static int warehouseinsert(string Wh_Name, string wh_Shortname)
        {
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "insertwarehouse", Wh_Name, wh_Shortname);
            return count;
        }
    }
}
