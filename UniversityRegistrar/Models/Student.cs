using System.Collections.Generic;
using System;

namespace UniversityRegistrar.Models
{
    public class Student
    {
        public int StudentId {get;set;}
        public string Name {get;set;}
        public DateTime DateOfEnrollment {get;set;}

        public virtual ICollection<StudentCourse> Courses {get;set;}
        public virtual ICollection<StudentDepartment> StudentDepartments {get;set;}
        public Student()
        {
            this.Courses = new HashSet<StudentCourse>();
            this.StudentDepartments = new HashSet<StudentDepartment>();
            this.DateOfEnrollment = DateTime.Now;
        }
    }
}