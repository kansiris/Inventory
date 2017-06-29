using System.Web;
using System.Web.Optimization;

namespace Inventory
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Vendor
            bundles.Add(new ScriptBundle("~/bundles/Vendors").Include("~/Scripts/vendor.js"));
            //customer
            bundles.Add(new ScriptBundle("~/bundles/Customers").Include("~/Scripts/customer.js"));

            //shop product
            bundles.Add(new ScriptBundle("~/bundles/ShopProduct").Include("~/Scripts/shopproduct.js"));

            //payments
            bundles.Add(new ScriptBundle("~/bundles/Payments").Include("~/Scripts/payments.js"));

            //create invoice
            bundles.Add(new ScriptBundle("~/bundles/Invoices").Include("~/Scripts/invoice.js"));
            //User Profile
            bundles.Add(new ScriptBundle("~/bundles/profile").Include("~/Scripts/UserProfile.js"));
            //Add Product
            bundles.Add(new ScriptBundle("~/bundles/product").Include("~/Scripts/Product.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
