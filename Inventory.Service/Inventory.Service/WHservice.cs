using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Repository;
using System.Data.SqlClient;

namespace Inventory.Service
{
    public class WHservice
    {
        public static int warehouseinsert(string Wh_Name, string wh_Shortname)
        {
            return WarehouseRepository.warehouseinsert(Wh_Name, wh_Shortname);
        }

        public static int WHaddressinsert(string WH_Name, string WH_ShortName, string contactperson, string jobposition,
            long phone, long mobile, string Email, string Note, string bill_street, string bill_city, string bill_state, 
            string bill_postalcode, string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country,string dbname)
        {
            return WarehouseRepository.WHaddressinsert(WH_Name,WH_ShortName,contactperson,jobposition, 
                phone, mobile, Email,Note, bill_street, bill_city, bill_state, bill_postalcode, bill_country, 
                ship_street, ship_city, ship_state, ship_postalcode, ship_country,dbname);
        }
        public static SqlDataReader getwarehousedtls(string dbname)
        {
            return WarehouseRepository.getwarehousedtls(dbname);
        }
    }
}
