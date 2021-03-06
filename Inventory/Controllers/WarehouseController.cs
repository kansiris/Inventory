﻿using System;
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

            ViewBag.country = new SelectList(CountryList().OrderBy(x => x.Value), "Value", "Text");

            ViewBag.jobpositions = whjobpositions();
            var wh = getlastinsertedwarehouse(ViewBag.wh_id);
            if (status == "complete")
            {
                ViewBag.Warehouse = 1;
                ViewBag.wh_name = wh.wh_name;
                ViewBag.wh_sname = wh.wh_Shortname;
            }

            return View();
        }
        //loading of warehouse details in partialview
        public PartialViewResult WarehouseDetails()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
                SqlDataReader value = WHservice.getwarehousedtls(user1.DbName);
                DataTable dt = new DataTable();
                dt.Load(value);
                value.Close();
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
            return PartialView("WarehouseDetails", null);
        }
        //loading of warehouse contactdetails in partialview
        public PartialViewResult WarehouseContact(string id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
                SqlDataReader value = WHservice.getcontactdetail(user1.DbName, id);
                var dt = new DataTable();
                dt.Load(value);
                value.Close();
                List<Warehouse> contact = new List<Warehouse>();
                contact = (from DataRow row in dt.Rows
                           select new Warehouse()
                           {
                               con_id = row["con_id"].ToString(),
                               conperson = row["contact_person"].ToString(),

                               Email = row["email"].ToString(),
                               Mobile = row["mobile"].ToString(),
                               phone = row["phone"].ToString(),
                               Job_position = row["job_position"].ToString(),
                               Image = row["image"].ToString()
                           }).OrderByDescending(m => m.con_id).ToList();
                ViewBag.records = contact;
                return PartialView("WarehouseContact", ViewBag.records);
            }
            return PartialView("WarehouseContact", null);
        }
        //To get the warehouseid
        private string getMaxwhid()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
                string wh_id = null;
                SqlDataReader exec = WHservice.getwhid(user1.DbName);
                if (exec.Read())
                {
                    wh_id = exec["wh_id"].ToString();
                }
                exec.Close();
                return wh_id;
            }
            return "";
        }
        //lastinserted warehouse details
        private Warehouse getlastinsertedwarehouse(string wh_id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
                SqlDataReader value = WHservice.getlastinsertedwarehouse(user1.DbName, wh_id);
                DataTable dt = new DataTable();
                dt.Load(value);
                value.Close();
                Warehouse wh = new Warehouse();
                wh = (from DataRow row in dt.Rows
                      select new Warehouse()
                      {

                          wh_name = row["wh_name"].ToString(),
                          wh_Shortname = row["wh_Shortname"].ToString()
                      }).FirstOrDefault();


                return wh;
            }
            return null;
        }


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
        //loading of total warehouse details
        public JsonResult getallwhdetails(string wh_id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
                        galimage1 = data["galimage1"].ToString(),
                        galimage2 = data["galimage2"].ToString(),
                        galimage3 = data["galimage3"].ToString(),
                        galimage4 = data["galimage4"].ToString(),
                        //Wh_logo = data["Wh_Image"].ToString(),
                    };
                    data.Close();
                    string json = JsonConvert.SerializeObject(wh);
                    return Json(json);
                }
                return Json("unique", JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }

        //loading of warehouse contact details
        public JsonResult getwhcondtls(string con_id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
                        Image = data["image"].ToString(),
                    };

                    string json = JsonConvert.SerializeObject(wh);
                    return Json(json);
                }
                data.Close();
                return Json("unique", JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }
        //update warehouse
        public JsonResult updatewarehouse(string wh_id, string wh_name, string wh_sname)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;

                var chkwh = WHservice.chkwh(user1.DbName, wh_name);
                var chksname = WHservice.chkwhsname(user1.DbName, wh_sname);
                if (chkwh.Read())
                {
                    return Json("Y", JsonRequestBehavior.AllowGet);
                }
                if (chksname.Read())
                {
                    return Json("SNAME", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = WHservice.updatewarehouse(user1.DbName, wh_id, wh_name, wh_sname);
                    if (data > 0)
                    {
                        ViewBag.wh_id = wh_id;
                        ViewBag.wh_name = wh_name;
                        ViewBag.wh_sname = wh_sname;
                       
                        return Json("success");
                      
                    }
                 
                }
                
                return Json("unique", JsonRequestBehavior.AllowGet);
               
            }
            return Json(null);
        }
        //Delete warehouse
        public JsonResult deletewarehouse(string wh_id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
            return Json(null);
        }
        //Delete warehouse contact person
        public JsonResult deletecontactperson(string con_id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
            return Json(null);
        }
        public JsonResult deletejobposition(string jp_id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var data = WHservice.deletewhjp(user1.DbName, jp_id);
                if (data > 0)
                {
                    ViewBag.jp_id = jp_id;
                    return Json("success");
                }
               
                return Json("unique", JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }
        //update warehouse address
        public JsonResult updatewhaddress(string wh_id, Warehouse warehouse)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
            return Json(null);

        }
        //update warehouse contact
        public JsonResult updatewhcontact(string con_id, Warehouse warehouse)  //string wh_id, string Contact_Person, long phone, long Mobile, string Email, string job_position
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
                //string dbname, string con_id, string Contact_Person, string phone, string Mobile, string Email, string job_position
                var data = WHservice.updatewhcontact(user1.DbName, warehouse.con_id, warehouse.conperson, warehouse.phone, warehouse.Mobile, warehouse.Email, warehouse.Job_position, warehouse.Image);
                if (data > 0)
                {
                    List<Warehouse> wh = new List<Warehouse>();
                    ViewBag.con_id = warehouse.con_id.TrimEnd();
                    ViewBag.Contact_PersonFname = warehouse.conperson;
                    ViewBag.Phone = warehouse.phone;
                    ViewBag.Mobile = warehouse.Mobile;
                    ViewBag.Email = warehouse.Email;
                    ViewBag.Job_position = warehouse.Job_position;
                    ViewBag.image = warehouse.Image;
                    return Json("success");
                }
                return Json("unique", JsonRequestBehavior.AllowGet);
            }
            return Json(null);

        }
        //update warehouse Notes
        public JsonResult updatewhnotes(string wh_id, Warehouse warehouse)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
            return Json(null);
        }
        //Save warehouse Details
        public JsonResult insertwhdtls(string wh_name, string wh_sname)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;

                var chkwh = WHservice.chkwh(user1.DbName, wh_name);
                var chksname = WHservice.chkwhsname(user1.DbName, wh_sname);
                if (chkwh.Read())
                {
                    return Json("Y", JsonRequestBehavior.AllowGet);
                }
                if (chksname.Read())
                {
                    return Json("SNAME", JsonRequestBehavior.AllowGet);
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
            return Json(null);
        }
        public JsonResult savewhjp(string job_position)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var data = WHservice.savewhjp(user1.DbName, job_position);
                if (data > 0)
                {
                    ViewBag.job_position = job_position;
                    return Json("success");
                }
                return Json("unique", JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        //save warehouse address
        public JsonResult insertwhaddress(string wh_id, Warehouse warehouse)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
            return Json(null);
        }
        //save gallery
        public JsonResult updategallery(string wh_id, Warehouse warehouse)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                wh_id = getMaxwhid();
                var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var data = WHservice.updategallery(user1.DbName, wh_id.TrimEnd(), warehouse.galimage1, warehouse.galimage2, warehouse.galimage3, warehouse.galimage4);
                if (data > 0)
                {
                    ViewBag.wh_id = wh_id;
                    ViewBag.galimage1 = warehouse.galimage1;
                    ViewBag.galimage2 = warehouse.galimage2;
                    ViewBag.galimage3 = warehouse.galimage3;
                    ViewBag.galimage4 = warehouse.galimage4;
                    return Json("success");
                }
                return Json("unique", JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }
        //update gallery
        
        public JsonResult savegallery(string wh_id, Warehouse warehouse)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //wh_id = getMaxwhid();
                var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var data = WHservice.updategallery(user1.DbName, warehouse.wh_Id.ToString(), warehouse.galimage1, warehouse.galimage2, warehouse.galimage3, warehouse.galimage4);
                if (data > 0)
                {
                    ViewBag.wh_id = warehouse.wh_Id;
                    ViewBag.galimage1 = warehouse.galimage1;
                    ViewBag.galimage2 = warehouse.galimage2;
                    ViewBag.galimage3 = warehouse.galimage3;
                    ViewBag.galimage4 = warehouse.galimage4;
                    return Json("success");
                }
                return Json("unique", JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }
        //update gallery

        //Save warehouse Notes
        public JsonResult insertwhnotes(string wh_id, Warehouse warehouse)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
            return Json(null);
        }
        //Save warehouse contact
        public JsonResult insertwhcontact(string wh_id, Warehouse warehouse)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                wh_id = getMaxwhid();
                wh_id = getMaxwhid().TrimEnd();
                var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var data = WHservice.insertwarehousecontact(user1.DbName, wh_id, warehouse.conperson, warehouse.Job_position, warehouse.Email, warehouse.phone, warehouse.Mobile, warehouse.Image);
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
                    ViewBag.image = warehouse.Image;
                    return Json("success");
                }
                return Json("unique", JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }
        //To get the ContactID
        private string getMaxcontactID()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string con_id = null;
                var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
                SqlDataReader value = WHservice.getwarehousedtls(user1.DbName);
                SqlDataReader exec = WHservice.getcontactid(user1.DbName);
                if (exec.Read())
                {
                    con_id = exec["con_id"].ToString();
                }
                exec.Close();
                return con_id;

            }
            return "";
        }

        //update contactperson pic
        [HttpPost]
        public ActionResult updatecontactpersonimage(HttpPostedFileBase helpSectionImages, string con_id)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["helpSectionImages"];
                Image img = Bitmap.FromStream(pic.InputStream);
                ImageConverter _imageConverter = new ImageConverter();
                byte[] contactimage = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
                string base64String = Convert.ToBase64String(contactimage);
                return Json(base64String);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult updategalimage1(HttpPostedFileBase helpSectionImages, string wh_id)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["helpSectionImages"];
                Image img = Bitmap.FromStream(pic.InputStream);
                ImageConverter _imageConverter = new ImageConverter();
                byte[] galimage1 = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
                string base64String = Convert.ToBase64String(galimage1);
                return Json(base64String);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult updategalimage2(HttpPostedFileBase helpSectionImages, string wh_id)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["helpSectionImages"];
                Image img = Bitmap.FromStream(pic.InputStream);
                ImageConverter _imageConverter = new ImageConverter();
                byte[] galimage2 = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
                string base64String = Convert.ToBase64String(galimage2);
                return Json(base64String);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult updategalimage3(HttpPostedFileBase helpSectionImages, string wh_id)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["helpSectionImages"];
                Image img = Bitmap.FromStream(pic.InputStream);
                ImageConverter _imageConverter = new ImageConverter();
                byte[] galimage3 = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
                string base64String = Convert.ToBase64String(galimage3);
                return Json(base64String);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult updategalimage4(HttpPostedFileBase helpSectionImages, string wh_id)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["helpSectionImages"];
                Image img = Bitmap.FromStream(pic.InputStream);
                ImageConverter _imageConverter = new ImageConverter();
                byte[] galimage4 = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
                string base64String = Convert.ToBase64String(galimage4);
                return Json(base64String);
            }
            return Json(JsonRequestBehavior.AllowGet);
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
        public class whjobposition
        {
            public string job_position { get; set; }
            public string jp_id { get; set; }
        }
        public List<whjobposition> whjobpositions()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //string  wh_id = getMaxwhid();
                var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var positions = WHservice.getwhjobpositions(user1.DbName);
                var dt = new DataTable();
                dt.Load(positions);
                List<whjobposition> jpositions = (from DataRow row in dt.Rows select new whjobposition() { jp_id = row["jp_id"].ToString(), job_position = row["job_position"].ToString() }).ToList();
                return jpositions;
            }
            return null;
        }
    }
}