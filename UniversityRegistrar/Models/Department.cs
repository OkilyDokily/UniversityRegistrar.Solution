using System.Collections.Generic;

namespace UniversityRegistrar.Models
{
  public class Department
  {
    public int DepartmentId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Student> Students { get; set; }
    public virtual ICollection<Course> Courses { get; set; }
    public Department()
    {
      this.Students = new HashSet<Student>();
      this.Courses = new HashSet<Course>();
    }
  }
}