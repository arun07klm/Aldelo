using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Aldelo_Report_Web.Api_Controller
{
    public class SaleCategoryController : ApiController
    {
        [System.Web.Http.HttpGet]
        public List<saleCategory> GetSaleCategories()
        {

            List<saleCategory> objSaleCategory = new List<saleCategory>();
            DataSet ds = new DataSet();
            //OleDbConnection con =new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\\Project\\Restaurant_Application\\Calicut Live.mdb");
            string strConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            OleDbConnection con = new OleDbConnection(strConnectionString);
            con.Open();
            try
            {
                string query = @" SELECT        Format(OrderHeaders.OrderDateTime, 'mm/dd/yyyy') AS Dates, format(SUM(OrderTransactions.ExtendedPrice), '0.000') AS Amount, MenuCategories.MenuCategoryText As Category
FROM            (((OrderTransactions LEFT OUTER JOIN
                         MenuItems ON MenuItems.MenuItemID = OrderTransactions.MenuItemID) LEFT OUTER JOIN
                         OrderHeaders ON OrderHeaders.OrderID = OrderTransactions.OrderID) LEFT OUTER JOIN
                         MenuCategories ON MenuCategories.MenuCategoryID = MenuItems.MenuCategoryID)

GROUP BY Format(OrderHeaders.OrderDateTime, 'mm/dd/yyyy'), MenuCategories.MenuCategoryText"; //WHERE        (MenuItems.MenuCategoryID = 2)
                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                objSaleCategory = (from DataRow dr in dt.Rows
                                  select new saleCategory()
                                  {
                                      Dates = (dr["Dates"]).ToString(),
                                      Amount = (dr["Amount"]).ToString(),
                                      Category = (dr["Category"]).ToString(),
                                  }).ToList();
            }
            catch (Exception ex)
            {

            }


            finally
            {
                con.Close();
            }

            return objSaleCategory;

        }

        public List<saleCategory> GetSaleCategoriesSearch(saleCategory objSale)
        {

            List<saleCategory> objSaleCategory = new List<saleCategory>();
            DataSet ds = new DataSet();
            //OleDbConnection con =new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\\Project\\Restaurant_Application\\Calicut Live.mdb");
            string strConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            OleDbConnection con = new OleDbConnection(strConnectionString);
            con.Open();
            try
            {
                string query = @" SELECT        Format(OrderHeaders.OrderDateTime, 'mm/dd/yyyy') AS Dates, format(SUM(OrderTransactions.ExtendedPrice), '0.000') AS Amount, MenuCategories.MenuCategoryText As Category
FROM            (((OrderTransactions LEFT OUTER JOIN
                         MenuItems ON MenuItems.MenuItemID = OrderTransactions.MenuItemID) LEFT OUTER JOIN
                         OrderHeaders ON OrderHeaders.OrderID = OrderTransactions.OrderID) LEFT OUTER JOIN
                         MenuCategories ON MenuCategories.MenuCategoryID = MenuItems.MenuCategoryID)
                    Where  Format(OrderHeaders.OrderDateTime, 'mm/dd/yyyy') between " + objSale.fromDate+ @"  and " + objSale.toDate + @"     and OrderTransactions.MenuItemID=" + objSale.Id+@"   
GROUP BY Format(OrderHeaders.OrderDateTime, 'mm/dd/yyyy'), MenuCategories.MenuCategoryText"; //WHERE        (MenuItems.MenuCategoryID = 2)
                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                objSaleCategory = (from DataRow dr in dt.Rows
                                   select new saleCategory()
                                   {
                                       Dates = (dr["Dates"]).ToString(),
                                       Amount = (dr["Amount"]).ToString(),
                                       Category = (dr["Category"]).ToString(),
                                   }).ToList();
            }
            catch (Exception ex)
            {

            }


            finally
            {
                con.Close();
            }

            return objSaleCategory;

        }


        public List<saleCategory> GetAllCategories()
        {

            List<saleCategory> objSaleCategory = new List<saleCategory>();
            DataSet ds = new DataSet();
            //OleDbConnection con =new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\\Project\\Restaurant_Application\\Calicut Live.mdb");
            string strConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            OleDbConnection con = new OleDbConnection(strConnectionString);
            con.Open();
            try
            {
                string query = @" SELECT MenuCategoryID AS id, MenuCategoryText AS name FROM  MenuCategories";

                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                objSaleCategory = (from DataRow dr in dt.Rows
                                   select new saleCategory()
                                   {
                                       Id = (dr["id"]).ToString(),
                                       Name = (dr["name"]).ToString(),                                     
                                   }).ToList();
            }
            catch (Exception ex)
            {

            }


            finally
            {
                con.Close();
            }

            return objSaleCategory;

        }
        public class saleCategory
        {
            public string Id { set; get; }
            public string Name { set; get; }
            public string Dates { set; get; }
            public string Amount { set; get; }
            public string Category { set; get; }

            public string fromDate { set; get; }
            public string toDate { set; get; }
        }
    }
}
