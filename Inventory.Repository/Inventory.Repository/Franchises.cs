using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.Linq.SqlClient;

namespace Inventory.Repository
{
    public class Franchises
    {
        private static string ConnectionString;
        #region FranchisesSelectAll
        public static SqlDataReader FranchisesSelectAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, "FranchisesSelectAllRows");
        }
        #endregion
        #region FranchisesSelectRow
        public static SqlDataReader FranchisesSelectRow(string Franchise_Id)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "FranchisesSelectRow", Franchise_Id);
        }
        #endregion
        #region FranchisesInsertRow
        public static void FranchisesInsertRow(string Franchise_Id, string Staff_Id, string Franchise_Name, string Location, string Logo, string Created_Date, string Bank_Name, string Accunt_Number, string LandLine_Number, string Remarks, string Paytym_Number, string Adhar_Number)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "FranchisesInsertRow", Franchise_Id, Staff_Id, Franchise_Name, Location, Logo, Created_Date, Bank_Name, Accunt_Number, LandLine_Number, Remarks, Paytym_Number, Adhar_Number);
        }
        #endregion
        #region FranchisesUpdateRow
        public static void FranchisesUpdateRow(string Franchise_Id, string Staff_Id, string Franchise_Name, string Location, string Logo, string Created_Date, string Bank_Name, string Accunt_Number, string LandLine_Number, string Remarks, string Paytym_Number, string Adhar_Number)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "FranchisesUpdateRow", Franchise_Id, Staff_Id, Franchise_Name, Location, Logo, Created_Date, Bank_Name, Accunt_Number, LandLine_Number, Remarks, Paytym_Number, Adhar_Number);
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

    }
}
