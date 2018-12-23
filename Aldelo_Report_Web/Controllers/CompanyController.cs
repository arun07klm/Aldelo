using DatabaseInterface;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aldelo_Report_Web.Controllers
{
    public class CompanyController : Controller
    {
        CompanyDal companyDal;
        public CompanyController()
        {
            companyDal = new CompanyDal();
        }
        // GET: Company
        public ActionResult GetAllCompany()
        {
            var companyDtoList = companyDal.GetAllCompanyDal();
            return GetJson(companyDtoList);
        }
        public static ContentResult GetJson(dynamic obj)
        {
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            dynamic resultJson = JsonConvert.SerializeObject(obj, Formatting.None, settings);

            var jsonResult = new ContentResult
            {
                Content = resultJson,
                ContentType = "application/json"
            };
            return jsonResult;
        }
    }
}