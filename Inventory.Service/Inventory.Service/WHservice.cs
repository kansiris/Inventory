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

        public static int WHaddressinsert(string jobposition, int phone, int mobile, string Email, string conperson, string Note, string Billing_Address, string Shipping_Address, string Other_Address)
        {
            return WarehouseRepository.WHaddressinsert(jobposition, phone,mobile, Email, conperson, Note, Billing_Address, Shipping_Address, Other_Address);
        }
    }
}
