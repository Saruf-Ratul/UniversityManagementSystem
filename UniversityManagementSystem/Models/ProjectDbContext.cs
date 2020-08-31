using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using UniversityManagementSystem.Models.ViewModel;

namespace UniversityManagementSystem.Models
{
    public class ProjectDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<AssignCrouse> AssignCrouses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<RoomInfo> RoomInfos { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<RoomAllocation> RoomAllocations { get; set; }
        public DbSet<CourseEnroll> CourseEnrolls { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<StudentResult> StudentResults { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}