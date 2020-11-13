using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System;


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
      return View(_db.StudentCourses.Include(x => x.Student).Include(x => x.Course).FirstOrDefault(x => x.StudentCourseId == id));
    }

    public ActionResult Edit(int id)
    {
      ViewBag.Grade = new SelectList(Enum.GetValues(typeof(Grade)));
      return View(_db.StudentCourses.Include(x => x.Student).Include(x => x.Course).FirstOrDefault(x => x.StudentCourseId == id));
    }

    [HttpPost]
    public ActionResult Edit(StudentCourse sc)
    {
      _db.Entry(sc).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", "Courses", new { id = sc.CourseId });
    }
  }
}