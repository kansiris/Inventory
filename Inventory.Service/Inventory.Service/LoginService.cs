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
        public static int CreateUser(string EmailId, string First_Name, string Last_Name, string DB_Name,  DateTime Created_Date, string Password, int SubscriptionId, int UserTypeId, string User_Site, string CompanyName, string Phone, DateTime? SubscriptionDate, int IsActive, string activationcode)
        {
            int count = LoginRepository.CreateUser(EmailId, First_Name, Last_Name, DB_Name, Created_Date, Password, SubscriptionId, UserTypeId, User_Site, CompanyName, Phone, SubscriptionDate, IsActive, activationcode);
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
        public static int ActivateEmail(string email, int usertype, DateTime? SubscriptionDate, int IsActive, string activationcode)
        {
            return LoginRepository.ActivateEmail(email, usertype, SubscriptionDate, IsActive, activationcode);
        }
        #endregion
    }
}
