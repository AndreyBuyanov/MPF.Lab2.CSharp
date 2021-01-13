using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Models;
using MySqlConnector;

using dto = App.Models;

namespace App.Controllers
{
    public class AdminController : Controller
    {
        private string sessionFile = "admin.lock";
        private Urls urlsModel;

        private bool IsAdminLogged()
        {
            return System.IO.File.Exists(sessionFile);
        }
        
        public AdminController(MySqlDatabase mySqlDatabase)
        {
            urlsModel = new Urls(mySqlDatabase);
        }
        
        public async Task<IActionResult> Index()
        {
            if (IsAdminLogged())
            {
                return View(await this.urlsModel.GetUrls());
            }
            else
            {
                return Redirect("/admin/login");
            }
        }

        [HttpPost]
        public IActionResult AddUrl(string url)
        {
            if (IsAdminLogged())
            {
                urlsModel.AddUrl(url);
                return Redirect("/admin");
            }
            else
            {
                return Redirect("/admin/login");
            }
        }
        
        public IActionResult RemoveUrlById(int id)
        {
            if (IsAdminLogged())
            {
                urlsModel.RemoveUrlById(id);
                return Redirect("/admin");
            }
            else
            {
                return Redirect("/admin/login");
            }
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            if (IsAdminLogged())
            {
                return Redirect("/admin");
            }
            else
            {
                return View();
            }
        }
        
        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            if (!IsAdminLogged())
            {
                if (login == "admin" && password == "admin")
                {
                    using System.IO.StreamWriter w = System.IO.File.AppendText(sessionFile);
                }
                else
                {
                    return Redirect("/admin/login");
                }
            }
            return Redirect("/admin");
        }
        
        public IActionResult Logout()
        {
            System.IO.File.Delete(sessionFile);
            return Redirect("/admin/login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}