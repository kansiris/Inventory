using Inventory.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Service
{
  public  class VendorService
    {
        
        #region VendorInsertRow
        public static int VendorInsertRow(string Contact_Person1Fname, long Mobile_No,
           long Bank_Acc_Number, long Paytym_Number, string Company_Name, string Contact_Person1Lname, long LandLine_Num,
            string Bank_Name, string Bank_Branch, byte[] Logo, string Remarks, string Contact_Person2Lname, string Contact_Person2Fname,
            string Email, string Adhar_Number)
        {
            return VendorRepository.VendorInsertRow( Contact_Person1Fname, Mobile_No, Bank_Acc_Number,
                Paytym_Number, Company_Name, Contact_Person1Lname, LandLine_Num, Bank_Name, Bank_Branch, Logo, Remarks, Contact_Person2Lname,
                Contact_Person2Fname, Email, Adhar_Number);
        }


        #endregion

        #region VendorAddressInsertRow
        public static int VendorAddressInsertRow(string Job_position, string Note, string bill_street, string bill_city, string bill_state, string bill_postalcode,
            string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            return VendorRepository.VendorAddressInsertRow(Job_position, Note, bill_street, bill_city, bill_state, bill_postalcode,
                bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country);
        }


        #endregion
    }
}
