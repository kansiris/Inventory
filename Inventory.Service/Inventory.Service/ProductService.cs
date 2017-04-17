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
        public static SqlDataReader GetSubCategory(string dbname, string categoryid)
        {
            return ProductRepository.GetSubCategory(dbname, categoryid);
        }
        #endregion

        #region Add/Remove ProductItems
        public static int ProductItems(string dbname, string command, string weight, string size, string color, string itemshape, string category, string subcategory, string id)
        {
            return ProductRepository.ProductItems(dbname, command, weight, size, color, itemshape, category, subcategory, id);
        }
        #endregion

        #region Get Product Items
        public static SqlDataReader GetProductItems(string dbname, string command)
        {
            return ProductRepository.GetProductItems(dbname, command);
        }
        #endregion
    }
}
