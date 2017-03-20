﻿using Inventory.Utility;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Inventory.Repository
{
    public class VendorRepository
    {

        private static string ConnectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();
        private static string ConnectionString1 = ConfigurationManager.ConnectionStrings["DbConnection1"].ToString();


        public static SqlDataReader getcomapnies()
        {
            //GetConnectionString getConnectionString = new GetConnectionString();
            //ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "getcompany");
        }

        #region CompanyInsertRow
        public static int CompanyInsertRow(string Company_name, string Email)
        {
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "insertCompany", Company_name, Email);
            return count;
        }
        #endregion

        #region VendorInsertRow
        public static int VendorInsertRow(int company_Id,string Contact_PersonFname, string Contact_PersonLname, int Mobile_No,
                         string Email, string Adhar_Number,string Job_position)
        {
             int count = SqlHelper.ExecuteNonQuery(ConnectionString, "insertvendor",company_Id, Contact_PersonFname, Contact_PersonLname, Mobile_No,  Email, Adhar_Number, Job_position);
            return count;
        }
        
            public static int VendorUpdateContact(int company_Id, string Contact_PersonFname, string Contact_PersonLname, int Mobile_No, 
                         string Email, string Adhar_Number, string Job_position)
        {
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "updatevendor",company_Id, Contact_PersonFname, Contact_PersonLname, Mobile_No, Email, Adhar_Number, Job_position);
            return count;
        }
        #endregion
        #region VendorAddressInsertRow

        public static int VendorAddressInsertRow(int company_Id, string bill_street, string bill_city, string bill_state, string bill_postalcode,
            string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            int count1 = SqlHelper.ExecuteNonQuery(ConnectionString, "insertVendor_address", company_Id, bill_street, bill_city, bill_state, bill_postalcode,
                bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country);
            return count1;
        }
        #endregion
        public static int VendorAddressUpdateRow(int company_Id, string bill_street, string bill_city, string bill_state, string bill_postalcode,
            string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            int count1 = SqlHelper.ExecuteNonQuery(ConnectionString, "updateVendor_address", company_Id, bill_street, bill_city, bill_state, bill_postalcode,
                bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country);
            return count1;
        }
        #region UpdateCompany
        public static int UpdateCompany(long company_Id, int Bank_Acc_Number,string Bank_Name,string Bank_Branch,string IFSC_No, string email)
        {
                      int count = SqlHelper.ExecuteNonQuery(ConnectionString, "updateCompany", company_Id, Bank_Acc_Number, Bank_Name,Bank_Branch, IFSC_No, email);
            return count;
        }
        #endregion
        public static int UpdateNotes(int company_Id,string notes)
        {
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "updateNotes", company_Id, Note);
            return count;
        }

        public static SqlDataReader getcompanyId()
        {
            return SqlHelper.ExecuteReader(ConnectionString, "getMaxCompanyid");
        }
        public static SqlDataReader getvendorId()
        {
            return SqlHelper.ExecuteReader(ConnectionString, "getVendorId");
        }
        //public static SqlDataReader Authenticateemail(string check, string Email_ID)
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString1, "Authenticateemail", check, Email_ID);
        //    //return SqlHelper.ExecuteReader(ConnectionString, "getuser", new SqlParameter("@Email_ID", Email_ID) , new SqlParameter("@Password", Password));
        //}

        public static SqlDataReader getlastinsertedcompany(int company_Id)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "getLastInsertedcompany", company_Id);
        }

        public static SqlDataReader getcontactdetail(int company_Id)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "getContactByCompany", company_Id);

        }

        public static SqlDataReader getAllDetails(int company_Id)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "getAllDetailsByCompany_Id", company_Id);

        }


        public static int UpdateCompany1(int company_Id, string Company_Name, string Email)
        {
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "updatecompany1", company_Id, Company_Name, Email);
            return count;
        }

    }
}
