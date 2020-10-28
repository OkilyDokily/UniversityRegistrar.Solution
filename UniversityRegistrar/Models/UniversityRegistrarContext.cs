using Microsoft.EntityFrameworkCore;

namespace UniversityRegistrar.Models
{
    public class UniversityRegistrarContext : DbContext
    {
        public virtual DbSet<Course> Courses {get;set;}
        public virtual DbSet<Student> Students {get;set;}
        public UniversityRegistrarContext(DbContextOptions options):base(options){}
    }
}