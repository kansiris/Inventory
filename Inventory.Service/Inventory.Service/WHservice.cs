using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Repository;
using System.Data.SqlClient;

namespace Inventory.Repository
{
    public class WHservice
    {
        public static int warehouseinsert(string Wh_Name, string wh_Shortname)
        {
            return WarehouseRepository.warehouseinsert(Wh_Name, wh_Shortname);
        }
    }
}
