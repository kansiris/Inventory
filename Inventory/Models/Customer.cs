using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Customer
    {
        public int cus_company_Id { get; set; }
        
            public string payment_due_date { get; set; }
        public string cus_company_name { get; set; }

        public string status { get; set; }

        public string cus_email { get; set; }

        public string cus_Note { get; set; }

        public string cus_logo { get; set; }

        public string Customer_Id { get; set; }

        public string Customer_contact_Fname { get; set; }

        public string Customer_contact_Lname { get; set; }

        public string Email_Id { get; set; }

        public string Adhar_Number { get; set; }
        public string GSTIN_Number { get; set; }


        public string cus_Job_position { get; set; }

        public string country { get; set; }

        public string Mobile_No { get; set; }

        public string image { get; set; }

        public string Bank_Acc_Number { get; set; }

        public int Paytym_Number { get; set; }

        public string Bank_Name { get; set; }

        public string Bank_Branch { get; set; }

        public string IFSC_No { get; set; }

        public string bill_street { get; set; }

        public string bill_city { get; set; }

        public string bill_state { get; set; }
        public string bill_postalcode { get; set; }

        public string bill_country { get; set; }

        public string ship_street { get; set; }
        public string ship_city { get; set; }

        public string ship_state { get; set; }

        public string ship_postalcode { get; set; }
        public string ship_country { get; set; }

        public string activationcode { get; set; }
        public string tax_reg_no { get; set; }
        public string pan_no { get; set; }
        public int tds_apply { get; set; }
        public int tax_exemption { get; set; }
        public string tax_files { get; set; }
        public string new_POs { get; set; }
        public string total_POs { get; set; }
        public string due { get; set; }
        public string overdue { get; set; }

    }
}