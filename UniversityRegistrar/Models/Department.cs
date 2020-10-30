using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UniversityRegistrar.Models
{
  public class Department
  {
    public int DepartmentId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<StudentDepartment> Students { get; set; }
    public virtual ICollection<Course> Courses { get; set; }
    public Department()
    {
      this.Students = new HashSet<StudentDepartment>();
      this.Courses = new HashSet<Course>();
    }
  }
}