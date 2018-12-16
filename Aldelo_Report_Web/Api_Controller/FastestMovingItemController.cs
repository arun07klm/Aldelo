using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Aldelo_Report_Web.Api_Controller
{
    public class FastestMovingItemController : ApiController
    {
        [System.Web.Http.HttpPost]
        public List<MenuItem> GetFastestMovingItem(DataTable dtBody)
        {

            List<MenuItem> objSaleCategory = new List<MenuItem>();
            DataSet ds = new DataSet();
            //OleDbConnection con =new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\\Project\\Restaurant_Application\\Calicut Live.mdb");
            string strConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            OleDbConnection con = new OleDbConnection(strConnectionString);
            con.Open();
            try
            {
                string query = @" SELECT a.MenuItemID, SUM(a.Quantity) AS totalCount, mItms.MenuItemText
                                        FROM     ((OrderTransactions a INNER JOIN
                  MenuItems mItms ON a.MenuItemID = mItms.MenuItemID) INNER JOIN
                  OrderHeaders ordHead ON a.OrderID = ordHead.OrderID)
                    where (FORMAT(ordHead.OrderDateTime, 'dd-mm-yyyy') BETWEEN " +  dtBody.Rows[0]["FromDate"].ToString() + @"  AND " +  dtBody.Rows[0]["Todate"].ToString() + @") 
                     GROUP BY a.MenuItemID, mItms.MenuItemText
                    HAVING (SUM(a.Quantity) IN
                                          (SELECT TOP 10 SUM(Quantity) AS Expr1
                                           FROM      OrderTransactions c
                                           GROUP BY MenuItemID))
                    ORDER BY SUM(a.Quantity)"; //WHERE        (MenuItems.MenuCategoryID = 2)
                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                objSaleCategory = (from DataRow dr in dt.Rows
                                   select new MenuItem()
                                   {
                                      MenuItemText= (dr["MenuItemText"]).ToString(),
                                       Quantity=(dr["totalCount"]).ToString()
                                   }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            finally
            {
                con.Close();
            }

            return objSaleCategory;

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
