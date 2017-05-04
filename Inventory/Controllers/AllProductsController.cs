using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Service;
using Inventory.Content;
using System.Data;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class AllProductsController : Controller
    {
        // GET: AllProducts
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var data = ProductService.GetAllProducts(user.DbName);
                DataTable dt = new DataTable();
                dt.Load(data);
                DataView dv = dt.DefaultView;
                dv.Sort = "id desc";
                dt = dv.ToTable();
                ViewBag.products = (from DataRow row in dt.Rows
                                    select new Product()
                                    {
                                        ID = row["ID"].ToString(),
                                        product_id = row["product_id"].ToString(),
                                        product_name = row["product_name"].ToString(),
                                        Measurement = row["Measurement"].ToString(),
                                        weight = row["weight"].ToString(),
                                        total_price = row["total_price"].ToString(),
                                        brand = row["brand"].ToString(),
                                        model = row["model"].ToString(),
                                        created_date = row["created_date"].ToString(),
                                        status = row["status"].ToString(),
                                    }).ToList();
            }
            return View();
        }

        public ActionResult productstatus(string id,string status)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                int count = ProductService.productstatus(user.DbName,id, status);
                //if(count > 0)
                //    return Content("<script language='javascript' type='text/javascript'>alert('Now Product is "+status+"');location.href='" + @Url.Action("Index", "AllProducts") + "'</script>"); // Stays in Same View
                return Content("<script language='javascript' type='text/javascript'>alert('Now Product " + id + " is " + status + "');location.href='" + @Url.Action("Index", "AllProducts") + "'</script>"); // Stays in Same View
            }
            return View();
        }
    }
}