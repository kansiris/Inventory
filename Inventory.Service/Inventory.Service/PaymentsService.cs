using Inventory.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Service
{
    public class PaymentsService
    {
        //AvailableInvoices
        public static SqlDataReader InvoicesForPayment(string dbname, string cid)
        {
            return PaymentsRepository.InvoicesForPayment(dbname, cid);
        }
    }
}
