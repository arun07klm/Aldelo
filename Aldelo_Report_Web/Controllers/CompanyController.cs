using Aldelo.DatabaseInterface.BOL;
using DatabaseInterface;
using DatabaseInterface.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
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
        //POST: Company
        public ActionResult SaveCompany(CompanyDto companyDto)
        {
            companyDto.CreatedOn = DateTime.Now;
            companyDto.Status = (byte)AldeloEnums.RecordStatus.ACTIVE;
            var sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/"+companyDto.Username);
            bool exists = System.IO.Directory.Exists(sPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(sPath);
            companyDto.DBFolderPath = companyDto.Username;
            var isCompanySavedSuccessfully = companyDal.SaveCompanyDal(companyDto);
            return GetJson(isCompanySavedSuccessfully);
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