using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Repository;
using System.Data.SqlClient;

namespace Inventory.Service
{
    public class ProductService
    {
        #region Get Warehouse Count
        public static SqlDataReader GetWarehouseCount(string dbname)
        {
            return ProductRepository.GetWarehouseCount(dbname);
        }
        #endregion

        #region Sorting Sub-Category
        public static SqlDataReader GetSubCategory(string dbname, string type, string categoryid)
        {
            return ProductRepository.GetSubCategory(dbname, type, categoryid);
        }
        #endregion

        #region Add/Remove ProductItems
        public static int ProductItems(string dbname, string command, string weight, string size, string color, string itemshape, string assignedcategoryid, string category, string subcategory, string brand, string model, string id)
        {
            return ProductRepository.ProductItems(dbname, command, weight, size, color, itemshape, assignedcategoryid, category, subcategory, brand, model, id);
        }
        #endregion

        #region Get Product Items
        public static SqlDataReader GetProductItems(string dbname, string command, string id)
        {
            return ProductRepository.GetProductItems(dbname, command, id);
        }
        #endregion

        #region Product Functionalities
        public static int ProductFunctionalities(string type, string dbname, int id, string product_id, string product_name, string batch_number, string brand, string model, string category, string sub_category, string cost_price, string selling_price, string tax,
           string discount, string shipping_price, string total_price, string Measurement, string weight, string size, string color, string item_shape, string product_consumable,
           string product_type, string product_perishability, string product_expirydate, string product_description, string product_tags, string product_images)
        {
            return ProductRepository.ProductFunctionalities(type, dbname, id, product_id, product_name, batch_number, brand, model, category, sub_category, cost_price, selling_price, tax, discount,
                shipping_price, total_price, Measurement, weight, size, color, item_shape, product_consumable, product_type, product_perishability, product_expirydate,
                product_description, product_tags, product_images);
        }
        #endregion

        #region Get All Products
        public static SqlDataReader GetAllProducts(string dbname)
        {
            return ProductRepository.GetAllProducts(dbname);
        }
        #endregion

        #region Get Product Max ID
        public static SqlDataReader GetProductMaxID(string dbname)
        {
            return ProductRepository.GetProductMaxID(dbname);
        }
        #endregion

        #region Add Quantity In Hand
        public static int AddQuantityInHand(string dbname, string product_id, string area, string Qty, string reorder, string Total)
        {
            return ProductRepository.AddQuantityInHand(dbname, product_id, area, Qty, reorder, Total);
        }
        #endregion

        #region Active/InActive Product
        public static int productstatus(string dbname, string id, string status)
        {
            return ProductRepository.productstatus(dbname, id, status);
        }
        #endregion

        #region Get Product
        public static SqlDataReader GetProduct(string dbname, string id)
        {
            return ProductRepository.GetProduct(dbname, id);
        }
        #endregion

        #region Get Quantity In Hand
        public static SqlDataReader GetQuantityInHand(string dbname, string id)
        {
            return ProductRepository.GetQuantityInHand(dbname, id);
        }
        #endregion

        #region Update Product
        public static int updateproduct(string dbname, string id, string batch, string cost_price, string selling_price, string tax, string discount, string shipping, string total_price)
        {
            return ProductRepository.updateproduct(dbname, id, batch, cost_price, selling_price, tax, discount, shipping, total_price);
        }
        #endregion

        #region Update Stock
        public static int UpdateStock(string dbname, string id, string qty,string total)
        {
            return ProductRepository.UpdateStock(dbname,id,qty,total);
        }
        #endregion

        #region Reordering
        public static SqlDataReader Reordering(string dbname)
        {
            return ProductRepository.Reordering(dbname);
        }
        #endregion

        //to get subcategory products
        public static SqlDataReader Getproductsbysubcategory(string dbname, string sub_category)
        {
            return ProductRepository.Getproductsbysubcategory(dbname, sub_category);
        }

        public static SqlDataReader Getdistinctproducts(string dbname)
        {
            return ProductRepository.Getdistinctproducts(dbname);
        }

        public static SqlDataReader Getdescripton(string dbname, string product_name)
        {
            return ProductRepository.Getdescripton(dbname, product_name);
        }

        public static int Addtocart(string dbname,string id, string product_name, string cost_price, string Quantity, string Measurement, /*string weight,*/ string total_price)
        {
            
            return ProductRepository.Addtocart(dbname, id, product_name, cost_price, Quantity, Measurement, total_price);
        }

        
             public static SqlDataReader Addtocartbyid(string dbname, string id)
        {
            return ProductRepository.Addtocartbyid(dbname, id);
        }

        
            public static int Removefromcart(string dbname, int cart_id)
        {
            return ProductRepository.Removefromcart(dbname, cart_id);
        }
        public static SqlDataReader Getcartdata(string dbname, string id)
        {
            return ProductRepository.Getcartdata(dbname, id);
        }
        
             public static SqlDataReader checkcartdata(string dbname, string product_name, string Measurement)
        {
            return ProductRepository.checkcartdata(dbname, product_name, Measurement);
        }

    }
}
