using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aldelo_Report_Web.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public List<DataRow> Getcustomer()
        {
            List<DataRow> list = new List<DataRow>();
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
WHERE        (OrderHeaders.OrderDateTime BETWEEN #" + "01/Apr/2018" + @"#  AND #" + "01/Jul/2018" + @" 23:59:00 # AND OrderHeaders.CustomerId  Is Not Null  )";

                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                //List<customer> lstCust =new  List<customer>();

                //IEnumerable<object> lst = dt.AsEnumerable();


                list = dt.AsEnumerable().ToList();

                //foreach(var t in list)
                //{
                //    lstCust.AddRange(t);
                //}


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
            return list;
        }
    }
}