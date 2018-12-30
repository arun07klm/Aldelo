using Aldelo.DatabaseInterface.BOL;
using Aldelo.DatabaseInterface.DAL;
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
        MenuDal menuDal;
        CompanyMenuListDal companyMenuListDal;
        public CompanyController()
        {
            companyDal = new CompanyDal();
            menuDal = new MenuDal();
            companyMenuListDal = new CompanyMenuListDal();
        }
        // GET: Company
        public ActionResult GetAllCompany()
        {
            var companyDtoList = companyDal.GetAllCompanyDal();
            return GetJson(companyDtoList);
        }
        //GET: Company By Id
        public ActionResult GetCompanyById(int id)
        {
            return GetJson(companyDal.GetCompanyByIdDal(id));
        }
        //POST: Company
        public ActionResult SaveCompany(CompanyDto companyDto)
        {
            companyDto.CreatedOn = DateTime.Now;
            companyDto.Status = (byte)AldeloEnums.RecordStatus.ACTIVE;
            var sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/AccessDB/" + companyDto.Username);
            bool exists = System.IO.Directory.Exists(sPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(sPath);
            companyDto.DBFolderPath = companyDto.Username;
            var isCompanySavedSuccessfully = companyDal.SaveCompanyDal(companyDto);
            return GetJson(isCompanySavedSuccessfully);
        }
        //POST: Company
        public ActionResult EditCompany(CompanyDto companyDto)
        {
            companyDto.Status = (byte)AldeloEnums.RecordStatus.ACTIVE;
            var sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/AccessDB/" + companyDto.Username);
            bool exists = System.IO.Directory.Exists(sPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(sPath);
            companyDto.DBFolderPath = companyDto.Username;
            bool isCompanySavedSuccessfully = false;
            if (companyMenuListDal.RemoveAllCompanyMenu(companyDto.CompanyId))
            {
                isCompanySavedSuccessfully = companyDal.EditCompanyDal(companyDto);
            }
           
            return GetJson(isCompanySavedSuccessfully);
        }
        public ActionResult GetAllMenu()
        {
            return GetJson(menuDal.GetAllMenuDal());
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