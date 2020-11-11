using Microsoft.EntityFrameworkCore;
using UniversityRegistrar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace UniversityRegistrar.Controllers
{
  public class DepartmentsController : Controller
  {
    UniversityRegistrarContext _db;
    public DepartmentsController(UniversityRegistrarContext db)
    {
      _db = db;
    }
    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Department department)
    {
      _db.Departments.Add(department);
      _db.SaveChanges();
      return View();
    }

    public ActionResult Details(int id)
    {

      Department department = _db.Departments.Include(x => x.Students).Include(x => x.Courses).FirstOrDefault(x => x.DepartmentId == id);

      double davg = (double)department.Students.Select(s =>
     {
       return _db.StudentCourses.Where(x => x.StudentId == s.StudentId).ToList().DefaultIfEmpty().Average(m => (double)m.Grade);
     }).ToList().DefaultIfEmpty().Average();

      ViewBag.GPA = davg;
      return View(department);
    }

    public ActionResult Index()
    {
      return View(_db.Departments.ToList());
    }
  }
}