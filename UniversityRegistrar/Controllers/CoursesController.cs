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
      Course course = _db.Courses.Include(c => c.Students).ThenInclude(sc => sc.Student).FirstOrDefault(c => c.CourseId == id)
      List<Student> studentsincourse = course.Students.Select(sc => new StudentWithGPA { Student = sc.Student, }).ToList();
      List<Student> studentsthatmatchcourse = students.Where()
      List<StudentWithGPA> studentswithgpa = students.Select(s => new StudentWithGPA { Student = s, GPA = s.Courses.Select(x => x.Grade).Average(x => (double)x) }).ToList();
      List<StudentWithGPA> studentswithgpaordered = studentswithgpa.Where(x => x.GPA >= (double)grades && x.GPA < (((double)grades) + 1)).OrderBy(x => x.GPA).ToList();
      //   
      //   double d = (double)course.Students.Select(s =>
      //  {
      //    return _db.StudentCourses.Where(x => x.StudentId == s.StudentId).ToList().Average(m => (double)m.Grade);
      //  }).ToList().Average();

      //   ViewBag.GPA = d;

      //   ViewBag.StudentCourses = _db.StudentCourses.Where(x => x.CourseId == id).Include(x => x.Student).ToList();
      //   return View(course);
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