using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

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
      double sl = (double)_db.Students.ToList().Select(s =>
     {
       return _db.StudentCourses.Where(x => x.StudentId == s.StudentId).ToList().Average(m => (double)m.Grade);
     }).ToList().Average();

      ViewBag.GPA = sl;
      return View(_db.Students.ToList());
    }

    public ActionResult Details(int id)
    {
      List<StudentCourse> scs = _db.StudentCourses.Where(x => x.StudentCourseId == id).ToList();
      ViewBag.GPA = scs.Average(x => (int)x.Grade);
      return View(_db.Students.Include(x => x.Courses).ThenInclude(x => x.Course).FirstOrDefault(x => x.StudentId == id));
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
      _db.StudentCourses.Add(new StudentCourse { CourseId = courseid, StudentId = studentid });
      _db.SaveChanges();
      return RedirectToAction("Enroll");
    }

    public ActionResult DeclareMajor()
    {
      ViewBag.StudentId = new SelectList(_db.Students, "StudentId", "Name");
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
      return View();
    }

    public ActionResult SelectDepartment(int id)
    {
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
      return View();
    }

    [HttpPost, ActionName("SelectDepartment")]
    public ActionResult SelectDepartmentPost(int id, int departmentid)
    {
      return RedirectToAction("SelectCourse", new { id = id, departmentid = departmentid });
    }

    public ActionResult SelectCourse(int id, int departmentid)
    {
      Department department = _db.Departments.Include(d => d.Courses).FirstOrDefault(d => d.DepartmentId == departmentid);

      ViewBag.CourseId = new SelectList(department.Courses, "CourseId", "CourseName");
      return View();
    }

    [HttpPost, ActionName("SelectCourse")]
    public ActionResult SelectCoursePost(int id, int courseid)
    {
      _db.StudentCourses.Add(new StudentCourse { CourseId = courseid, StudentId = id });
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = id });
    }


    [HttpPost]
    public ActionResult DeclareMajor(int studentid, int departmentid)
    {
      Student student = _db.Students.FirstOrDefault(x => x.StudentId == studentid);
      student.DepartmentId = departmentid;
      _db.SaveChanges();
      return RedirectToAction("DeclareMajor");
    }
  }
}