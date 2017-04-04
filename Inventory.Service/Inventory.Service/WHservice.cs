using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Repository;
using System.Data.SqlClient;

namespace Inventory.Service
{
    public class WHservice
    {
        public static int warehouseinsert(string Wh_Name, string wh_Shortname)
        {
            return WarehouseRepository.warehouseinsert(Wh_Name, wh_Shortname);
        }

        public static int WHaddressinsert(string WH_Name, string WH_ShortName, string contactperson, string jobposition,
            long phone, long mobile, string Email, string Note, string bill_street, string bill_city, string bill_state,
            string bill_postalcode, string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country, string dbname)
        {
            return WarehouseRepository.WHaddressinsert(WH_Name, WH_ShortName, contactperson, jobposition,
                phone, mobile, Email, Note, bill_street, bill_city, bill_state, bill_postalcode, bill_country,
                ship_street, ship_city, ship_state, ship_postalcode, ship_country, dbname);
        }
        public static SqlDataReader getwarehousedtls(string dbname)
        {
            return WarehouseRepository.getwarehousedtls(dbname);
        }
        public static SqlDataReader getwhid(string dbname)
        {
            return WarehouseRepository.getwhid(dbname);
        }

        public static SqlDataReader getcontactdetail(string dbname,string wh_id)
        {
            return WarehouseRepository.getwhcondtls(dbname,wh_id);
        }
        public static SqlDataReader getlastinsertedwarehouse(string dbname, string wh_id)
        {
            return WarehouseRepository.getlastinsertedwarehouse(dbname,wh_id);
        }
        public static SqlDataReader getallwhdetails(string dbname,string wh_id)
        {
            return WarehouseRepository.getallwhdetails(dbname,wh_id);
        }
        public static int updatewarehouse(string dbname, string wh_id, string wh_name, string wh_sname)
        {
            return WarehouseRepository.updatewarehouse(dbname, wh_id, wh_name, wh_sname);
        }
        public static int updatewhaddress(string dbname, string wh_id, string bill_street, string bill_city, string bill_state, string bill_postalcode,
            string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            return WarehouseRepository.updatewhaddress(dbname, wh_id, bill_street, bill_city, bill_state, bill_postalcode,
                bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country);
        }
        public static int updatewhcontact(string dbname, string wh_id, string Contact_Person, long phone, long Mobile, string Email, string job_position)
        {
            return WarehouseRepository.updatewhcontact(dbname, wh_id, Contact_Person, phone, Mobile, Email, job_position);
        }
        public static int updatewhnote(string dbname, string wh_id,string Note)
        {
            return WarehouseRepository.updatewhnotes(dbname, wh_id, Note);
        }
        public static int insertwhdls(string dbname, string wh_name, string wh_sname)
        {
            return WarehouseRepository.insertwhdtls(dbname, wh_name, wh_sname);
        }
        public static int insertwhaddress(string dbname, string wh_id, string bill_street, string bill_city, string bill_state, string bill_postalcode,
            string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            return WarehouseRepository.insertwhaddress(dbname, wh_id, bill_street, bill_city, bill_state, bill_postalcode,
               bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country);
        }
        public static int insertwhcontact(string dbname, string wh_id, string Contact_Person, long phone, long Mobile, string Email, string job_position)
        {
            return WarehouseRepository.insertwhcontact(dbname, wh_id, Contact_Person, phone, Mobile, Email, job_position);
        }

        public static int deletewarehouse(string dbname, string wh_Id)
        {
            return WarehouseRepository.deletewarehouse(dbname, wh_Id);
        }
        public static SqlDataReader getwhcondtls(string dbname, string wh_id)
        {
            return WarehouseRepository.getwhcondtls(dbname, wh_id);
        }
        public static int updatewhimage(string wh_id, string wh_image)
        {
            return WarehouseRepository.updatewhimage( wh_id, wh_image);
        }
    }
}
