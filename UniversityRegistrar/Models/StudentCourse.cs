namespace UniversityRegistrar.Models
{
  public class StudentCourse
  {
    public int StudentCourseId {get;set;}
    
    public int CourseId{get;set;}
    public int StudentId{get;set;}

    public Course Course {get;set;}
    public Student Student {get;set;}
  }
}