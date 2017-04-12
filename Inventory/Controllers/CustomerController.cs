using Inventory.Content;
using Inventory.Models;
using Inventory.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
       
        public PartialViewResult CustomerCompany()
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var records = CustomerService.getcuscomapnies(user.DbName);
            var dt = new DataTable();
            dt.Load(records);
            List<Customer> customer = new List<Customer>();
            customer = (from DataRow row in dt.Rows
                      select new Customer()
                      {
                          cus_company_Id = int.Parse(row["cus_company_Id"].ToString()),
                          cus_company_name = row["cus_company_name"].ToString(),
                          cus_email = row["cus_email"].ToString(),
                          cus_logo = row["cus_logo"].ToString()
                      }).OrderByDescending(m => m.cus_company_Id).ToList();
            ViewBag.records = customer;
            return PartialView("CustomerCompany", ViewBag.records);
        }
    }
}