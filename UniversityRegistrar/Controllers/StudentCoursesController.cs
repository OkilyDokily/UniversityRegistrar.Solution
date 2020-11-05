using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace UniversityRegistrar.Controllers
{
    public class StudentCoursesController : Controller
    {
        public UniversityRegistrarContext _db;
        public StudentCoursesController(UniversityRegistrarContext db)
        {
            _db = db;
        }

        public ActionResult Details(int id)
        {
            return View(_db.StudentCourses.FirstOrDefault(x => x.StudentCourseId == id));
        }

        public ActionResult Edit(int id)
        {

        }       
    }
}