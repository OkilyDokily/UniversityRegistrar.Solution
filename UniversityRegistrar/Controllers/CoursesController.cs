using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace UniversityRegistrar.Controllers
{
  public class CoursesController : Controller
  {
    UniversityRegistrarContext _db;

    public CoursesController(UniversityRegistrarContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Courses.ToList());
    }

    public ActionResult Details(int id)
    {
      ViewBag.StudentCourses = _db.StudentCourses.Where(x => x.CourseId == id).Include(x => x.Student).ToList();
      return View(_db.Courses.Include(c => c.Students).ThenInclude(x => x.Student).FirstOrDefault(x => x.CourseId == id));
    }

    public ActionResult Create()
    {
      ViewBag.DepartmentId = new SelectList(_db.Departments.ToList(), "DepartmentId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Course course)
    {
      _db.Courses.Add(course);
      _db.SaveChanges();
      return RedirectToAction("Create");
    }
  }
}