using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPT_Management_System_Console_App;
using SPT_Management_System_Console_App.Models_Classes;

namespace SPT_Management_System_Console_App
{
    class AppDbContext : DbContext
    {
        public DbSet<Student_Model> StudentTable { get; set; }
        public DbSet<Course_Model> CourseTable { get; set; }
        public DbSet<Result_Model> ResultTable { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = "C:\\Users\\David Oyem\\source\\repos\\Student Productivity Toolkit\\SPT-Management System Console App\\Student Database";
            optionsBuilder.UseSqlite($"Data Source = {path}");
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student_Model>().HasKey(s => s._id);// this and

            modelBuilder.Entity<Student_Model>().Property(s=>s._id).ValueGeneratedOnAdd();//this makes the db turn _id to primary key 


            modelBuilder.Entity<Student_Model>().HasIndex(s => s.uniqueUserId).IsUnique();
            
            modelBuilder.Entity<Course_Model>().HasKey(c => c._id);
            modelBuilder.Entity<Course_Model>().Property(c=>c._id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Course_Model>()
                .HasOne(c=>c.Student).WithMany(s=>s.studentCourses)
                .HasPrincipalKey(s=>s.uniqueUserId).HasForeignKey(c=>c._CuniqueUserId);// this make the unique unique user id the foreign key to cuniqueuserid

            modelBuilder.Entity<Result_Model>().HasKey(c => c._id);
            modelBuilder.Entity<Result_Model>().Property(c => c._id).ValueGeneratedOnAdd();

        }
    }
}
