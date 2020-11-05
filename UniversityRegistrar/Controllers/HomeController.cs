using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;

namespace UniversityRegistrar.Controllers
{
    public class HomeController : Controller
    {
        public UniversityRegistrarContext _db;
        public HomeController(UniversityRegistrarContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}