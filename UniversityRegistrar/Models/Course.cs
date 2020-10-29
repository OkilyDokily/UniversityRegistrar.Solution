using System.Collections.Generic;

namespace UniversityRegistrar.Models
{
  public class Course
  {
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public string CourseNumber { get; set; }
    public virtual ICollection<StudentCourse> Students { get; set; }
    public virtual ICollection<CourseDepartment> CourseDepartments { get; set; }

    public Course()
    {
      this.CourseDepartments = new HashSet<CourseDepartment>();
      this.Students = new HashSet<StudentCourse>();
    }
  }
}