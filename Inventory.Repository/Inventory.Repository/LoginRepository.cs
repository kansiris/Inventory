using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using Inventory;


namespace Inventory.Repository
{
    public class LoginRepository
    {        
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();
        private static string ConnectionString1 = ConfigurationManager.ConnectionStrings["DbConnection1"].ToString();
        #region FranchisesSelectAll
        public static SqlDataReader FranchisesSelectAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, "FranchisesSelectAllRows");
        }
        #endregion
        #region UserSelectRow
        public static SqlDataReader UserSelectRow(string Email_ID, string Password)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "getuser", Email_ID, Password);
            //return SqlHelper.ExecuteReader(ConnectionString, "getuser", new SqlParameter("@Email_ID", Email_ID) , new SqlParameter("@Password", Password));
        }
        #endregion
        #region UserInsertRow
        public static int UserInsertRow(string User_Type, string Description, string User_Id, string Remarks, string User_FName, string User_LName, string Email_ID, string Password, string Company_ID)
        {
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "insertuser", User_Type, Description, User_Id, Remarks, User_FName, User_LName, Email_ID, Password, Company_ID);
            return count;
        }
        #endregion
        #region UserUpdateRow
        public static int UserUpdateRow(string User_Type, string Description, string User_Id, string Remarks, string User_FName, string User_LName, string Email_ID, string Password, string Company_ID)
        {
            int count = SqlHelper.ExecuteNonQuery(ConnectionString, "updateuser", User_Type, Description, User_Id, Remarks, User_FName, User_LName, Email_ID, Password, Company_ID);
            return count;
        }
        #endregion
        #region FranchisesDeleteRow
        public static void FranchisesDeleteRow(string Franchise_Id)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "FranchisesDeleteRow", Franchise_Id);
        }

        public static IDataReader FranchisesSelectRow()
        {
            throw new NotImplementedException();
        }
        #endregion


        #region CreateUser
        public static int CreateUser(string EmailId, string First_Name, string Last_Name, string DB_Name,  DateTime Created_Date, string Password, int SubscriptionId,int UserTypeId,string User_Site,string CompanyName,string Phone,DateTime? SubscriptionDate,int IsActive,string activationcode, string Profile_Picture,
            string Date_Format, string Timezone, string Currency)
        {
            int count = SqlHelper.ExecuteNonQuery(ConnectionString1, "createuser", EmailId, First_Name, Last_Name, DB_Name,  Created_Date, Password, SubscriptionId, UserTypeId, User_Site, CompanyName, Phone,SubscriptionDate,IsActive,activationcode, Profile_Picture, Date_Format, Timezone, Currency);
            return count;
        }
        #endregion
        #region Authenticateuser
        public static SqlDataReader Authenticateuser(string check,string Email_ID, string Password,string site,long usertypeid)
        {
            return SqlHelper.ExecuteReader(ConnectionString1, "Authenticateuser",check, Email_ID, Password,site,usertypeid);
            //return SqlHelper.ExecuteReader(ConnectionString, "getuser", new SqlParameter("@Email_ID", Email_ID) , new SqlParameter("@Password", Password));
        }
        #endregion
        #region GetUserType
        public static object GetUserTypeId(string type,long id)
        {
            return SqlHelper.ExecuteScalar(ConnectionString1, "GetUserType", type,id);
        }
        #endregion

        #region getsubscriptionid
        public static object getsubscriptionid(string type)
        {
            return SqlHelper.ExecuteScalar(ConnectionString1, "getsubscriptionid", type);
        }
        #endregion

        #region EmailActivation
        public static int ActivateEmail(string email, string activationcode)
        {
            int count = SqlHelper.ExecuteNonQuery(ConnectionString1, "updateuser",email,activationcode);
            return count;
        }
        #endregion
        #region EmailActivations
        public static int ActivatesEmail(string email, int usertype, DateTime? SubscriptionDate, int IsActive, string activationcode,string DB_Name)
        {
            int count = SqlHelper.ExecuteNonQuery(ConnectionString1, "updateusers", email, usertype, IsActive, SubscriptionDate, activationcode, DB_Name);
            return count;
        }
        #endregion

        #region getuserrecord
        public static SqlDataReader getuserrecord(string email, string code)
        {
            return SqlHelper.ExecuteReader(ConnectionString1, "activateuser", email, code);
        }
        #endregion

        #region timezone
        public static int updatetimezone(string dateformat, string timezone,string id)
        {
            int count = SqlHelper.ExecuteNonQuery(ConnectionString1, "updatetimezone", dateformat, timezone,id);
            return count;
        }
        #endregion
    }
}
