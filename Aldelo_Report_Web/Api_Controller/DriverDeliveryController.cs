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
    public class DriverDeliveryController : ApiController
    {
        [HttpGet]
        public List<Employee> getInitialData()
        {
            List<Employee> objSaleCategory = new List<Employee>();
            DataSet ds = new DataSet();
            //OleDbConnection con =new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\\Project\\Restaurant_Application\\Calicut Live.mdb");
            string strConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            OleDbConnection con = new OleDbConnection(strConnectionString);
            con.Open();
            try
            {
                string query = @" SELECT EmployeeID, FirstName + LastName AS Name
                                    FROM     EmployeeFiles
                                WHERE  (EmployeeInActive = True) or (EmployeeIsDriver = True)";
                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                objSaleCategory = (from DataRow dr in dt.Rows
                                   select new Employee()
                                   {
                                       EmployeeName = (dr["Name"]).ToString(),
                                       EmployeeId = (dr["EmployeeId"]).ToString()
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

        [HttpPost]
        public List<Employee> getTotalDeliveryCharge(DataTable dtBody)
        {
            List<Employee> objSaleCategory = new List<Employee>();
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
                     query = @" SELECT COUNT(*) AS TotalCount, OrderHeaders.EmployeeID  AS EmployeeId, EmployeeFiles.FirstName AS firstName, EmployeeFiles.LastName AS lastName
FROM     (OrderHeaders INNER JOIN
                  EmployeeFiles ON OrderHeaders.EmployeeID = EmployeeFiles.EmployeeID OR OrderHeaders.DriverEmployeeID = EmployeeFiles.EmployeeID)
GROUP BY OrderHeaders.EmployeeID, OrderHeaders.DriverEmployeeID, EmployeeFiles.FirstName, EmployeeFiles.LastName";
                }
                else if(dtBody.Rows[0]["employeeId"] != DBNull.Value && (dtBody.Rows[0]["fromDate"] == DBNull.Value || dtBody.Rows[0]["fromDate"] == DBNull.Value))
                {
                    query = @" SELECT COUNT(*) AS TotalCount, OrderHeaders.EmployeeID  AS EmployeeId, EmployeeFiles.FirstName AS firstName, EmployeeFiles.LastName AS lastName
FROM     (OrderHeaders INNER JOIN
                  EmployeeFiles ON OrderHeaders.EmployeeID = EmployeeFiles.EmployeeID OR OrderHeaders.DriverEmployeeID = EmployeeFiles.EmployeeID)
where OrderHeaders.EmployeeID=" + dtBody.Rows[0]["employeeId"]+ @" or OrderHeaders.DriverEmployeeID="+ dtBody.Rows[0]["employeeId"] +@"
GROUP BY OrderHeaders.EmployeeID, OrderHeaders.DriverEmployeeID, EmployeeFiles.FirstName, EmployeeFiles.LastName";
                }

                else if (dtBody.Rows[0]["employeeId"] != DBNull.Value && dtBody.Rows[0]["fromDate"] != DBNull.Value && dtBody.Rows[0]["fromDate"] != DBNull.Value)
                {
                    query = @" SELECT COUNT(*) AS TotalCount, OrderHeaders.EmployeeID  AS EmployeeId, EmployeeFiles.FirstName AS firstName, EmployeeFiles.LastName AS lastName
FROM     (OrderHeaders INNER JOIN
                  EmployeeFiles ON OrderHeaders.EmployeeID = EmployeeFiles.EmployeeID OR OrderHeaders.DriverEmployeeID = EmployeeFiles.EmployeeID)
where OrderHeaders.EmployeeID=" + dtBody.Rows[0]["employeeId"] + @" or OrderHeaders.DriverEmployeeID=" + dtBody.Rows[0]["employeeId"] + @"
and (FORMAT(OrderDateTime, 'dd-mm-yyyy') BETWEEN " + dtBody.Rows[0]["FromDate"].ToString() + @"  AND " + dtBody.Rows[0]["Todate"].ToString() + @")
GROUP BY OrderHeaders.EmployeeID, OrderHeaders.DriverEmployeeID, EmployeeFiles.FirstName, EmployeeFiles.LastName";
                }
                else
                {
                    query = @" SELECT COUNT(*) AS TotalCount, OrderHeaders.EmployeeID  AS EmployeeId, EmployeeFiles.FirstName AS firstName, EmployeeFiles.LastName AS lastName
FROM     (OrderHeaders INNER JOIN
                  EmployeeFiles ON OrderHeaders.EmployeeID = EmployeeFiles.EmployeeID OR OrderHeaders.DriverEmployeeID = EmployeeFiles.EmployeeID)
where (FORMAT(OrderDateTime, 'dd-mm-yyyy') BETWEEN " + dtBody.Rows[0]["FromDate"].ToString() + @"  AND " + dtBody.Rows[0]["Todate"].ToString() + @")
GROUP BY OrderHeaders.EmployeeID, OrderHeaders.DriverEmployeeID, EmployeeFiles.FirstName, EmployeeFiles.LastName";
                }
                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                objSaleCategory = (from DataRow dr in dt.Rows
                                   select new Employee()
                                   {
                                       EmployeeName = (dr["firstName"]).ToString()+" "+ (dr["lastName"]).ToString(),
                                       EmployeeId = (dr["EmployeeId"]).ToString(),
                                       TotalDeliveryCharge=(dr["TotalCount"]).ToString()
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
        public class Employee
        {
            public string EmployeeName { set; get; }
            public string EmployeeId { set; get; }
            public string TypeOfEmployee { set; get; }
            public string TotalDeliveryCharge { set; get; }
            public string IsActive { set; get; }
            

        }
    }
}
