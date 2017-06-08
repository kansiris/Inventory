using Inventory.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Service
{
    public class InvoiceService
    {
        public static SqlDataReader AvailablePos(string dbname, string cid)
        {
            return InvoiceRepository.AvailablePos(dbname, cid);
        }

        public static SqlDataReader GetPodata(string dbname, string cid,string Prchaseorder_no)
        {
            return InvoiceRepository.GetPodata(dbname, cid, Prchaseorder_no);
        }
    }
}
