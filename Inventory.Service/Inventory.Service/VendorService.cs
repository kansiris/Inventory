using Inventory.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Service
{
  public  class VendorService
    {
        public static SqlDataReader getcomapnies()
        {
            return VendorRepository.getcomapnies();
        }

        #region CompanyInsertRow
        public static int CompanyInsertRow(string Company_name,string Email)
        {
            
            return VendorRepository.CompanyInsertRow(Company_name, Email);
        }
        
        #endregion
        
        #region VendorInsertRow
        public static int VendorInsertRow(int company_Id,string Contact_PersonFname, string Contact_PersonLname, long Mobile_No, 
                          string Email, string Adhar_Number, string Job_position)
        {
            return VendorRepository.VendorInsertRow(company_Id,Contact_PersonFname, Contact_PersonLname, Mobile_No, Email, Adhar_Number, Job_position);
        }

        #endregion

            public static int VendorUpdateContact(int company_Id, string Contact_PersonFname, string Contact_PersonLname, long Mobile_No, 
                          string Email, string Adhar_Number, string Job_position)
        {
            return VendorRepository.VendorInsertRow(company_Id, Contact_PersonFname, Contact_PersonLname, Mobile_No,  Email, Adhar_Number, Job_position);
        }
        #region VendorAddressInsertRow
        public static int VendorAddressInsertRow(int company_Id,string bill_street, string bill_city, string bill_state, string bill_postalcode,
            string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            return VendorRepository.VendorAddressInsertRow(company_Id,bill_street, bill_city, bill_state, bill_postalcode,
                bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country);
        }

        #endregion
        public static int VendorAddressupdateRow(int company_Id, string bill_street, string bill_city, string bill_state, string bill_postalcode,
            string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            return VendorRepository.VendorAddressInsertRow(company_Id, bill_street, bill_city, bill_state, bill_postalcode,
                bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country);
        }
        #region UpdateCompany
        public static int UpdateCompany(int company_Id,int Bank_Acc_Number, string Bank_Name, string Bank_Branch, int Paytym_Number, string email)
        {

            return VendorRepository.UpdateCompany(company_Id, Bank_Acc_Number, Bank_Name,Bank_Branch, Paytym_Number, email);
        }

        #endregion
        
            public static int UpdateNotes(int company_Id, string notes)
        {

            return VendorRepository.UpdateNotes(company_Id, notes);
        }
        public static SqlDataReader getcompanyId()
        {
            return VendorRepository.getcompanyId();
        }
        public static SqlDataReader getvendorId()
        {
            return VendorRepository.getvendorId();
        }
        public static SqlDataReader Authenticateemail(string check, string Email_ID)
        {
            return VendorRepository.Authenticateemail(check, Email_ID);
        }
       
            public static SqlDataReader getlastinsertedcompany(int company_Id)
        {
            return VendorRepository.getlastinsertedcompany(company_Id);
        }

    }
}
