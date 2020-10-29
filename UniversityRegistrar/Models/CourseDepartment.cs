namespace UniversityRegistrar.Models
{
  public class CourseDepartment
  {
    public int CourseDepartmentId { get; set; }
    public int CourseId { get; set; }
    public int DepartmentId { get; set; }

    public Course Course { get; set; }
    public Student Department { get; set; }
  }
}