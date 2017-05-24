using Inventory.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Service
{
    public class CustomerService
    {
        public static SqlDataReader getcuscomapnies(string dbname)
        {
            return CustomerRepository.getcuscomapnies(dbname);
        }
        #region CustomerCompanyInsertRow
        public static int CustomerCompanyInsertRow(string cus_company_name, string cus_email, string cus_logo, string dbname)
        {

            return CustomerRepository.CustomerCompanyInsertRow(cus_company_name, cus_email, cus_logo, dbname);
        }

        #endregion

        #region CustomerInsertRow
        public static int CustomerInsertRow(int cus_company_Id, string Customer_contact_Fname, string Customer_contact_Lname, string Mobile_No,
                          string Email_Id, string Adhar_Number, string cus_Job_position, string image, string dbname)
        {
            return CustomerRepository.CustomerInsertRow(cus_company_Id, Customer_contact_Fname, Customer_contact_Lname, Mobile_No, Email_Id, Adhar_Number, cus_Job_position,image,dbname);
        }

        #endregion

        public static int CustomerUpdateContact(string Customer_Id, string Customer_contact_Fname, string Customer_contact_Lname, string Mobile_No,
                          string Email_Id, string Adhar_Number, string cus_Job_position, string image, string dbname)
        {
            return CustomerRepository.CustomerUpdateContact(Customer_Id, Customer_contact_Fname, Customer_contact_Lname, Mobile_No, Email_Id, Adhar_Number, cus_Job_position, image, dbname);
        }
        #region CustomerAddressInsertRow
        public static int CustomerAddressInsertRow(int cus_company_Id, string bill_street, string bill_city, string bill_state, string bill_postalcode,
            string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country, string dbname)
        {
            return CustomerRepository.CustomerAddressInsertRow(cus_company_Id, bill_street, bill_city, bill_state, bill_postalcode,
                bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country, dbname);
        }

        #endregion
        public static int CustomerAddressUpdateRow(int cus_company_Id, string bill_street, string bill_city, string bill_state, string bill_postalcode,
            string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country, string dbname)
        {
            return CustomerRepository.CustomerAddressUpdateRow(cus_company_Id, bill_street, bill_city, bill_state, bill_postalcode,
                bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country, dbname);
        }
        #region UpdatecusCompany
        public static int UpdatecusCompany(int company_Id, string Bank_Acc_Number, string Bank_Name, string Bank_Branch, string IFSC_No, string dbname)
        {

            return CustomerRepository.UpdatecusCompany(company_Id, Bank_Acc_Number, Bank_Name, Bank_Branch, IFSC_No, dbname);
        }

        #endregion

        public static int UpdatecusNotes(int cus_company_Id, string cus_Note, string dbname)
        {

            return CustomerRepository.UpdatecusNotes(cus_company_Id, cus_Note, dbname);
        }
        public static SqlDataReader getcuscompanyId(string cus_company_name, string dbname)
        {
            return CustomerRepository.getcuscompanyId(cus_company_name, dbname);
        }
        public static int getcuscompanyIdlatest(string dbname)
        {
            return CustomerRepository.getcuscompanyIdlatest(dbname);
        }
        public static SqlDataReader getCustomerId(string dbname)
        {
            return CustomerRepository.getCustomerId(dbname);
        }

        public static SqlDataReader getlastinsertedcompany(int cus_company_Id, string dbname)
        {
            return CustomerRepository.getlastinsertedcompany(cus_company_Id, dbname);
        }
        public static SqlDataReader getcuscontactdetail(int cus_company_Id, string dbname)
        {
            return CustomerRepository.getcuscontactdetail(cus_company_Id, dbname);
        }

        public static SqlDataReader getAllcusDetails(int cus_company_Id, string dbname)
        {
            return CustomerRepository.getAllcusDetails(cus_company_Id, dbname);
        }
        public static int UpdatecusCompany1(int cus_company_Id, string cus_company_name, string cus_email, string cus_logo, string dbname)
        {

            return CustomerRepository.UpdatecusCompany1(cus_company_Id, cus_company_name, cus_email, cus_logo, dbname);
        }
        public static int deletecuscompRecord(int cus_company_Id,string status, string dbname)
        {

            return CustomerRepository.deletecuscompRecord(cus_company_Id, status, dbname);
        }

        public static int deleteCustomer(string Customer_Id,string status ,string dbname)
        {

            return CustomerRepository.deleteCustomer(Customer_Id,status ,dbname);
        }

        public static SqlDataReader getCustomerContact(string Customer_Id, string dbname)
        {
            return CustomerRepository.getCustomerContact(Customer_Id, dbname);
        }
        public static SqlDataReader checkcuscompany1(string cus_company_name, string dbname)
        {
            return CustomerRepository.checkcuscompany1(cus_company_name, dbname);

        }
        public static SqlDataReader getusermaster(string id, string dbname)
        {
            return CustomerRepository.getusermaster(id, dbname);
        }
        //for job_Position
        public static int insertcusJobposition(string cus_Job_position, int cus_company_Id, string dbname)
        {

            return CustomerRepository.insertcusJobposition(cus_Job_position, cus_company_Id, dbname);
        }
        public static SqlDataReader getcusJobposition(string cus_Job_position, string dbname)
        {
            return CustomerRepository.getcusJobposition(cus_Job_position, dbname);
        }
        public static SqlDataReader getallcusJobposition(string dbname)
        {
            return CustomerRepository.getallcusJobposition(dbname);
        }
        //company pic upload
        public static int updatecuscompanyprofile(int id, string cus_logo, string dbname)
        {
            return CustomerRepository.updatecuscompanyprofile(id, cus_logo, dbname);
        }
        

        //for products purpose
             public static SqlDataReader getAllDetailsByCompany_Id(string Customer_Id, string dbname)
        {
            return CustomerRepository.getAllDetailsByCompany_Id(Customer_Id, dbname);
        }

    }
}
