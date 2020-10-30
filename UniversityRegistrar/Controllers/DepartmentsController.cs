using Microsoft.EntityFrameworkCore;
using UniversityRegistrar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    public ActionResult DeclareMajor()
    {
      return View();
    }

    [HttpPost]
    public ActionResult DeclareMajor(int studentid, int departmentid)
    {
      ViewBag.StudentId = new SelectList(_db.Students, "StudentId", "Name");
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
      _db.StudentDepartments.Add(new StudentDepartment { StudentId = studentid, DepartmentId = departmentid });
      _db.SaveChanges();
      return RedirectToAction("DeclareMajor");
    }
  }
}