using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Repository;
using System.Data.SqlClient;


namespace Inventory.Service
{
    public class LoginService
    {
        #region UserSelectRow
        public static SqlDataReader UserSelectRow(string Email_ID, string Password)
        {
            return LoginRepository.UserSelectRow(Email_ID, Password);
        }
        #endregion
        #region UserInsertRow
        public static int UserInsertRow(string User_Type, string Description, string User_Id, string Remarks, string User_FName, string User_LName, string Email_ID, string Password, string Company_ID)
        {
            return LoginRepository.UserInsertRow(User_Type, Description, User_Id, Remarks, User_FName, User_LName, Email_ID, Password, Company_ID);
        }
        #endregion
        #region UserUpdateRow
        public static int UserUpdateRow(string User_Type, string Description, string User_Id, string Remarks, string User_FName, string User_LName, string Email_ID, string Password, string Company_ID)
        {
            return LoginRepository.UserUpdateRow(User_Type, Description, User_Id, Remarks, User_FName, User_LName, Email_ID, Password, Company_ID);
        }
        #endregion

        #region Authenticateuser
        public static SqlDataReader Authenticateuser(string check, string Email_ID, string Password,string site, long usertypeid)
        {
            return LoginRepository.Authenticateuser(check,Email_ID, Password,site,usertypeid);
        }
        #endregion

        #region CreateUser
        public static int CreateUser(string EmailId, string First_Name, string Last_Name, string DB_Name,  DateTime Created_Date, string Password, int SubscriptionId, int UserTypeId, string User_Site, string CompanyName, string Phone, DateTime? SubscriptionDate, int IsActive, string activationcode,
            string Profile_Picture,
            string Date_Format, string Timezone, string Currency)
        {
            int count = LoginRepository.CreateUser(EmailId, First_Name, Last_Name, DB_Name, Created_Date, Password, SubscriptionId, UserTypeId, User_Site, CompanyName, Phone, SubscriptionDate, IsActive, activationcode, Profile_Picture, Date_Format, Timezone, Currency);
            return count;
        }
        #endregion

        #region GetUserType
        public static object GetUserTypeId(string type,long id)
        {
            return LoginRepository.GetUserTypeId(type,id);
        }
        #endregion

        #region getsubscriptionid
        public static object getsubscriptionid(string type)
        {
            return LoginRepository.getsubscriptionid(type);
        }
        #endregion

        #region EmailActivation
        public static int ActivateEmail(string email, string activationcode)
        {
            return LoginRepository.ActivateEmail(email, activationcode);
        }
        #endregion
        #region EmailActivations
        public static int ActivatesEmail(string email,string activationcode, string DB_Name)
        {
            return LoginRepository.ActivatesEmail(email,activationcode, DB_Name);
            
        }
        #endregion

        #region getuserrecord
        public static SqlDataReader getuserrecord(string email, string code)
        {
            return LoginRepository.getuserrecord(email, code);
        }
        #endregion
        #region getusersite
        public static SqlDataReader getusersite(string site)
        {
            return LoginRepository.getusersite(site);

        }
        #endregion

        #region timezone
        public static int updatetimezone(string dateformat, string timezone, string id)
        {
            return LoginRepository.updatetimezone(dateformat, timezone, id);
        }
        #endregion

        #region ProfileProgress
        public static SqlDataReader GetProfileProgress(string dbname)
        {
            return LoginRepository.GetProfileProgress(dbname);
        }
        #endregion

        #region getuserprofile
        public static SqlDataReader GetUserProfile(int id)
        {
            return LoginRepository.GetUserProfile(id);
        }
        #endregion

        #region Update User Address
        public static int updateuseraddress(int userid, string Line1, string Line2, string city, string state, string postalcode, string country)
        {
            return LoginRepository.updateuseraddress(userid, Line1, Line2, city, state, postalcode, country);
        }
        #endregion

        #region Update Company Address
        public static int updatecompanyaddress(int userid, string Line1, string Line2, string city, string state, string postalcode, string country)
        {
            return LoginRepository.updatecompanyaddress(userid, Line1, Line2, city, state, postalcode, country);
        }
        #endregion
    }
}
