using Aldelo.DatabaseInterface.BOL;
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
            var adminPassword= WebConfigurationManager.AppSettings["Password"];
            if (companyDto.Username == adminUsername && companyDto.Password == adminPassword)
            {
                Session["LoggedInUser"]= companyDto.Username;
                return RedirectToAction("AdminIndex", "Home");
            }
            return View();
        }
    }
}