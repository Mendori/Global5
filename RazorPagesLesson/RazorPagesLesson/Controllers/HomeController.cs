using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RazorPagesLesson.Controllers
{
    public class HomeController : Controller
    {
        string InfoMessage { get; set; } = "Witaj na stronie startowej!";
        public IActionResult Index()
        {
            InfoMessage += $" Aktualny czass to: {DateTime.Now}";
            ViewBag.Message = InfoMessage;
            return View();
        }
    }
}