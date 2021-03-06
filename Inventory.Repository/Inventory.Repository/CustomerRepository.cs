﻿using Inventory.Utility;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repository
{
    public class CustomerRepository
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();
        private static string ConnectionString1 = ConfigurationManager.ConnectionStrings["DbConnection1"].ToString();

        public static SqlDataReader getcuscomapnies(string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "getcuscompany");
        }


        #region CustomerCompanyInsertRow
        public static int CustomerCompanyInsertRow(string cus_company_name, string cus_email, string cus_logo, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "insertCustomer_Company", cus_company_name, cus_email, cus_logo);
            return count;
        }
        #endregion

        #region CustomerInsertRow
        public static int CustomerInsertRow(int cus_company_Id, string Customer_contact_Fname, string Customer_contact_Lname, string Mobile_No,
                          string Email_Id, string Adhar_Number, string cus_Job_position, string image, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "insertcustomer", cus_company_Id, Customer_contact_Fname, Customer_contact_Lname, Mobile_No, Email_Id, Adhar_Number, cus_Job_position, image);
            return count;
        }

        public static int CustomerUpdateContact(string Customer_Id, string Customer_contact_Fname, string Customer_contact_Lname, string Mobile_No,
                          string Email_Id, string Adhar_Number, string cus_Job_position, string image, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "updatecustomer", Customer_Id, Customer_contact_Fname, Customer_contact_Lname, Mobile_No, Email_Id, Adhar_Number, cus_Job_position, image);
            return count;
        }
        #endregion
        #region CustomerAddressInsertRow

        public static int CustomerAddressInsertRow(int cus_company_Id, string bill_street, string bill_city, string bill_state, string bill_postalcode,
            string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country, string dbname)
        {
            int count1 = SqlHelper.ExecuteNonQuery(ConnectionString, "insertCustomer_address",cus_company_Id, bill_street, bill_city, bill_state, bill_postalcode,
                bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country);
            return count1;
        }
        #endregion
        public static int CustomerAddressUpdateRow(int cus_company_Id, string bill_street, string bill_city, string bill_state, string bill_postalcode,
            string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            int count1 = SqlHelper.ExecuteNonQuery(ConnectionString, "updateCustomer_address", cus_company_Id, bill_street, bill_city, bill_state, bill_postalcode,
                bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country);
            return count1;
        }
        #region UpdatecusCompany
        public static int UpdatecusCompany(int company_Id, string Bank_Acc_Number, string Bank_Name, string Bank_Branch, string IFSC_No, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "updatecusCompany", company_Id, Bank_Acc_Number, Bank_Name, Bank_Branch, IFSC_No);
            return count;
        }
        #endregion
        public static int UpdatecusNotes(int cus_company_Id, string cus_Note, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "updatecusNotes", cus_company_Id, cus_Note);
            return count;
        }
        public static int getcuscompanyIdlatest(string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "getMaxcusCompanyid");
            return count;
        }
        public static SqlDataReader getcuscompanyId(string cus_company_name, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "getMaxcusCompanyid", cus_company_name);
        }
        public static SqlDataReader getCustomerId(string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "getCustomerId");
        }

        public static SqlDataReader getlastinsertedcompany(int cus_company_Id, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "getLastInsertedcuscompany", cus_company_Id); //getLastInsertedcompany
        }

        public static SqlDataReader getcuscontactdetail(int cus_company_Id, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "getContactBycusCompany", cus_company_Id);

        }

        public static SqlDataReader getAllcusDetails(int cus_company_Id, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "getAllDetailsBycus_company_Id", cus_company_Id);

        }

        public static SqlDataReader getCustomerContact(string Customer_Id, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "getcustomercontact", Customer_Id);
        }


        public static int UpdatecusCompany1(int cus_company_Id, string cus_company_name, string cus_email, string cus_logo, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "updatecuscompany1", cus_company_Id, cus_company_name, cus_email, cus_logo);
            return count;
        }

        public static int deletecuscompRecord(int cus_company_Id,string status, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "deletecuscompRecord", cus_company_Id, status);
            return count;
        }

        public static int deleteCustomer(string Customer_Id,string status, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "deleteCustomer", Customer_Id,status);
            return count;
        }

        public static SqlDataReader checkcuscompany1(string cus_company_name, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "checkCustomer_Company", cus_company_name);

        }

        public static SqlDataReader getusermaster(string id, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString1, "getdatabyId", id);
        }

        public static int insertcusJobposition(string cus_Job_position, int cus_company_Id, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "insertcusjobposition", cus_Job_position, cus_company_Id);
            return count;
        }
        public static SqlDataReader getcusJobposition(string cus_Job_position, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "getcusjobposition", cus_Job_position);
        }
        public static SqlDataReader getallcusJobposition(string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "getallcusjobposition");
        }
        //company pic upload
        public static int updatecuscompanyprofile(int id, string cus_logo, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "cus_CompanyProfilePic", id, cus_logo);
            return count;
        }

        public static SqlDataReader getAllDetailsByCompany_Id(string dbname , string Customer_Id)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "customerforPO", Customer_Id);
        }

        //for customer tax details
        public static int Updatecustax(int cus_company_Id,string Adhar_Number,string GSTIN_Number ,string tax_reg_no, string pan_no, int tds_apply, int tax_exemption, string tax_files,string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "updatecus_taxdetails", cus_company_Id, Adhar_Number, GSTIN_Number, tax_reg_no, pan_no, tds_apply, tax_exemption, tax_files);
            return count;
        }
        //insertcus_taxdetails
        public static int insertcustaxdetails(int cus_company_Id,string Adhar_Number,string GSTIN_Number, string tax_reg_no, string pan_no, int tds_apply, int tax_exemption, string tax_files, string dbname)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "insertcus_taxdetails", cus_company_Id, Adhar_Number, GSTIN_Number, tax_reg_no, pan_no, tds_apply, tax_exemption, tax_files);
            return count;
        }

        //to get invoice paymentdue date based on customer id
        public static SqlDataReader getPaymentduedatebyCompany_Id(string dbname, string cus_company_Id)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "paymentduedate", cus_company_Id);
        }

    }
}
