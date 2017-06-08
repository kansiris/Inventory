using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using Inventory.Utility;

namespace Inventory.Repository
{
    public class InvoiceRepository
    {
        private static string ConnectionString;
        public static SqlDataReader AvailablePos(string dbname, string cid)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "availablePos", cid);
        }

        public static SqlDataReader GetPodata(string dbname, string cid,string Prchaseorder_no)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "getpodatabasedonponum", cid, Prchaseorder_no);
        }

    }
}