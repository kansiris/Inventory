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
                    ViewBag.weights = convert(user.DbName, "getallweights");
                    ViewBag.sizes = convert(user.DbName, "getallsizes");
                    ViewBag.colors = convert(user.DbName, "getallcolors");
                    ViewBag.itemshapes = convert(user.DbName, "getallitemshapes");
                    ViewBag.categories = convert(user.DbName, "getallcategories");
                    ViewBag.brands = convert(user.DbName, "getallbrands");
                    //ViewBag.brandmodels = convert(user.DbName, "getallmodels");
                }
                else
                {
                    ViewBag.count = "";
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string command, Product product)
        {
            if (command == "AddProduct")
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                int count = ProductService.ProductFunctionalities(command, user.DbName, product.product_name, product.brand, product.model, product.category, product.sub_category,
                    product.cost_price, product.selling_price, product.tax, product.discount, product.shipping_price, product.total_price, product.Measurement, product.weight,
                    product.size, product.color, product.item_shape, product.product_consumable, product.product_type, product.product_perishability, product.product_expirydate,
                    product.product_description, product.product_tags);
                if (count > 1)
                    return Content("<script language='javascript' type='text/javascript'>alert('Product Added Successfully!!!');location.href='" + @Url.Action("Index", "AllProducts") + "'</script>"); // Redirects to AllProducts View
                else
                    return Content("<script language='javascript' type='text/javascript'>alert('Failed To Add Product');location.href='" + @Url.Action("Index", "Products") + "'</script>"); // Stays in Same View
            }
            return View();
        }

        public List<ProductItems> convert(string dbname, string type)
        {
            List<ProductItems> result = new List<ProductItems>();
            var reader = ProductService.GetProductItems(dbname, type);
            DataTable dt = new DataTable();
            dt.Load(reader);
            if (type == "getallweights")
            {
                result = (from DataRow row in dt.Rows select new ProductItems() { weight_id = row["weight_id"].ToString(), weight = row["weight"].ToString() }).ToList();
            }
            if (type == "getallsizes")
            {
                result = (from DataRow row in dt.Rows select new ProductItems() { size_id = row["size_id"].ToString(), size = row["size"].ToString() }).ToList();
            }
            if (type == "getallcolors")
            {
                result = (from DataRow row in dt.Rows select new ProductItems() { color_id = row["color_id"].ToString(), color = row["color"].ToString() }).ToList();
            }
            if (type == "getallitemshapes")
            {
                result = (from DataRow row in dt.Rows select new ProductItems() { itemshape_id = row["itemshape_id"].ToString(), itemshape = row["itemshape"].ToString() }).ToList();
            }
            if (type == "getallcategories")
            {
                result = (from DataRow row in dt.Rows select new ProductItems() { category_id = row["category_id"].ToString(), category = row["category"].ToString() }).ToList();
            }
            if (type == "getallbrands")
            {
                result = (from DataRow row in dt.Rows select new ProductItems() { brand_id = row["brand_id"].ToString(), brand = row["brand"].ToString() }).ToList();
            }
            if (type == "getallmodels")
            {
                result = (from DataRow row in dt.Rows select new ProductItems() { brandmodel_id = row["model_id"].ToString(), brandmodel = row["model"].ToString() }).ToList();
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
                if (type == "brand")
                    result = (from DataRow row in dt.Rows select new ProductItems() { brandmodel_id = row["model_id"].ToString(), brandmodel = row["model"].ToString() }).ToList();
                return Json(result);
            }
            return Json("empty");
        }

        public JsonResult UpdateProductsItems(string command, string id,Product product)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            //int count = ProductService.ProductItems(user.DbName, command, product.weight, product.size,product.color, product.item_shape, product.category, product.sub_category,product.brand,product.model, id);
            //if (count > 1)
            //    return Json(command);
            //else
            //    return Json("Failed");
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}