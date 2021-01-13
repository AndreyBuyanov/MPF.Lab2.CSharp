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
    public class HomeController : Controller
    {
        private Urls urlsModel;
        
        public HomeController(MySqlDatabase mySqlDatabase)
        {
            urlsModel = new Urls(mySqlDatabase);
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await this.urlsModel.GetUrls());
        }
        
        public IActionResult Go(string url)
        {
            var plainTextBytes = System.Convert.FromBase64String(url);
            var decodedUrl = Encoding.UTF8.GetString(plainTextBytes);
            urlsModel.IncrementUrl(decodedUrl);
            return Redirect(decodedUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}