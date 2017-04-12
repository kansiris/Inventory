using Inventory.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Service
{
    public class CustomerService
    {
        public static SqlDataReader getcuscomapnies(string dbname)
        {
            return CustomerRepository.getcuscomapnies(dbname);
        }

    }
}
