using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Service;
using Inventory.Content;
using System.Data;
using Inventory.Models;
using System.Data.SqlClient;

namespace Inventory.Controllers
{
    public class AddProductController : Controller
    {
        // GET: AddProduct
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var count = ProductService.GetWarehouseCount(user.DbName);
                if (count.HasRows)
                {
                    ViewBag.count = "1";
                    ViewBag.weights = convert(user.DbName, "weight", "");
                    ViewBag.sizes = convert(user.DbName, "size", "");
                    ViewBag.colors = convert(user.DbName, "color", "");
                    ViewBag.itemshapes = convert(user.DbName, "itemshape", "");
                    ViewBag.categories = convert(user.DbName, "category", "");
                    ViewBag.brands = convert(user.DbName, "brand", "");
                    //ViewBag.brandmodels = convert(user.DbName, "getallmodels");
                    ViewBag.AvailableWarehouses = WarehouseQuantity(user.DbName);
                }
                else
                {
                    ViewBag.count = "";
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string command, Product product, HttpPostedFileBase file)
        {
            if (command == "AddProduct")
            {
                if (file != null && file.ContentLength > 0)
                {
                    
                    var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                    int id = ProductMaxID(user.DbName); // Get Max Product ID
                    string product_id = "P" + id;
                    ViewBag.AvailableWarehouses = WarehouseQuantity(user.DbName);
                    string imagename="";
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file1 = Request.Files[i];
                        if (file1 != null && file1.ContentLength > 0)
                        {
                            imagename = imagename+"," +  product_id+"_"+file1.FileName;
                            file1.SaveAs(Server.MapPath("~/ProductImages/" + product_id + "_" + file1.FileName));
                        }

                    }
                    imagename = imagename.TrimStart(',');
                    int count = ProductService.ProductFunctionalities(command, user.DbName, id, product_id, product.product_name, product.batch_number, product.brand, product.model, product.category, product.sub_category,
                        product.cost_price, product.selling_price, product.tax, product.discount, product.shipping_price, product.total_price, product.Measurement, product.weight,
                        product.size, product.color, product.item_shape, product.product_consumable, product.product_type, product.product_perishability, product.product_expirydate,
                        product.product_description, product.product_tags, imagename);
                    if (count > 0)
                    {
                        for (int i = 0; i < product.Quantity_Qty.Count; i++)
                        {
                            int response = ProductService.AddQuantityInHand(user.DbName, product_id, ViewBag.AvailableWarehouses[i].wh_Shortname, product.Quantity_Qty[i], product.Quantity_Total);
                        }
                        return Content("<script language='javascript' type='text/javascript'>alert('Product Added Successfully!!!');location.href='" + @Url.Action("Index", "AllProducts") + "'</script>"); // Redirects to AllProducts View
                    }
                    return Content("<script language='javascript' type='text/javascript'>alert('Failed To Add Product');location.href='" + @Url.Action("Index", "AddProduct") + "'</script>"); // Stays in Same View
                }
            }
        
            return View();
        }

        public List<ProductItems> convert(string dbname, string type, string id)
        {
            List<ProductItems> result = new List<ProductItems>();
            var reader = ProductService.GetProductItems(dbname, type, id);
            DataTable dt = new DataTable();
            dt.Load(reader);
            if (type == "weight")
            {
                result = (from DataRow row in dt.Rows select new ProductItems() { weight_id = row["weight_id"].ToString(), weight = row["weight"].ToString() }).ToList();
            }
            if (type == "size")
            {
                result = (from DataRow row in dt.Rows select new ProductItems() { size_id = row["size_id"].ToString(), size = row["size"].ToString() }).ToList();
            }
            if (type == "color")
            {
                result = (from DataRow row in dt.Rows select new ProductItems() { color_id = row["color_id"].ToString(), color = row["color"].ToString() }).ToList();
            }
            if (type == "itemshape")
            {
                result = (from DataRow row in dt.Rows select new ProductItems() { itemshape_id = row["itemshape_id"].ToString(), itemshape = row["itemshape"].ToString() }).ToList();
            }
            if (type == "category")
            {
                result = (from DataRow row in dt.Rows select new ProductItems() { category_id = row["category_id"].ToString(), category = row["category"].ToString() }).ToList();
            }
            if (type == "brand")
            {
                result = (from DataRow row in dt.Rows select new ProductItems() { brand_id = row["brand_id"].ToString(), brand = row["brand"].ToString() }).ToList();
            }
            if (type == "model")
            {
                result = (from DataRow row in dt.Rows select new ProductItems() { brandmodel_id = row["model_id"].ToString(), brandmodel = row["model"].ToString() }).ToList();
            }
            if (type == "subcategory")
            {
                result = (from DataRow row in dt.Rows select new ProductItems() { subcategory_id = row["subcategory_id"].ToString(), subcategory = row["sub_category"].ToString() }).ToList();
            }
            return result;
        }

        public JsonResult getsub(string type, string id)
        {
            List<ProductItems> result = new List<ProductItems>();
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = ProductService.GetSubCategory(user.DbName, type, id);
            if (data.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(data);
                if (type == "category")
                    result = (from DataRow row in dt.Rows select new ProductItems() { subcategory_id = row["subcategory_id"].ToString(), subcategory = row["sub_category"].ToString() }).ToList();
                //if (type == "brand")
                //    result = (from DataRow row in dt.Rows select new ProductItems() { brandmodel_id = row["model_id"].ToString(), brandmodel = row["model"].ToString() }).ToList();
                return Json(result);
            }
            return Json("empty");
        }

        public JsonResult UpdateProductsItems(string command, string id, Product product)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            int count = ProductService.ProductItems(user.DbName, command, product.weight, product.size, product.color, product.item_shape, product.category_id, product.category, product.sub_category, product.brand, product.model, id);
            if (count > 0)
            {
                string replace;
                // var data = ProductService.GetProductItems(user.DbName, command.Replace("add", ""), id);
                if (id != null && id != "")
                 replace = "del"; 
                else
                 replace = "add"; 
                var records = convert(user.DbName, command.Replace(replace, ""), product.category_id).Distinct();
                var result = new { command = command, records = records };
                return Json(result);
            }
            else
                return Json("Failed");
            //return Json(JsonRequestBehavior.AllowGet);
        }

        public int ProductMaxID(string dbname)
        {
            var maxid = ProductService.GetProductMaxID(dbname);
            int id = 0;
            if (maxid.Read())
                id = int.Parse(maxid["id"].ToString()) + 1;
            else
                id = 1;
            return id;
        }

        public List<Warehouse> WarehouseQuantity(string dbname)
        {
            SqlDataReader value = WHservice.getwarehousedtls(dbname);
            DataTable dt = new DataTable();
            dt.Load(value);
            List<Warehouse> warehouse = new List<Warehouse>();
            warehouse = (from DataRow row in dt.Rows
                         select new Warehouse()
                         {
                             //wh_Id = row["wh_id"].ToString(),
                             wh_name = row["wh_name"].ToString(),
                             wh_Shortname = row["wh_Shortname"].ToString(),
                             //conperson = row["Contact_person"].ToString(),
                             //Email = row["Email"].ToString(),
                             // Wh_logo = row["Wh_Image"].ToString()
                         }).OrderByDescending(m => m.wh_Id).ToList();
            return warehouse;
        }
    }
}