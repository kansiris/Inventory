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
using System.IO;

namespace Inventory.Controllers
{
    public class AddProductController : Controller
    {
        // GET: AddProduct
        public ActionResult Index(string pid)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var count = ProductService.GetWarehouseCount(user.DbName);
                if (count.Read())
                {
                    if (count["wcount"].ToString() != "0")
                    {
                        ViewBag.count = "addproduct";
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
                        ViewBag.count = "empty";
                    }
                }
                else
                {
                    ViewBag.count = "empty";
                }
                if (pid != "" && pid != null)
                {
                    ViewBag.AvailableWarehouses = "";
                    ViewBag.count = "editproduct";
                    ViewBag.Title = ":: Update Product ::";
                    ViewBag.product = convertproduct(user.DbName, pid);
                    ViewBag.AvailableQty = convertquantity(user.DbName, pid);
                    ViewBag.total = ViewBag.AvailableQty[0].Quantity_Total;
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string command, Product product, HttpPostedFileBase file,string pid)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;

                #region Add Product
                if (command == "AddProduct")
                {
                    int id = ProductMaxID(user.DbName); // Get Max Product ID
                    string product_id = "P" + id;
                    ViewBag.AvailableWarehouses = WarehouseQuantity(user.DbName);
                    string imagename = "";
                    //Uploaded Images
                    if (file != null && file.ContentLength > 0)
                    {
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            var file1 = Request.Files[i];
                            if (file1 != null && file1.ContentLength > 0)
                            {
                                imagename = imagename + "," + product_id + "_" + file1.FileName;
                                file1.SaveAs(Server.MapPath("~/ProductImages/" + product_id + "_" + file1.FileName));
                            }
                        }
                        imagename = imagename.TrimStart(',');
                    }
                    //Library Images
                    if (product.product_images != null)
                    {
                        var images = product.product_images.Split(',');
                        for (int i = 0; i < images.Count(); i++)
                        {
                            string fileName = product_id + "_" + images[i];
                            string pathString = System.IO.Path.Combine(Server.MapPath("~/ProductImages/"), fileName);
                            string spath = Server.MapPath("~/images/" + images[i] + "");
                            System.IO.File.Copy(spath, pathString); //copy image from images folder to productimages folder
                            imagename = imagename + "," + fileName;
                        }
                        imagename = imagename.TrimStart(',');
                    }
                    int count = ProductService.ProductFunctionalities(command, user.DbName, id, product_id, product.product_name, product.batch_number, product.brand, product.model, product.category, product.sub_category,
                        product.cost_price, product.selling_price, product.tax, product.discount, product.shipping_price, product.total_price, product.Measurement, product.weight,
                        product.size, product.color, product.item_shape, product.product_consumable, product.product_type, product.product_perishability, product.product_expirydate,
                        product.product_description, product.product_tags, imagename);
                    if (count > 0)
                    {
                        for (int i = 0; i < product.Quantity_Qty.Count; i++)
                        {
                            int response = ProductService.AddQuantityInHand(user.DbName, product_id, ViewBag.AvailableWarehouses[i].wh_Shortname, product.Quantity_Qty[i], product.Reorder_level[i], product.Quantity_Total);
                        }
                        TempData["smsg"] = "Product Added Successfully!!!";
                        //return Content("<script language='javascript' type='text/javascript'>alert('Product Added Successfully!!!');location.href='" + @Url.Action("Index", "AllProducts") + "'</script>"); // Redirects to AllProducts View
                    }
                    TempData["msg"] = "Failed To Add Product";
                    return RedirectToAction("Index", "AllProducts");
                    //return Content("<script language='javascript' type='text/javascript'>alert('Failed To Add Product');location.href='" + @Url.Action("Index", "AddProduct") + "'</script>"); // Stays in Same View
                }
                #endregion

                #region Update Product
                if (command == "UpdateProduct")
                {
                    int count = ProductService.ProductFunctionalities(command, user.DbName, 1, pid, product.product_name, product.batch_number, product.brand, product.model, product.category, product.sub_category,
                        product.cost_price, product.selling_price, product.tax, product.discount, product.shipping_price, product.total_price, product.Measurement, product.weight,
                        product.size, product.color, product.item_shape, product.product_consumable, product.product_type, product.product_perishability, product.product_expirydate,
                        product.product_description, product.product_tags.Replace("!",""), product.product_images);
                    if (count > 0)
                    {
                        for (int i = 0; i < product.Quantity_Qty.Count; i++)
                        {
                            int response = ProductService.UpdateReorder(user.DbName, product.Qid[i].ToString(), product.Quantity_Qty[i], product.Reorder_level[i], product.Quantity_Total);
                        }
                        TempData["smsg"] = "Product Updated Successfully!!!"; //Success Message
                    }
                    TempData["msg"] = "Failed To update product"; // Failure Message
                    return RedirectToAction("Index", "AllProducts");

                }
                #endregion
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
                         }).OrderByDescending(m => m.wh_Id).Take(5).ToList();
            return warehouse;
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
                           }).ToList();
            }
            return product;
        }
    }
}