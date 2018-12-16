using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aldelo_Report_Web.customModel
{
    public class customer
    {

        public string OrderDateTime { set; get; }
        public string EmployeeID { set; get; }
        public string CustomerName { set; get; }
        public string CPR { set; get; }

        public string SiteID { set; get; }
        public string MenuItemID { set; get; }
        public string ITEM { set; get; }
        public string ItemPrice { set; get; }
        public string ITEMQUANTITY { set; get; }
       public string TOTALSALE { set; get; }

        public string FROMCUST { set; get; }
        public string FROMSUBSIDY { set; get; }
    }

  //  List<carddata> crdobj = ds.Tables[0].AsEnumerable().Where(dr => dr.Table.Rows.Count != 0).Select(dr => new carddata { ip = dr.Table.Rows[0].ToString(), host = dr.Table.Rows[1].ToString(), data = dr.Table.Rows[2].ToString() }).ToList();
}