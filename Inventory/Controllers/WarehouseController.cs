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
using System.Drawing;

namespace Inventory.Controllers
{
    public class WarehouseController : Controller
    {
        // GET: Warehouse
        Warehouse wh = new Warehouse();
        public ActionResult Index(string status)
        {
            ViewBag.wh_id = getMaxwhid();
            ViewBag.con_id = getMaxcontactID();
            var wh = getlastinsertedwarehouse(ViewBag.wh_id);
            if (status == "complete")
            {
                ViewBag.Warehouse = 1;
                ViewBag.wh_name = wh.wh_name;
                ViewBag.wh_sname = wh.wh_Shortname;
            }

            return View();
        }
        public PartialViewResult WarehouseDetails()
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
                             Email = row["Email"].ToString(),
                             // Wh_logo = row["Wh_Image"].ToString()
                         }).OrderByDescending(m => m.wh_Id).ToList();
            ViewBag.records = warehouse;
            return PartialView("WarehouseDetails", ViewBag.records);
        }
        public PartialViewResult WarehouseContact(string id)
        {
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            SqlDataReader value = WHservice.getcontactdetail(user1.DbName, id);
            var dt = new DataTable();
            dt.Load(value);
            List<Warehouse> contact = new List<Warehouse>();
            contact = (from DataRow row in dt.Rows
                       select new Warehouse()
                       {
                           con_id = row["con_id"].ToString(),
                           conperson = row["contact_person"].ToString(),

                           Email = row["email"].ToString(),
                           Mobile = row["mobile"].ToString(),
                           Job_position = row["job_position"].ToString()
                       }).OrderByDescending(m => m.con_id).ToList();
            ViewBag.records = contact;
            return PartialView("WarehouseContact", ViewBag.records);

        }
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
        //public List<Warehouse> getcontactdetails()
        //{

        //    var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
        //    string wh_id = ViewBag.wh_id;
        //    SqlDataReader value = WHservice.getcontactdetail(user1.DbName, wh_id.TrimEnd());
        //    if (value.Read())
        //    {
        //        DataTable dt = new DataTable();
        //        dt.Load(value);
        //        List<Warehouse> contact = new List<Warehouse>();
        //        WHservice.getcontactdetail(user1.DbName, wh_id.TrimEnd());
        //        //string cp = dt.Rows[0]["Contact_Person"].ToString();
        //        //string em = dt.Rows[0]["Email"].ToString();
        //        //string Mb = dt.Rows[0]["Mobile"].ToString();
        //        //string jb = dt.Rows[0]["Job_position"].ToString();

        //        contact = (from DataRow row in dt.Rows
        //                   select new Warehouse()
        //                   {
        //                       conperson = row["Contact_Person"].ToString(),
        //                       Email = row["Email"].ToString(),
        //                       Mobile = row["Mobile"].ToString(),
        //                       Job_position = row["Job_position"].ToString(),
        //                   }).ToList();



        //        return contact;
        //    }
        //    return null;
        //}

        [HttpPost]
        public ActionResult Updatewhimage(HttpPostedFileBase helpSectionImages, string wh_id)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                ViewBag.wh_id = wh_id;
                var pic = System.Web.HttpContext.Current.Request.Files["helpSectionImages"];
                Image img = Bitmap.FromStream(pic.InputStream);
                ImageConverter _imageConverter = new ImageConverter();
                byte[] whpic = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
                string base64String = Convert.ToBase64String(whpic);
                int count = WHservice.updatewhimage(wh_id, base64String);
                return Json(base64String);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult getallwhdetails(string wh_id)
        {
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            WHservice.getlastinsertedwarehouse(user1.DbName, wh_id);
            var data = WHservice.getallwhdetails(user1.DbName, wh_id);
            long phn;
            long Mob;
            if (data.Read())
            {
                if (data["Phone"].ToString() == "")
                    phn = 0;
                else
                    phn = long.Parse(data["Phone"].ToString());
                if (data["Mobile"].ToString() == "")
                    Mob = 0;
                else
                    Mob = long.Parse(data["Mobile"].ToString());
                Warehouse wh = new Warehouse
                {
                    wh_Id = wh_id,//data["wh_id"].ToString(),
                    wh_name = data["wh_name"].ToString(),
                    wh_Shortname = data["wh_Shortname"].ToString(),
                    conperson = data["Contact_person"].ToString(),
                    Job_position = data["Job_position"].ToString(),
                    Email = data["Email"].ToString(),
                    phone = data["Phone"].ToString(),
                    Mobile = data["Mobile"].ToString(),
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
                    //Wh_logo = data["Wh_Image"].ToString(),
                };

                string json = JsonConvert.SerializeObject(wh);
                return Json(json);
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult getwhcondtls(string con_id)
        {
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;

            var data = WHservice.getwhcondtls(user1.DbName, con_id);

            if (data.Read())
            {

                Warehouse wh = new Warehouse
                {
                    con_id = data["con_id"].ToString(),
                    conperson = data["contact_person"].ToString(),
                    Job_position = data["job_position"].ToString(),
                    Email = data["email"].ToString(),
                    phone = data["phone"].ToString(),
                    Mobile = data["mobile"].ToString(),
                };

                string json = JsonConvert.SerializeObject(wh);
                return Json(json);
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult updatewarehouse(string wh_id, string wh_name, string wh_sname)
        {
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = WHservice.updatewarehouse(user1.DbName, wh_id, wh_name, wh_sname);
            if (data > 0)
            {
                ViewBag.wh_id = wh_id;
                ViewBag.wh_name = wh_name;
                ViewBag.wh_sname = wh_sname;
                return Json("success");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        private List<SelectListItem> CountryList()
        {
            List<SelectListItem> cultureList = new List<SelectListItem>();
            CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            if (getCultureInfo.Count() > 0)
            {
                foreach (CultureInfo cultureInfo in getCultureInfo)
                {
                    RegionInfo getRegionInfo = new RegionInfo(cultureInfo.LCID);
                    var newitem = new SelectListItem { Text = getRegionInfo.EnglishName, Value = getRegionInfo.EnglishName };
                    cultureList.Add(newitem);
                }
            }
            return cultureList;
        }
        public JsonResult deletewarehouse(string wh_id)
        {
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = WHservice.deletewarehouse(user1.DbName, wh_id);
            if (data > 0)
            {
                ViewBag.wh_id = wh_id;
                return Json("success");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult deletecontactperson(string con_id)
        {
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = WHservice.deletecontactperson(user1.DbName, con_id);
            if (data > 0)
            {
                ViewBag.con_id = con_id;
                return Json("success");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult updatewhaddress(string wh_id, Warehouse warehouse)
        {
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = WHservice.updatewhaddress(user1.DbName, warehouse.wh_Id.ToString(), warehouse.bill_Street, warehouse.bill_City, warehouse.bill_State, warehouse.bill_Postalcode,
                warehouse.bill_Country, warehouse.ship_Street, warehouse.ship_City, warehouse.ship_State, warehouse.ship_Postalcode, warehouse.ship_Country);
            if (data > 0)
            {
                List<Warehouse> wh = new List<Warehouse>();
                ViewBag.wh_id = warehouse.wh_Id;
                ViewBag.bill_street = warehouse.bill_Street;
                ViewBag.bill_city = warehouse.bill_City;
                ViewBag.bill_state = warehouse.bill_State;
                ViewBag.bill_postalcode = warehouse.bill_Postalcode;
                ViewBag.bill_country = warehouse.bill_Country;
                ViewBag.ship_street = warehouse.ship_Street;
                ViewBag.ship_city = warehouse.ship_City;
                ViewBag.ship_state = warehouse.ship_State;
                ViewBag.ship_postalcode = warehouse.ship_Postalcode;
                ViewBag.ship_country = warehouse.ship_Country;
                return Json("success");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);

        }

        //public JsonResult updatewhcontact(string wh_id, Warehouse warehouse)  //string wh_id, string Contact_Person, long phone, long Mobile, string Email, string job_position
        //{
        //    var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
        //    var data = WHservice.updatewhcontact(user1.DbName, warehouse.wh_Id.ToString(), warehouse.conperson, warehouse.phone, warehouse.Mobile, warehouse.Email, warehouse.Job_position);
        //    if (data > 0)
        //    {
        //        List<Warehouse> wh = new List<Warehouse>();
        //        ViewBag.wh_id = warehouse.wh_Id;
        //        ViewBag.Contact_PersonFname = warehouse.conperson;
        //        ViewBag.Phone = warehouse.phone;
        //        ViewBag.Mobile = warehouse.Mobile;
        //        ViewBag.Email = warehouse.Email;
        //        ViewBag.Job_position = warehouse.Job_position;
        //        return Json("success");
        //    }
        //    return Json("unique", JsonRequestBehavior.AllowGet);

        //}
        public JsonResult updatewhcontact(string con_id, Warehouse warehouse)  //string wh_id, string Contact_Person, long phone, long Mobile, string Email, string job_position
        {
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = WHservice.updatewhcontact(user1.DbName, warehouse.con_id, warehouse.conperson, warehouse.Job_position, warehouse.Email, warehouse.phone, warehouse.Mobile);
            if (data > 0)
            {
                List<Warehouse> wh = new List<Warehouse>();
                ViewBag.con_id = warehouse.con_id.TrimEnd();
                ViewBag.Contact_PersonFname = warehouse.conperson;
                ViewBag.Phone = warehouse.phone;
                ViewBag.Mobile = warehouse.Mobile;
                ViewBag.Email = warehouse.Email;
                ViewBag.Job_position = warehouse.Job_position;
                return Json("success");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);

        }
        public JsonResult updatewhnotes(string wh_id, Warehouse warehouse)
        {
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = WHservice.updatewhnote(user1.DbName, warehouse.wh_Id.ToString(), warehouse.Note);
            if (data > 0)
            {
                ViewBag.wh_id = warehouse.wh_Id;
                ViewBag.Note = warehouse.Note;
                return Json("success");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
      
        public JsonResult insertwhdtls(string wh_name, string wh_sname)
        {

            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;

            var chkwh = WHservice.chkwh(user1.DbName, wh_name);
            if (chkwh.Read())
            {
                return Json("Y", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = WHservice.insertwhdls(user1.DbName, wh_name, wh_sname);
                if (data > 0)
                {
                    ViewBag.wh_name = wh_name;
                    ViewBag.wh_sname = wh_sname;
                    return Json("success");
                }
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult insertwhaddress(string wh_id, Warehouse warehouse)
        {
            wh_id = getMaxwhid();
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = WHservice.updatewhaddress(user1.DbName, wh_id.TrimEnd(), warehouse.bill_Street, warehouse.bill_City, warehouse.bill_State, warehouse.bill_Postalcode,
                warehouse.bill_Country, warehouse.ship_Street, warehouse.ship_City, warehouse.ship_State, warehouse.ship_Postalcode, warehouse.ship_Country);
            if (data > 0)
            {
                ViewBag.wh_id = wh_id;
                ViewBag.bill_street = warehouse.bill_Street;
                ViewBag.bill_city = warehouse.bill_City;
                ViewBag.bill_state = warehouse.bill_State;
                ViewBag.bill_postalcode = warehouse.bill_Postalcode;
                ViewBag.bill_country = warehouse.bill_Country;
                ViewBag.ship_street = warehouse.ship_Street;
                ViewBag.ship_city = warehouse.ship_City;
                ViewBag.ship_state = warehouse.ship_State;
                ViewBag.ship_postalcode = warehouse.ship_Postalcode;
                ViewBag.ship_country = warehouse.ship_Country;

                return Json("success");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult insertwhnotes(string wh_id, Warehouse warehouse)
        {
            wh_id = getMaxwhid();
            wh_id = wh_id.TrimEnd();
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = WHservice.updatewhnote(user1.DbName, wh_id, warehouse.Note);
            if (data > 0)
            {
                ViewBag.wh_id = wh_id;
                ViewBag.Note = warehouse.Note;

                return Json("success");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }

        public JsonResult insertwhcontact(string wh_id, Warehouse warehouse)
        {
            wh_id = getMaxwhid();
            wh_id = getMaxwhid().TrimEnd();
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = WHservice.insertwarehousecontact(user1.DbName, wh_id, warehouse.conperson, warehouse.Job_position, warehouse.Email, warehouse.phone, warehouse.Mobile);
            if (data > 0)
            {
                //ViewBag.wh_id = wh_id;
                string con_id = getMaxcontactID().TrimEnd();
                ViewBag.con_id = con_id;
                var result = new { Result = "success", ID = con_id };
                ViewBag.Contact_PersonFname = warehouse.conperson;
                ViewBag.Phone = warehouse.phone;
                ViewBag.Mobile = warehouse.Mobile;
                ViewBag.Email = warehouse.Email;
                ViewBag.Job_position = warehouse.Job_position;
                return Json("success");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        private string getMaxcontactID()
        {
            string con_id = null;
            var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            SqlDataReader value = WHservice.getwarehousedtls(user1.DbName);
            SqlDataReader exec = WHservice.getcontactid(user1.DbName);
            if (exec.Read())
            {
                con_id = exec["con_id"].ToString();
            }
            return con_id;
        }
    }
}