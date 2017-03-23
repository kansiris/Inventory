using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Service;
using Inventory.Models;
using System.Data.SqlClient;
using System.Web.Routing;
using System.Web.Caching;
using System.Globalization;
using Microsoft.Web.Administration;
using System.IO;
using System.Data;
using Inventory.Content;
using Newtonsoft.Json;

namespace Inventory.Controllers
{
    public class WarehouseController : Controller
    {
        // GET: Warehouse
        Warehouse wh = new Warehouse();
        public ActionResult Index(string status)
        {

            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            SqlDataReader value = WHservice.getwarehousedtls(user1.DbName);
            DataTable dt = new DataTable();
            dt.Load(value);
            List<Warehouse> warehouse = new List<Warehouse>();
            warehouse = (from DataRow row in dt.Rows
                         select new Warehouse()
                         {
                             wh_Id = row["wh_id"].ToString(),
                             wh_name = row["wh_name"].ToString(),
                             wh_Shortname = row["wh_Shortname"].ToString(),
                             conperson = row["Contact_person"].ToString(),
                             Email = row["Email"].ToString()
                         }).OrderByDescending(m => m.wh_Id).ToList();
            ViewBag.records = warehouse;
            ViewBag.wh_id = getMaxwhid();
            ViewBag.contact = getcontactdetails();
            var wh = getlastinsertedwarehouse(ViewBag.wh_id);
            if (status == "complete")
            {
                ViewBag.Warehouse = 1;
                ViewBag.wh_name = wh.wh_name;
                ViewBag.wh_sname = wh.wh_Shortname;
            }

            return View();
        }
        [HttpPost]
        public ActionResult Index(Warehouse wh, string command)
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult Index(Warehouse wh)
        //{
        //    var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
        //    int count = WHservice.WHaddressinsert(wh.wh_name, wh.wh_Shortname, wh.conperson, wh.Job_position,
        //        wh.phone, wh.Mobile, wh.Email, wh.Note, wh.bill_Street, wh.bill_City, wh.bill_State, wh.bill_Postalcode, wh.bill_Country,
        //        wh.ship_Street, wh.ship_City, wh.ship_State, wh.ship_Postalcode, wh.ship_Country, user.DbName);
        //    if (count > 0)
        //        return Content("<script language='javascript' type='text/javascript'>alert('Warehouse Added successfully');location.href='" + @Url.Action("Index", "Warehouse") + "'</script>"); // Stays in Same View
        //    return Content("<script language='javascript' type='text/javascript'>alert('Failed!!!');location.href='" + @Url.Action("Index", "Warehouse") + "'</script>"); // Stays in Same View
        //}

