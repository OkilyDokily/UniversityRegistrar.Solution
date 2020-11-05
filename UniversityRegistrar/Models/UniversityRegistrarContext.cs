using Microsoft.EntityFrameworkCore;

namespace UniversityRegistrar.Models
{
  public class UniversityRegistrarContext : DbContext
  {

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Department>()
        .HasData(
          new Department { DepartmentId = 1, Name = "Undeclared"}
      );
    }

    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<StudentCourse> StudentCourses { get; set; }
    public UniversityRegistrarContext(DbContextOptions options) : base(options) { }
  }
}