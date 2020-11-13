using Microsoft.EntityFrameworkCore;
using UniversityRegistrar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System;

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
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {

      Department department = _db.Departments.Include(x => x.Students).ThenInclude(s => s.Courses).Include(x => x.Courses).FirstOrDefault(x => x.DepartmentId == id);
      if (department.Students.Count > 0)
      {
        List<Student> sid = department.Students.Where(s => s.Courses.Count > 0).ToList();
        if (sid.Count > 0)
        {
          double davg = (double)sid.Select(s =>
          {
            return _db.StudentCourses.Where(x => x.StudentId == s.StudentId).ToList().Average(m => (double)m.Grade);
          }).ToList().Average();
          ViewBag.GPA = davg;
        }
        else
        {
          ViewBag.GPA = "There are no students in this department with a GPA record.";
        }
      }
      else
      {
        ViewBag.GPA = "There are no student in this department";
      }
      return View(department);
    }

    public ActionResult Index()
    {
      return View(_db.Departments.ToList());
    }
  }
}