using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;

namespace UniversityRegistrar.Controllers
{
    public class StudentsController : Controller
    {
        UniversityRegistrarContext _db;

        public StudentsController(UniversityRegistrarContext db)
        {
            _db = db;
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student student)
        {
            _db.Students.Add(student);
            _db.SaveChanges();
            return View();
        }
    }
}