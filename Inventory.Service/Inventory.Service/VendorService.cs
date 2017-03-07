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


        #region CompanyInsertRow
        public static int CompanyInsertRow(string Company_name, int Bank_Acc_Number, string Bank_Name, string Bank_Branch,
                                              int Paytym_Number, string Email, byte[] Logo)
        {
            return VendorRepository.CompanyInsertRow(Company_name, Bank_Acc_Number, Bank_Name, Bank_Branch, Paytym_Number, Email, Logo);
        }


        #endregion


        #region VendorInsertRow
        public static int VendorInsertRow(string Contact_PersonFname, string Contact_PersonLname, long Mobile_No, long LandLine_Num,
                         string Remarks, string Email, string Adhar_Number, string Job_position)
        {
            return VendorRepository.VendorInsertRow(Contact_PersonFname, Contact_PersonLname, Mobile_No, LandLine_Num, Remarks, Email, Adhar_Number, Job_position);
        }


        #endregion

        #region VendorAddressInsertRow
        public static int VendorAddressInsertRow(string bill_street, string bill_city, string bill_state, string bill_postalcode,
            string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            return VendorRepository.VendorAddressInsertRow(bill_street, bill_city, bill_state, bill_postalcode,
                bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country);
        }

        #endregion

        #region UpdateCompany
        public static int UpdateCompany(int company_Id, string company_name, int Bank_Acc_Number, string Bank_Name, string Bank_Branch, int Paytym_Number, string email, byte[] logo)
        {

            return VendorRepository.UpdateCompany(company_Id, company_name, Bank_Acc_Number, Bank_Name,
                Bank_Branch, Paytym_Number, email, logo);
        }

        #endregion
    }
}
