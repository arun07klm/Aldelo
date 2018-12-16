using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Aldelo_Report_Web.customModel;
using Newtonsoft.Json.Linq;

namespace Aldelo_Report_Web.Api_Controller
{
    public class SaleController : ApiController
    {
        private object crystalReportViewer1;
        [System.Web.Http.HttpGet]
        public List<customer> Getcustomer()
        {
            /* SELECT        OrderTransactions.OrderTransactionID
FROM            ((OrderTransactions LEFT OUTER JOIN
                         MenuItems ON MenuItems.MenuItemID = OrderTransactions.MenuItemID ) )
                       
WHERE        (MenuItems.MenuCategoryID=2  ) orderheaders*/



            List<customer> customerList = new List<customer>();
            try  //
            {

                string str = @"""#,##0.000""";
                string str1 = @"""#,##0.200""";
                DataSet ds = new DataSet();
                //OleDbConnection con =new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\\Project\\Restaurant_Application\\Calicut Live.mdb");
                string strConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
                OleDbConnection con = new OleDbConnection(strConnectionString);
                con.Open();
                string query = @"SELECT        OrderHeaders.OrderDateTime, CustomerFiles.AccountCode AS EmployeeID, CustomerFiles.CustomerName, CustomerFiles.PhoneNumber AS CPR, CustomerFiles.ZipCode AS SiteID, 
                         OrderTransactions.MenuItemID, MenuItems.MenuItemText AS ITEM,
                         iif(OrderTransactions.MenuItemID=104 OR OrderTransactions.MenuItemID=103 , Format (0.200," + str + @"), Format (OrderTransactions.MenuItemUnitPrice ," + str + @") )AS ItemPrice ,                   
OrderTransactions.Quantity AS ITEMQUANTITY,  iif(OrderTransactions.MenuItemID=104 OR OrderTransactions.MenuItemID=103 , Format (0.200*OrderTransactions.quantity," + str + @"), Format (OrderTransactions.ExtendedPrice ," + str + @")) AS TOTALSALE,
 switch (
            MenuItems.MenuItemDiscountable=true,  iif(OrderTransactions.MenuItemID=104 OR OrderTransactions.MenuItemID=103 , Format (0.000," + str + @"), Format (OrderTransactions.ExtendedPrice/2 ," + str + @") ),
              MenuItems.MenuItemDiscountable=false,  Format (OrderTransactions.MenuItemUnitPrice ," + str + @")  )AS FROMCUST,

switch (
            MenuItems.MenuItemDiscountable=true,  iif(OrderTransactions.MenuItemID=104 OR OrderTransactions.MenuItemID=103 , Format (0.200*OrderTransactions.quantity," + str + @"), Format (OrderTransactions.ExtendedPrice/2 ," + str + @") ),
            MenuItems.MenuItemDiscountable=false,  Format (0.000 ," + str + @")  )AS FROMSUBSIDY 

 

FROM            (((OrderHeaders LEFT OUTER JOIN
                         CustomerFiles ON CustomerFiles.CustomerID = OrderHeaders.CustomerID) LEFT OUTER JOIN
                         OrderTransactions ON OrderTransactions.OrderID = OrderHeaders.OrderID) LEFT OUTER JOIN
                         MenuItems ON MenuItems.MenuItemID = OrderTransactions.MenuItemID )
WHERE        (OrderHeaders.OrderDateTime BETWEEN #" + "01/Apr/2018" + @"#  AND #" + "01/Jul/2018" + @" 23:59:00 #  AND OrderHeaders.CustomerId  Is Not Null  )";

                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                DataTable dt = ds.Tables[0];


                customerList = (from DataRow dr in dt.Rows
                                select new customer()
                                {
                                    OrderDateTime = (dr["OrderDateTime"]).ToString(),
                                    EmployeeID = (dr["EmployeeID"]).ToString(),
                                    CustomerName = (dr["CustomerName"]).ToString(),
                                    CPR = (dr["CPR"]).ToString(),
                                    MenuItemID = (dr["MenuItemID"]).ToString(),
                                    ITEM = (dr["ITEM"]).ToString(),
                                    ItemPrice = (dr["ItemPrice"]).ToString(),
                                    TOTALSALE = (dr["TOTALSALE"]).ToString(),
                                    FROMCUST = (dr["FROMCUST"]).ToString(),
                                    FROMSUBSIDY = (dr["FROMSUBSIDY"]).ToString()

                                }).ToList();


                //Customer rpt = new Customer();
                ////  rpt.SetParameterValue("@txtData","From  " + dtpFrom.Text + "  To  " + dtpTo.Text + "");
                //rpt.SetDataSource(dt);
                //rpt.SetParameterValue("Date", "From   " + dtpFrom.Text + "   To   " + dtpTo.Text);
                //crystalReportViewer1.ReportSource = rpt;

                //crystalReportViewer1.Refresh();

            }
            catch (Exception ex)
            {

            }

            var list = customerList.GroupBy(a => a.EmployeeID).ToList();
            return   customerList;
        }
    }
}
