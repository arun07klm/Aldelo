using Aldelo.DatabaseInterface.BOL;
using DatabaseInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Aldelo_Report_Web.Controllers
{
    public class LoginController : Controller
    {
        CompanyDal companyDal;
        public LoginController()
        {
            companyDal = new CompanyDal();
        }
        // GET: Login
        public ActionResult Login()
        {
            var companyDto = new CompanyDto();
            return View(companyDto);
        }
        [HttpPost]
        public ActionResult Login(CompanyDto companyDto)
        {
            var adminUsername = WebConfigurationManager.AppSettings["AdminUsername"];
            var adminPassword = WebConfigurationManager.AppSettings["Password"];
            if (companyDto.Username == adminUsername && companyDto.Password == adminPassword)
            {
                Session["LoggedInUser"] = companyDto.Username;
                return RedirectToAction("AdminIndex", "Home");
            }
            var validCompanyDto = companyDal.IsValidCompany(companyDto.Username, companyDto.Password);
            if (validCompanyDto != null)
            {
                Session["LoggedInUser"] = validCompanyDto.Username;
                return companyDto.PasswordExpireOn <= DateTime.Now
                      ? RedirectToAction("ValidityExpired", "Home")
                      : RedirectToAction("Index", "Home");
            }
            TempData["ErrorMessage"] = "Please check your login credentials.";
            return View();
        }

    }
}