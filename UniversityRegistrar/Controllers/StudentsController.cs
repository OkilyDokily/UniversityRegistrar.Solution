using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace UniversityRegistrar.Controllers
{
  public class StudentsController : Controller
  {
    UniversityRegistrarContext _db;

    public StudentsController(UniversityRegistrarContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Students.ToList());
    }

    public ActionResult Details(int id)
    {
      return View(_db.Students.Include(x =>x.Courses).ThenInclude(x => x.Course).FirstOrDefault(x => x.StudentId == id));
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

    public ActionResult Enroll()
    {
      ViewBag.StudentId = new SelectList(_db.Students, "StudentId", "Name");
      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "CourseName");
      return View();
    }

    [HttpPost]
    public ActionResult Enroll(int studentid, int courseid)
    {
      
      Console.WriteLine(courseid);
      _db.StudentCourses.Add(new StudentCourse { CourseId = courseid, StudentId = studentid });
      _db.SaveChanges();
      return RedirectToAction("Enroll");
    }
  }
}