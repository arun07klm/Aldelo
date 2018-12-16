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
    public class RefundAmountDetailsController : ApiController
    {
        [HttpPost]
        public List<MenuItem> GetTotalRefundAmount(DataTable dtBody)
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
                if (dtBody.Rows[0]["fromDate"] == DBNull.Value || dtBody.Rows[0]["toDate"] == DBNull.Value)
                {
                   query = @" SELECT RefundDateTime, SUM(AmountRefunded) AS amount
                            FROM     OrderRefunds
                            GROUP BY RefundDateTime";
                }
                else
                {
                    query = @" SELECT  sum(AmountRefunded) as Amount
                  FROM     OrderRefunds
                    where format(RefundDateTime,'dd-mm-yyyy') between" + dtBody.Rows[0]["fromDate"].ToString()+"@ and "+ dtBody.Rows[0]["toDate"].ToString();
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
