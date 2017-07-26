using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Utility
{
    public class GetConnectionString
    {
        public string CustomizeConnectionString(string DbName)
        {
            string sqlConnectionString = @"Integrated Security=False;Initial Catalog=" + DbName + ";Data Source=192.168.0.62;User ID=user_inv;Password=user1234;"; // for local
            //string sqlConnectionString = @"Integrated Security=False;Initial Catalog="+ DbName + ";Data Source=183.82.97.220;User ID=user_inv;Password=user1234;"; //for server
            return sqlConnectionString;
        }
    }
}
