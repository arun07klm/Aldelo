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
    public class TotalDeliveryChargeController : ApiController
    {
        [HttpPost]
        public List<MenuItem> GetTotalDeliveryCharge(DataTable dtBody)
        {

            List<MenuItem> objSaleCategory = new List<MenuItem>();
            DataSet ds = new DataSet();
            //OleDbConnection con =new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\\Project\\Restaurant_Application\\Calicut Live.mdb");
            string strConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            OleDbConnection con = new OleDbConnection(strConnectionString);
            con.Open();
            try
            {
                string query = string.Empty;
                if (dtBody.Rows[0]["employeeId"] == DBNull.Value && (dtBody.Rows[0]["fromDate"] == DBNull.Value || dtBody.Rows[0]["fromDate"] == DBNull.Value))
                {
                   query = @" SELECT SUM(OrderHeaders.DeliveryCharge) AS Amount, EmployeeFiles.FirstName + EmployeeFiles.LastName AS Name
                                FROM     (OrderHeaders INNER JOIN
                  EmployeeFiles ON OrderHeaders.EmployeeID = EmployeeFiles.EmployeeID AND OrderHeaders.DriverEmployeeID = EmployeeFiles.EmployeeID)
                    GROUP BY OrderHeaders.EmployeeID, EmployeeFiles.FirstName, EmployeeFiles.LastName";
                }
                else if(dtBody.Rows[0]["employeeId"] != DBNull.Value && (dtBody.Rows[0]["fromDate"] == DBNull.Value || dtBody.Rows[0]["fromDate"] == DBNull.Value))
                {
                    query = @" SELECT SUM(OrderHeaders.DeliveryCharge) AS Amount, EmployeeFiles.FirstName + EmployeeFiles.LastName AS Name
                                FROM     (OrderHeaders INNER JOIN
                  EmployeeFiles ON OrderHeaders.EmployeeID = EmployeeFiles.EmployeeID AND OrderHeaders.DriverEmployeeID = EmployeeFiles.EmployeeID)
                where  OrderHeaders.EmployeeID=" + dtBody.Rows[0]["employeeId"] + @" or OrderHeaders.DriverEmployeeID=" + dtBody.Rows[0]["employeeId"] + @"
                    GROUP BY OrderHeaders.EmployeeID, EmployeeFiles.FirstName, EmployeeFiles.LastName";
                }
                else if(dtBody.Rows[0]["employeeId"] == DBNull.Value && (dtBody.Rows[0]["fromDate"] != DBNull.Value && dtBody.Rows[0]["fromDate"] != DBNull.Value))
                {
                    query = @" SELECT SUM(OrderHeaders.DeliveryCharge) AS Amount, EmployeeFiles.FirstName + EmployeeFiles.LastName AS Name
                                FROM     (OrderHeaders INNER JOIN
                  EmployeeFiles ON OrderHeaders.EmployeeID = EmployeeFiles.EmployeeID AND OrderHeaders.DriverEmployeeID = EmployeeFiles.EmployeeID)
                    where (FORMAT(OrderDateTime, 'dd-mm-yyyy') BETWEEN " + dtBody.Rows[0]["FromDate"].ToString() + @"  AND " + dtBody.Rows[0]["Todate"].ToString() + @")
                    GROUP BY OrderHeaders.EmployeeID, EmployeeFiles.FirstName, EmployeeFiles.LastName";

                }
                else
                {
                    query = @" SELECT SUM(OrderHeaders.DeliveryCharge) AS Amount, EmployeeFiles.FirstName + EmployeeFiles.LastName AS Name
                                FROM     (OrderHeaders INNER JOIN
                  EmployeeFiles ON OrderHeaders.EmployeeID = EmployeeFiles.EmployeeID AND OrderHeaders.DriverEmployeeID = EmployeeFiles.EmployeeID)
                    where  OrderHeaders.EmployeeID=" + dtBody.Rows[0]["employeeId"] + @" or OrderHeaders.DriverEmployeeID=" + dtBody.Rows[0]["employeeId"] + @"
                    and (FORMAT(OrderDateTime, 'dd-mm-yyyy') BETWEEN " + dtBody.Rows[0]["FromDate"].ToString() + @"  AND " + dtBody.Rows[0]["Todate"].ToString() + @")
                    GROUP BY OrderHeaders.EmployeeID, EmployeeFiles.FirstName, EmployeeFiles.LastName";
                }
                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                objSaleCategory = (from DataRow dr in dt.Rows
                                   select new MenuItem()
                                   {
                                       Name = (dr["Name"]).ToString(),
                                       Amount = (dr["Amount"]).ToString()
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

        [HttpGet]
        public List<MenuItem> GetInitialData()
        {

            List<MenuItem> objSaleCategory = new List<MenuItem>();
            DataSet ds = new DataSet();
            //OleDbConnection con =new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\\Project\\Restaurant_Application\\Calicut Live.mdb");
            string strConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            OleDbConnection con = new OleDbConnection(strConnectionString);
            con.Open();
            try
            {
                string query = @" select EmployeeFiles.FirstName + EmployeeFiles.LastName AS Name, EmployeeID
                                FROM EmployeeFiles";
                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                objSaleCategory = (from DataRow dr in dt.Rows
                                   select new MenuItem()
                                   {
                                       Name = (dr["Name"]).ToString(),
                                       Id = (dr["EmployeeID"]).ToString()
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

