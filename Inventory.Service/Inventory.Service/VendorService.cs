using Inventory.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Service
{
    public class VendorService
    {
        public static SqlDataReader getcomapnies(string dbname)
        {
            return VendorRepository.getcomapnies(dbname);
        }

        #region CompanyInsertRow
        public static int CompanyInsertRow(string Company_name, string Email,string logo, string dbname)
        {

            return VendorRepository.CompanyInsertRow(Company_name, Email,logo, dbname);
        }

        #endregion

        #region VendorInsertRow
        public static int VendorInsertRow(int company_Id, string Contact_PersonFname, string Contact_PersonLname, string Mobile_No,
                          string emailid, string Adhar_Number, string Job_position,string image, string dbname)
        {
            return VendorRepository.VendorInsertRow(company_Id, Contact_PersonFname, Contact_PersonLname, Mobile_No, emailid, Adhar_Number, Job_position,image, dbname);
        }

        #endregion

        public static int VendorUpdateContact(string Vendor_Id, string Contact_PersonFname, string Contact_PersonLname, string Mobile_No,
                      string emailid, string Adhar_Number, string Job_position,string image, string dbname)
        {
            return VendorRepository.VendorUpdateContact(Vendor_Id, Contact_PersonFname, Contact_PersonLname, Mobile_No, emailid, Adhar_Number, Job_position,image, dbname);
        }
        #region VendorAddressInsertRow
        public static int VendorAddressInsertRow(int company_Id, string bill_street, string bill_city, string bill_state, string bill_postalcode,
            string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country, string dbname)
        {
            return VendorRepository.VendorAddressInsertRow(company_Id, bill_street, bill_city, bill_state, bill_postalcode,
                bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country, dbname);
        }

        #endregion
        public static int VendorAddressupdateRow(int company_Id, string bill_street, string bill_city, string bill_state, string bill_postalcode,
            string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country, string dbname)
        {
            return VendorRepository.VendorAddressUpdateRow(company_Id, bill_street, bill_city, bill_state, bill_postalcode,
                bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country, dbname);
        }
        #region UpdateCompany
        public static int UpdateCompany(int company_Id, string Bank_Acc_Number, string Bank_Name, string Bank_Branch, string IFSC_No, string dbname)
        {

            return VendorRepository.UpdateCompany(company_Id, Bank_Acc_Number, Bank_Name, Bank_Branch, IFSC_No, dbname);
        }

        #endregion

        public static int UpdateNotes(int company_Id, string Note, string dbname)
        {

            return VendorRepository.UpdateNotes(company_Id, Note, dbname);
        }
        public static SqlDataReader getcompanyId(string Company_Name,string dbname)
        {
            return VendorRepository.getcompanyId(Company_Name,dbname);
        }
        public static int getcompanyIdlatest(string dbname)
        {
            return VendorRepository.getcompanyIdlatest(dbname);
        }
        public static SqlDataReader getvendorId(string dbname)
        {
            return VendorRepository.getvendorId(dbname);
        }

        public static SqlDataReader getlastinsertedcompany(int company_Id, string dbname)
        {
            return VendorRepository.getlastinsertedcompany(company_Id,dbname);
        }
        public static SqlDataReader getcontactdetail(int company_Id,string dbname)
        {
            return VendorRepository.getcontactdetail(company_Id,dbname);
        }

        public static SqlDataReader getAllDetails(int company_Id, string dbname)
        {
            return VendorRepository.getAllDetails(company_Id,dbname);
        }
        public static int UpdateCompany1(int company_Id, string Company_Name, string Email,string logo, string dbname)
        {

            return VendorRepository.UpdateCompany1(company_Id, Company_Name, Email, logo, dbname);
        }
        public static int deleteRecord(int company_Id,string status, string dbname)
        {

            return VendorRepository.deleteRecord(company_Id,status, dbname);
        }
        
            public static int deleteVendor(string Vendor_Id, string dbname)
        {

            return VendorRepository.deleteVendor(Vendor_Id, dbname);
        }

        public static SqlDataReader getVendorContact(string Vendor_Id, string dbname)
        {
            return VendorRepository.getVendorContact(Vendor_Id, dbname);
        }
        public static SqlDataReader checkcompany1(string Company_Name, string dbname)
        {
            return VendorRepository.checkcompany1(Company_Name, dbname);

        }
        public static SqlDataReader getusermaster(string id, string dbname)
        {
            return VendorRepository.getusermaster(id, dbname);
        }
        //for job_Position
        public static int insertjobposition(string Job_position, int company_Id,string dbname)
        {

            return VendorRepository.insertJobposition(Job_position, company_Id, dbname);
        }
        public static SqlDataReader getjobposition(string Job_position,string dbname)
        {
            return VendorRepository.getJobposition(Job_position,dbname);
        }
        public static SqlDataReader getalljobposition(string dbname)
        {
            return VendorRepository.getallJobposition(dbname);
        }
        //company pic upload
        public static int updatecompanyprofile(int id, string companylogo, string dbname)
        {
            return VendorRepository.updatecompanyprofile(id, companylogo,dbname);
        }
        
    }
}
