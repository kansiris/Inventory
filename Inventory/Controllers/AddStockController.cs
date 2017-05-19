using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Service;
using System.Data;
using Inventory.Content;
using Inventory.Models;
using System.Data.SqlClient;

namespace Inventory.Controllers
{
    public class AddStockController : Controller
    {
        // GET: AddStock
        public ActionResult Index(string pid)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                ViewBag.product = convertproduct(user.DbName, pid);
                ViewBag.AvailableQty = convertquantity(user.DbName, pid);
                ViewBag.total = ViewBag.AvailableQty[0].Quantity_Total;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(Product product, string pid)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                int status = ProductService.updateproduct(user.DbName, pid, product.batch_number, product.cost_price, product.selling_price, product.tax, product.discount, product.shipping_price, product.total_price);
                if (status > 0)
                {
                    for (int i = 0; i < product.Quantity_QtyStock.Count; i++)
                    {
                        int newstock = int.Parse(product.Quantity_Qty[i]) + int.Parse(product.Quantity_QtyStock[i]);
                        int response = ProductService.UpdateStock(user.DbName, product.Qid[i].ToString(), newstock.ToString(), product.Quantity_Total);
                    }
                    //addproductlog(pid, user.DbName, "1006", "StarBucks");  //Product & Quantity In Hand Log
                    return Content("<script language='javascript' type='text/javascript'>alert('Stock Updated Successfully!!!');location.href='" + @Url.Action("Index", "AllProducts") + "'</script>"); // Redirects to AllProducts View
                }
                return Content("<script language='javascript' type='text/javascript'>alert('Failed To Update Stock');location.href='" + @Url.Action("Index", "AddProduct") + "'</script>"); // Stays in Same View
            }
            return View();
        }

        public List<Product> convertproduct(string dbname, string pid)
        {
            SqlDataReader data = ProductService.GetProduct(dbname, pid);
            List<Product> product = new List<Product>();
            if (data.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(data);
                product = (from DataRow row in dt.Rows
                           select new Product()
                           {
                               ID = row["ID"].ToString(),
                               product_id = row["product_id"].ToString(),
                               product_name = row["product_name"].ToString(),
                               batch_number = row["Batch_number"].ToString(),
                               brand = row["brand"].ToString(),
                               model = row["model"].ToString(),
                               category = row["category"].ToString(),
                               sub_category = row["sub_category"].ToString(),
                               cost_price = row["cost_price"].ToString(),
                               selling_price = row["selling_price"].ToString(),
                               tax = row["tax"].ToString(),
                               discount = row["discount"].ToString(),
                               shipping_price = row["shipping_price"].ToString(),
                               total_price = row["total_price"].ToString(),
                               Measurement = row["Measurement"].ToString(),
                               weight = row["weight"].ToString(),
                               size = row["size"].ToString(),
                               color = row["color"].ToString(),
                               item_shape = row["item_shape"].ToString(),
                               product_consumable = row["product_consumable"].ToString(),
                               product_type = row["product_type"].ToString(),
                               product_perishability = row["product_perishability"].ToString(),
                               product_expirydate = row["product_expirydate"].ToString(),
                               product_description = row["product_description"].ToString(),
                               product_tags = row["product_tags"].ToString(),
                               product_images = row["product_images"].ToString(),
                               created_date = row["created_date"].ToString(),
                               status = row["status"].ToString()
                           }).Take(1).ToList();
            }
            return product;
        }

        public List<Product> convertquantity(string dbname, string pid)
        {
            SqlDataReader data = ProductService.GetQuantityInHand(dbname, pid);
            List<Product> product = new List<Product>();
            if (data.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(data);
                product = (from DataRow row in dt.Rows
                           select new Product()
                           {
                               Quantity_id = int.Parse(row["id"].ToString()),
                               Quantity_area = row["area"].ToString(),
                               Qty_Stock = row["Qty"].ToString(),
                               Quantity_Total = row["Total"].ToString(),
                               Qty_Reorder = row["Reorder_level"].ToString()
                           }).Take(5).ToList();
            }
            return product;
        }

        public void addproductlog(string pid, string dbname, string vendorid, string vendor)
        {
            int id = ProductLogMaxID(dbname); // Get Max Product Log ID
            var product = convertproduct(dbname, pid).FirstOrDefault();
            //ViewBag.AvailableQty = convertquantity(dbname, pid);
            var AvailableQty = convertquantity(dbname, pid);
            //ViewBag.total = ViewBag.AvailableQty[0].Quantity_Total;
            
            int productlog = ProductService.ProductFunctionalitieslog(dbname, id, product.ID, product.product_id, product.product_name, product.batch_number, product.brand, product.model, product.category, product.sub_category,
                        product.cost_price, product.selling_price, product.tax, product.discount, product.shipping_price, product.total_price, product.Measurement, product.weight,
                        product.size, product.color, product.item_shape, product.product_consumable, product.product_type, product.product_perishability, product.product_expirydate,
                        product.product_description, product.product_tags, product.product_images, product.created_date, product.status);
            if (productlog > 0)
            {
                for (int i = 0; i < AvailableQty.Count; i++)
                {
                    int response = ProductService.AddQuantityInHandLog(dbname, AvailableQty[i].Quantity_id, pid, AvailableQty[i].Quantity_area, AvailableQty[i].Qty_Stock, AvailableQty[i].Qty_Reorder, AvailableQty[i].Quantity_Total, vendorid, vendor);
                }
            }
        }
        public int ProductLogMaxID(string dbname)
        {
            var maxid = ProductService.GetProductLogMaxID(dbname);
            int id = 0;
            if (maxid.Read())
            {
                string max_log_id = maxid["id"].ToString();
                if (max_log_id == null || max_log_id == "")
                    id = 1;
                else
                    id = int.Parse(maxid["id"].ToString()) + 1;
            }
            else
                id = 1;
            return id;
        }
    }
}



//string total = ViewBag.total;
//string reorder = "10";
//int qty = ViewBag.AvailableQty[i].Quantity_id;
//int response = ProductService.AddQuantityInHandLog(dbname, qty, pid, ViewBag.AvailableQty[i].Quantity_area, ViewBag.AvailableQty[i].Qty_Stock[i], reorder, ViewBag.AvailableQty[i].Quantity_Total, vendorid, vendor);
