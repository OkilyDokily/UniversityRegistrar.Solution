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
      List<Student> students = _db.Students.Where(s => s.Courses.Count > 0).Include(x => x.Courses).ToList();
      List<StudentWithGPA> studentswithgpa = students.Select(s => new StudentWithGPA { Student = s, GPA = s.Courses.Select(x => x.Grade).Average(x => (double)x) }).ToList();
      List<StudentWithGPA> studentswithgpaordered = studentswithgpa.OrderBy(x => x.GPA).ToList();

      ViewBag.GPA = studentswithgpaordered.Select(x => x.GPA).Average();
      return View(studentswithgpaordered);
    }

    public ActionResult Details(int id)
    {
      List<StudentCourse> scs = _db.StudentCourses.Where(x => x.StudentCourseId == id).ToList();
      ViewBag.GPA = (scs.Count > 0) ? scs.Average(x => (int)x.Grade).ToString() : "No courses to determine GPA";
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

    public ActionResult SortByGrade()
    {
      ViewBag.Grades = new SelectList(Enum.GetValues(typeof(Grade)));
      return View();
    }

    [HttpPost]
    public ActionResult SortByGrade(Grade grades)
    {
      List<Student> students = _db.Students.Where(s => s.Courses.Count > 0).Include(x => x.Courses).ToList();
      List<StudentWithGPA> studentswithgpa = students.Select(s => new StudentWithGPA { Student = s, GPA = s.Courses.Select(x => x.Grade).Average(x => (double)x) }).ToList();
      List<StudentWithGPA> studentswithgpaordered = studentswithgpa.Where(x => x.GPA >= (double)grades && x.GPA < (((double)grades) + 1)).OrderBy(x => x.GPA).ToList();

      return View("SortByGradeResults", studentswithgpaordered);
    }
  }
}