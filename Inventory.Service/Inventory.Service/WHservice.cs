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

        public static SqlDataReader getcontactdetail(string dbname, string wh_id)
        {
            return WarehouseRepository.getcontactdetail(dbname, wh_id);
        }
        public static SqlDataReader getlastinsertedwarehouse(string dbname, string wh_id)
        {
            return WarehouseRepository.getlastinsertedwarehouse(dbname, wh_id);
        }
        public static SqlDataReader getallwhdetails(string dbname, string wh_id)
        {
            return WarehouseRepository.getallwhdetails(dbname, wh_id);
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
        public static int updatewhcontact(string dbname, string con_id, string Contact_Person, string phone, string Mobile, string Email, string job_position, string image)
        {
            return WarehouseRepository.updatewhcontact(dbname, con_id, Contact_Person, phone, Mobile, Email, job_position, image);
        }
        public static int updatewhnote(string dbname, string wh_id, string Note)
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

        public static int insertwarehousecontact(string dbname, string wh_id, string Contact_Person, string job_position, string Email, string phone, string Mobile, string image)
        {
            return WarehouseRepository.insertwarehousecontact(dbname, wh_id, Contact_Person, job_position, Email, phone, Mobile, image);
        }

        public static int deletewarehouse(string dbname, string wh_Id)
        {
            return WarehouseRepository.deletewarehouse(dbname, wh_Id);
        }
        public static int deletecontactperson(string dbname, string con_id)
        {
            return WarehouseRepository.deletecontactperson(dbname, con_id);
        }
        public static SqlDataReader getwhcondtls(string dbname, string con_id)
        {
            return WarehouseRepository.getwhcondtls(dbname, con_id);
        }
        public static int updatewhimage(string wh_id, string wh_image)
        {
            return WarehouseRepository.updatewhimage(wh_id, wh_image);
        }
        public static SqlDataReader chkwh(string dbname, string wh_name)
        {
            return WarehouseRepository.chkwh(dbname, wh_name);
        }
        public static SqlDataReader chkwhsname(string dbname, string wh_sname)
        {
            return WarehouseRepository.chkwhsname(dbname, wh_sname);
        }
        public static SqlDataReader getcontactid(string dbname)
        {
            return WarehouseRepository.getcontactid(dbname);
        }
        public static int updategallery(string dbname, string wh_id, string galimage1, string galimage2, string galimage3, string galimage4)
        {
            return WarehouseRepository.updategallery(dbname, wh_id, galimage1, galimage2, galimage3, galimage4);
        }
        public static SqlDataReader getwhjobpositions(string dbname)
        {
            return WarehouseRepository.getwhjobpositions(dbname);
        }
        public static int savewhjp(string dbname, string job_position)
        {
            return WarehouseRepository.savewhjp(dbname, job_position);
        }
        public static int deletewhjp(string dbname, string jp_id)
        {
            return WarehouseRepository.deletewhjobposition(dbname, jp_id);
        }
       
    }
}
