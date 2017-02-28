﻿using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repository
{
  public class VendorRepository
    {
        
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();
        private static string ConnectionString1 = ConfigurationManager.ConnectionStrings["DbConnection1"].ToString();
        #region VendorInsertRow

        public static int VendorInsertRow(string Contact_Person1Fname, long Mobile_No, string Address,
            long Bank_Acc_Number, long Paytym_Number, string Company_Name, string Contact_Person1Lname, long LandLine_Num,
            string Bank_Name, string Bank_Branch, byte[] Logo, string Remarks, string Contact_Person2Lname, string Contact_Person2Fname,
            string Email, string Adhar_Number)
        {

            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "insertvendor", Contact_Person1Fname, Mobile_No, Address, Bank_Acc_Number,
                Paytym_Number, Company_Name, Contact_Person1Lname, LandLine_Num, Bank_Name, Bank_Branch, Logo, Remarks, Contact_Person2Lname,
                Contact_Person2Fname, Email, Adhar_Number);
            return count;
        }
        #endregion
        #region VendorAddressInsertRow
        
        public static int VendorAddressInsertRow(string Job_position,string Note,string Billing_Address,string Shipping_Address, string Other_Address)
        {

            int count1 = SqlHelper.ExecuteNonQuery(ConnectionString, "insertVendor_address",Job_position,Note, Billing_Address, Shipping_Address, Other_Address);
            
            return count1;
        }
        #endregion
        
    }
}