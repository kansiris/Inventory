using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;


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
        public static int CreateUser(string EmailId, string First_Name, string Last_Name, string DB_Name,  DateTime Created_Date, string Password, int SubscriptionId,int UserTypeId,string User_Site,string CompanyName,string Phone)
        {
            int count = SqlHelper.ExecuteNonQuery(ConnectionString1, "createuser", EmailId, First_Name, Last_Name, DB_Name,  Created_Date, Password, SubscriptionId, UserTypeId, User_Site, CompanyName, Phone);
            return count;
        }
        #endregion
        #region Authenticateuser
        public static SqlDataReader Authenticateuser(string check,string Email_ID, string Password,string site)
        {
            return SqlHelper.ExecuteReader(ConnectionString1, "Authenticateuser",check, Email_ID, Password,site);
            //return SqlHelper.ExecuteReader(ConnectionString, "getuser", new SqlParameter("@Email_ID", Email_ID) , new SqlParameter("@Password", Password));
        }
        #endregion
        #region GetUserType
        public static object GetUserTypeId(string type)
        {
            return SqlHelper.ExecuteScalar(ConnectionString1, "GetUserType", type);
        }
        #endregion

        #region getsubscriptionid
        public static object getsubscriptionid(string type)
        {
            return SqlHelper.ExecuteScalar(ConnectionString1, "getsubscriptionid", type);
        }
        #endregion
    }
}