        private string getMaxwhid()
        {
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            string wh_id = null;
            SqlDataReader exec = WHservice.getwhid(user1.DbName);
            if (exec.Read())
            {
                wh_id = exec["wh_id"].ToString();
            }
            return wh_id;
        }
        private Warehouse getlastinsertedwarehouse(string wh_id)
        {
            
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            SqlDataReader value = WHservice.getlastinsertedwarehouse(user1.DbName, wh_id);
            DataTable dt = new DataTable();
            dt.Load(value);
            Warehouse wh = new Warehouse();
            wh = (from DataRow row in dt.Rows
                      select new Warehouse()
                      {
                          
                          wh_name = row["wh_name"].ToString(),
                          wh_Shortname = row["wh_Shortname"].ToString()
                      }).FirstOrDefault();
            

            return wh;
        }
        public List<Warehouse> getcontactdetails()
        {
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            string wh_id = ViewBag.wh_id;
            SqlDataReader value = WHservice.getcontactdetail(user1.DbName,wh_id);
            DataTable dt = new DataTable();
            dt.Load(value);
            List<Warehouse> contact = new List<Warehouse>();
            WHservice.getcontactdetail(user1.DbName,wh_id);

            contact = (from DataRow row in dt.Rows
                       select new Warehouse()
                       {
                           conperson = row["Contact_Person"].ToString(),
                           Email = row["Email"].ToString(),
                           Mobile = long.Parse(row["Mobile"].ToString())
                       }).ToList();

            return contact;

        }
        public JsonResult getallwhdetails(string wh_id)
        {
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            WHservice.getlastinsertedwarehouse(user1.DbName, wh_id);
            var data = WHservice.getallwhdetails(user1.DbName, wh_id);
            if (data.Read())
            {
                Warehouse wh = new Warehouse
                {
                    wh_Id = data["wh_id"].ToString(),
                    wh_name = data["wh_name"].ToString(),
                    wh_Shortname = data["wh_Shortname"].ToString(),
                    conperson = data["Contact_person"].ToString(),
                    Job_position = data["Job_position"].ToString(),
                    Email = data["Email"].ToString(),
                    phone = long.Parse(data["Phone"].ToString()),
                    Mobile = long.Parse(data["Mobile"].ToString()),
                    Note = data["Note"].ToString(),
                    bill_Street = data["bill_street"].ToString(),
                    bill_City = data["bill_city"].ToString(),
                    bill_State = data["bill_state"].ToString(),
                    bill_Postalcode = data["bill_postalcode"].ToString(),
                    bill_Country = data["bill_country"].ToString(),
                    ship_Street = data["ship_street"].ToString(),
                    ship_City = data["ship_city"].ToString(),
                    ship_State = data["ship_state"].ToString(),
                    ship_Postalcode = data["ship_postalcode"].ToString(),
                    ship_Country = data["ship_country"].ToString(),

                };

                string json = JsonConvert.SerializeObject(wh);
                return Json(json);
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult updatewarehouse(string wh_id, string wh_name, string wh_sname)
        {
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = WHservice.updatewarehouse(user1.DbName,wh_id, wh_name, wh_sname);
            if (data > 0)
            {
                ViewBag.wh_id = wh_id;
                ViewBag.wh_name = wh_name;
                ViewBag.wh_sname = wh_sname;
                return Json("success");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }

        public JsonResult updatewhaddress(string wh_id, string bill_street, string bill_city, string bill_state, string bill_postalcode,
            string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = WHservice.updatewhaddress(user1.DbName, wh_id, bill_street, bill_city, bill_state, bill_postalcode,
                bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country);
            if (data > 0)
            {
                List<Warehouse> wh = new List<Warehouse>();
                ViewBag.wh_id = wh_id;
                ViewBag.bill_street = bill_street;
                ViewBag.bill_city = bill_city;
                ViewBag.bill_state = bill_state;
                ViewBag.bill_postalcode = bill_postalcode;
                ViewBag.bill_country = bill_country;
                ViewBag.ship_street = ship_street;
                ViewBag.ship_city = ship_city;
                ViewBag.ship_state = ship_state;
                ViewBag.ship_postalcode = ship_postalcode;
                ViewBag.ship_country = ship_country;
                return Json("success");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);

        }

        public JsonResult updatewhcontact(string wh_id, string Contact_Person, long phone, long Mobile, string Email, string job_position)
        {
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = WHservice.updatewhcontact(user1.DbName, wh_id, Contact_Person, phone, Mobile, Email, job_position);
            if (data > 0)
            {
                ViewBag.wh_id = wh_id;
                ViewBag.Contact_PersonFname = Contact_Person;
                ViewBag.Phone = phone;
                ViewBag.Mobile = Mobile;
                ViewBag.Email = Email;
                ViewBag.Job_position = job_position;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult updatewhnotes(string wh_id, string Note)
        {
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = WHservice.updatewhnote(user1.DbName, wh_id, Note);
            if (data > 0)
            {
                ViewBag.wh_id = wh_id;
                ViewBag.Note = Note;
                return Json("success");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult insertwhdtls(string wh_name, string wh_sname)
        {
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = WHservice.insertwhdls(user1.DbName, wh_name, wh_sname);
            if (data > 0)
            {
                ViewBag.wh_name = wh_name;
                ViewBag.wh_sname = wh_sname;
                return Json("success");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult insertwhaddress(string wh_id, string bill_street, string bill_city, string bill_state, string bill_postalcode,
            string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            wh_id = getMaxwhid();
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = WHservice.insertwhaddress(user1.DbName, wh_id, bill_street, bill_city, bill_state, bill_postalcode,
               bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country);
            if (data > 0)
            {
                ViewBag.wh_id = wh_id;
                ViewBag.bill_street = bill_street;
                ViewBag.bill_city = bill_city;
                ViewBag.bill_state = bill_state;
                ViewBag.bill_postalcode = bill_postalcode;
                ViewBag.bill_country = bill_country;
                ViewBag.ship_street = ship_street;
                ViewBag.ship_city = ship_city;
                ViewBag.ship_state = ship_state;
                ViewBag.ship_postalcode = ship_postalcode;
                ViewBag.ship_country = ship_country;

                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult insertwhnotes(string wh_id, string Note)
        {
            wh_id = getMaxwhid();
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = WHservice.updatewhnote(user1.DbName,wh_id, Note);
            if (data > 0)
            {
                ViewBag.wh_id = wh_id;
                ViewBag.Note = Note;

                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }

        public JsonResult insertwhcontact(string wh_id, string Contact_Person, long phone, long Mobile, string Email, string job_position)
        {
            wh_id = getMaxwhid();
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = WHservice.insertwhcontact(user1.DbName,wh_id, Contact_Person, phone, Mobile, Email, job_position);
            if(data > 0)
            {
                ViewBag.wh_id = wh_id;
                ViewBag.Contact_PersonFname = Contact_Person;
                ViewBag.Phone = phone;
                ViewBag.Mobile = Mobile;
                ViewBag.Email = Email;
                ViewBag.Job_position = job_position;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
       
    }
}