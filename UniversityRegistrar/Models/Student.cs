using System.Collections.Generic;
using System;

namespace UniversityRegistrar.Models
{
  public class Student
  {
    public int StudentId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfEnrollment { get; set; }

    public virtual ICollection<StudentCourse> Courses { get; set; }
    public int DepartmentId { get; set; } = 1;
    public virtual Department Department { get; set; }
    public Student()
    {
      this.Courses = new HashSet<StudentCourse>();      
      this.DateOfEnrollment = DateTime.Now;
    }
  }
}