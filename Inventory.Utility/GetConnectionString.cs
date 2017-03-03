using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory;

namespace Inventory.Utility
{
    public class GetConnectionString
    {
        public string CustomizeConnectionString(string DbName)
        {
            //var user = (Inventory.Custom.CustomPrinciple)System.Web.HttpContext.Current.User;
            //string db = user.DbName;
            string sqlConnectionString = @"Integrated Security=False;Initial Catalog=" + DbName + ";Data Source=192.168.0.131;User ID=user_inv;Password=123456;"; // for local
            //string sqlConnectionString = @"Integrated Security=False;Initial Catalog="+ DbName + ";Data Source=183.82.97.220;User ID=user_inv;Password=123456;"; //for server
            return sqlConnectionString;
        }
    }
}
