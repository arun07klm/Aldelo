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
    public class SaleMenuItemController : ApiController
    {

        [System.Web.Http.HttpGet]
        public List<MenuItem> GetSaleMenuItem()
        {

            List<MenuItem> objSaleCategory = new List<MenuItem>();
            DataSet ds = new DataSet();
            //OleDbConnection con =new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\\Project\\Restaurant_Application\\Calicut Live.mdb");
            string strConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            OleDbConnection con = new OleDbConnection(strConnectionString);
            con.Open();
            try
            {
       string query = @" SELECT MenuItems.MenuItemID, MenuItems.MenuItemText, Format(OrderHeaders.OrderDateTime, 'mm/dd/yyyy') AS Dates, OrderTransactions.Quantity, format(SUM(OrderTransactions.ExtendedPrice), '0.000') 
                         AS Amount FROM ((MenuItems LEFT OUTER JOIN                       
                         OrderTransactions ON OrderTransactions.MenuItemID = MenuItems.MenuItemID) LEFT OUTER JOIN
                         OrderHeaders ON OrderHeaders.OrderID = OrderTransactions.OrderID)
                         GROUP BY Format(OrderHeaders.OrderDateTime, 'mm/dd/yyyy'), MenuItems.MenuItemID, MenuItems.MenuItemText, OrderTransactions.Quantity"; //WHERE        (MenuItems.MenuCategoryID = 2)
                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                objSaleCategory = (from DataRow dr in dt.Rows
                                   select new MenuItem()
                                   {
                                       MenuItemId = (dr["MenuItemId"]).ToString(),
                                       MenuItemText = (dr["MenuItemText"]).ToString(),
                                       Dates = (dr["Dates"]).ToString(),
                                       Quantity = (dr["Quantity"]).ToString(),
                                       Amount = (dr["Amount"]).ToString(),
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

        public List<MenuItem> GetAllmenuItem()
        {

            List<MenuItem> objmenuItem = new List<MenuItem>();
            DataSet ds = new DataSet();
            //OleDbConnection con =new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\\Project\\Restaurant_Application\\Calicut Live.mdb");
            string strConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            OleDbConnection con = new OleDbConnection(strConnectionString);
            con.Open();
            try
            {
                string query = @" SELECT menuitemId AS id, menuitemtext AS name FROM  menuitems";

                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                objmenuItem = (from DataRow dr in dt.Rows
                                   select new MenuItem()
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

            return objmenuItem;

        }
        public class MenuItem
        {
            public string MenuItemId { set; get; }
            public string MenuItemText { set; get; }
            public string Dates { set; get; }
            public string Quantity { set; get; }
            public string Amount { set; get; }

            public string Id { set; get; }
            public string Name { set; get; }

        }

    }
}
