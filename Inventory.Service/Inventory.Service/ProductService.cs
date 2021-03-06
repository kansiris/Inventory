﻿using System;
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
           string product_type, string product_perishability, string product_expirydate, string product_description, string product_tags, string product_images,
           string sgst, string cgst, string igst, string hsncode)
        {
            return ProductRepository.ProductFunctionalities(type, dbname, id, product_id, product_name, batch_number, brand, model, category, sub_category, cost_price, selling_price, tax, discount,
                shipping_price, total_price, Measurement, weight, size, color, item_shape, product_consumable, product_type, product_perishability, product_expirydate,
                product_description, product_tags, product_images, sgst, cgst, igst, hsncode);
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
        public static int AddQuantityInHand(string dbname, string product_id, string area, int Qty, string reorder, string Total)
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
        public static int UpdateStock(string dbname, string id, int qty,string total)
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

        #region Add Product Log
        public static int ProductFunctionalitieslog(string dbname, int id, string mainproduct_id, string product_id, string product_name, string batch_number,
                string brand, string model, string category, string sub_category, string cost_price, string selling_price, string tax,
                string discount, string shipping_price, string total_price, string Measurement, string weight, string size, string color, string item_shape,
                string product_consumable, string product_type, string product_perishability, string product_expirydate, string product_description,
                string product_tags, string product_images, string created_date, string status)
        {
            return ProductRepository.ProductFunctionalitieslog(dbname, id,mainproduct_id, product_id, product_name, batch_number, brand, model,
                category, sub_category, cost_price, selling_price, tax, discount, shipping_price, total_price, Measurement, weight, size, color, item_shape,
                product_consumable, product_type, product_perishability, product_expirydate, product_description, product_tags, product_images, created_date, status);
        }
        #endregion

        #region Add Quantity In Hand Log
        public static int AddQuantityInHandLog(string dbname, int mainquantity_id, string product_id, string area, int Qty,string reorder,string Total, string vendor_id, string vendor)
        {
            return ProductRepository.AddQuantityInHandLog(dbname, mainquantity_id, product_id, area, Qty, reorder, Total, vendor_id, vendor);
        }
        #endregion

        #region Get Product Log Max ID
        public static SqlDataReader GetProductLogMaxID(string dbname)
        {
            return ProductRepository.GetProductLogMaxID(dbname);
        }
        #endregion

        #region Update Product Stock & Re-Oreder Level
        public static int UpdateReorder(string dbname, string id, int qty, string reorder, string total)
        {
            return ProductRepository.UpdateReorder(dbname, id, qty, reorder, total);
        }
        #endregion

        #region Product Image Removal
        public static int removeproductimage(string dbname, string id,string updatedimage)
        {
            return ProductRepository.removeproductimage(dbname, id, updatedimage);

        }
        #endregion

        #region Update Max Warehouse Qty
        public static int updateMaxWarehouseQty(string dbname, string product_id, string id, int qty, string total)
        {
            return ProductRepository.updateMaxWarehouseQty(dbname, product_id,id,qty, total);
        }
        #endregion

        #region Get Max Warehouse Qty
        public static SqlDataReader GetMaxWarehouseQty(string dbname, string id)
        {
            return ProductRepository.GetMaxWarehouseQty(dbname, id);
        }
        #endregion

        //#region Update Product Stock
        //public static int UpdateStock(string dbname, string id, string total)
        //{
        //    return ProductRepository.UpdateStock(dbname, id, total);
        //}
        //#endregion

        //to get all categories
        public static SqlDataReader GetAllCategories(string dbname)
        {
            return ProductRepository.GetAllCategories(dbname);
        }

        //to get all subcategories based on category id
        public static SqlDataReader GetAllSubCategories(string dbname,string category_id)
        {
            return ProductRepository.GetAllSubCategories(dbname, category_id);
        }


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

        public static int Addtocart(string dbname,string cid,string product_id, string product_name, string cost_price, string Quantity, string Measurement, string total_price,string product_images)
        {
            
            return ProductRepository.Addtocart(dbname, cid, product_id, product_name, cost_price, Quantity, Measurement, total_price, product_images);
        }
        //Updatecart
            public static int Updatecart(string dbname, int cart_id, string Quantity,string total_price)
        {

            return ProductRepository.Updatecart(dbname, cart_id, Quantity,total_price);
        }

        public static SqlDataReader Addtocartbyid(string dbname, string cid)
        {
            return ProductRepository.Addtocartbyid(dbname, cid);
        }
        //for images

        
            public static SqlDataReader Getimages(string dbname, string product_id)
        {
            return ProductRepository.Getimages(dbname, product_id);
        }
        //for genarating po
        public static int GenaratePurchaseOrder(string dbname, string cid, string product_id, string cname, string created_date, string Prchaseorder_no,string shipping_date,string shipping_terms, string product_name, string description, string quantity
                , string price, string total_price, string remarks, string sub_total,/* float vat, float discount,*/ string grand_total)
        {
            return ProductRepository.GenaratePurchaseOrder(dbname, cid, product_id, cname, created_date, Prchaseorder_no, shipping_date, shipping_terms, product_name, description, quantity,price, total_price, remarks,sub_total,/* vat, discount,*/ grand_total);
        }
        
            public static int Removefromcart(string dbname, int cart_id)
        {
            return ProductRepository.Removefromcart(dbname, cart_id);
        }

        
            public static int Emptycart(string dbname, string cid)
        {
            return ProductRepository.Emptycart(dbname, cid);
        }
        public static SqlDataReader Getcartdata(string dbname, string cid)
        {
            return ProductRepository.Getcartdata(dbname, cid);
        }

        public static SqlDataReader Getpodata(string dbname, string Prchaseorder_no)
        {
            return ProductRepository.Getpodata(dbname, Prchaseorder_no);
        }

        public static SqlDataReader Getpoproductdata(string dbname, string Prchaseorder_no)
        {
            return ProductRepository.Getpoproductdata(dbname, Prchaseorder_no);
        }
        //for quty in hand
        public static SqlDataReader GetqtyInHand(string dbname, string product_id)
        {
            return ProductRepository.GetqtyInHand(dbname, product_id);
        }

        //public static SqlDataReader checkcartdata(string dbname, string product_name, string Measurement,string cid)
        //{
        //    return ProductRepository.checkcartdata(dbname, product_name, Measurement,cid);
        //}

        public static SqlDataReader checkcartdata(string dbname, string product_id, string cid)
        {
            return ProductRepository.checkcartdata(dbname, product_id,cid);
        }

        public static SqlDataReader checkponum(string dbname, string Prchaseorder_no)
        {
            return ProductRepository.checkponum(dbname, Prchaseorder_no);
        }
        //for displayiung Po's

        public static SqlDataReader PosOfCustomer(string dbname, string cid)
        {
            return ProductRepository.PosOfCustomer(dbname, cid);
        }
    }
}
