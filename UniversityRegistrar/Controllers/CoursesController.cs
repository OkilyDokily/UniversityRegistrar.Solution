using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;

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
      Course course = _db.Courses.Include(c => c.Students).ThenInclude(s => s.Student).Include(c => c.Department).FirstOrDefault(c => c.CourseId == id);
      List<StudentWithGPAAndSC> studentsincourse = course.Students.Select(sc => new StudentWithGPAAndSC { StudentCourse = sc, StudentWithGPA = new StudentWithGPA { Student = sc.Student, GPA = (double)sc.Grade } }).OrderBy(sic => sic.StudentWithGPA.GPA).ToList();

      ViewBag.GPA = studentsincourse.Average(s => s.StudentWithGPA.GPA);
      ViewBag.Department = course.Department;
      return View(studentsincourse);
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
      return RedirectToAction("Index");
    }
  }
}