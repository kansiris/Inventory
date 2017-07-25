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
        LoginRepository loginRepository = new LoginRepository();
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
            string Profile_Picture,string Date_Format, string Timezone, string Currency, string companylogo)
        {
            int count = LoginRepository.CreateUser(EmailId, First_Name, Last_Name, DB_Name, Created_Date, Password, SubscriptionId, UserTypeId, User_Site, CompanyName, Phone, SubscriptionDate, IsActive, activationcode, Profile_Picture, Date_Format, Timezone, Currency,companylogo);
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
        //public static SqlDataReader GetUserProfile(int id)
        //{
        //    return LoginRepository.GetUserProfile(id);
        //}

        public List<GetUserProfile_Result> GetUserProfile(int id)
        {
            return loginRepository.GetUserProfile(id);
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

        #region Update User Profile
        public static int updateuserprofile(string type, int id,string FirstName, string LastName, string Password, string ProfilePicture, string DateFormat, string Timezone, string Currency, string companylogo)
        {
            return LoginRepository.updateuserprofile(type,id, FirstName, LastName, Password, ProfilePicture, DateFormat, Timezone, Currency, companylogo);
        }
        #endregion


        #region Create Staff
        public static int CreateStaff(int ownerid, string firstname, string lastname, long mobile, string email, int vendoraccess, int customeraccess, string jobposition, string userpic)
        {
            return LoginRepository.CreateStaff(ownerid, firstname, lastname, mobile, email, vendoraccess, customeraccess, jobposition,userpic);
        }
        #endregion

        #region Update Staff
        public static int UpdateStaff(string type,int staffid, string firstname, string lastname, long mobile, string email,  int vendoraccess, int customeraccess, string jobposition,string userpic,string status)
        {
            return LoginRepository.UpdateStaff(type,staffid,  firstname, lastname, mobile, email, vendoraccess, customeraccess, jobposition,userpic,status);
        }
        #endregion

        #region Get Staff Status
        public static SqlDataReader GetStaffStatus(string description)
        {
            return LoginRepository.GetStaffStatus(description);
        }
        #endregion

        #region Get Staff
        public static SqlDataReader GetStaff(int id, string command)
        {
            return LoginRepository.GetStaff(id,command);
        }
        #endregion

        #region Get Job Positions
        public static SqlDataReader GetJobPostions(int id)
        {
            return LoginRepository.GetJobPostions(id);
        }
        #endregion

        #region Available Job Positions
        public static int JobPositions(string type, int id, string position, string PositionID)
        {
            return LoginRepository.JobPositions(type, id, position,PositionID);
        }
        #endregion

        #region Get Owner Staff
        public static SqlDataReader getownerstaff(string id, string usertypeid)
        {
            return LoginRepository.getownerstaff(id, usertypeid);
        }
        #endregion

        #region Invite check
        public static SqlDataReader invitecheck(string email, string usersite, string usertype)
        {
            return LoginRepository.invitecheck(email, usersite, usertype);
        }
        #endregion
    }
}
