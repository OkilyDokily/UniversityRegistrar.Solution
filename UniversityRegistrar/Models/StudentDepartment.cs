namespace UniversityRegistrar.Models
{
  public class StudentDepartment
  {
    public int StudentDepartmentId { get; set; }
    public int StudentId { get; set; }
    public int DepartmentId { get; set; }

    public Course Course { get; set; }
    public Department Department { get; set; }
  }
}