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
                                        product_images = row["product_images"].ToString(),
                                        Quantity_Total = row["Total"].ToString(),
                                    }).ToList();
                Reordering(user.DbName);
                if (TempData["msg"] != null)
                {
                    ViewBag.msg = TempData["msg"];
                }
                if (TempData["smsg"] != null)
                {
                    ViewBag.smsg = TempData["smsg"];
                }
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        public ActionResult productstatus(string id, string status)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                int count = ProductService.productstatus(user.DbName, id, status);
                if (status == "Active")
                    TempData["smsg"] = "Now Product " + id + " is " + status + "";
                else
                    TempData["msg"] = "Now Product " + id + " is " + status + "";
                return RedirectToAction("Index", "AllProducts");
                //if(count > 0)
                //    return Content("<script language='javascript' type='text/javascript'>alert('Now Product is "+status+"');location.href='" + @Url.Action("Index", "AllProducts") + "'</script>"); // Stays in Same View
                //return Content("<script language='javascript' type='text/javascript'>alert('Now Product " + id + " is " + status + "');location.href='" + @Url.Action("Index", "AllProducts") + "'</script>"); // Stays in Same View
            }
            return View();
        }

        public void Reordering(string dbname)
        {
            var items = ProductService.Reordering(dbname);
            DataTable dt = new DataTable();
            dt.Load(items);
            var reorderitems = (from DataRow row in dt.Rows
                                select new Product()
                                {
                                    Quantity_id = int.Parse(row["id"].ToString()),
                                    product_id = row["product_id"].ToString(),
                                    Quantity_area = row["area"].ToString(),
                                    Qty_Stock = row["Qty"].ToString(),
                                    Qty_Reorder = row["Reorder_level"].ToString()
                                }).ToList();
            var reorderlist = new List<Product>();
            foreach (var item in reorderitems)
            {
                int qty = int.Parse(item.Qty_Stock);
                int reorder = int.Parse(item.Qty_Reorder.ToString());
                if (qty < reorder)
                {
                    reorderlist.Add(item);
                }
            }
            ViewBag.reorderlistitems = reorderlist;
        }
    }
}