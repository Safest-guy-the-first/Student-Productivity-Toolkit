using Microsoft.EntityFrameworkCore;
using SPT_API.Models;

namespace SPT_API.Data
{
    public class SPT_APIDbContext : DbContext
    {
        public DbSet<StudentModel> StudentTable { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = "C:\\Users\\David Oyem\\source\\repos\\Student Productivity Toolkit\\SPT-API\\Data\\SPT-APIDatabase";
            optionsBuilder.UseSqlite($"Data Source ={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentModel>().HasKey(s => s._id);
        }


        public SPT_APIDbContext(DbContextOptions<SPT_APIDbContext> options) : base(options) { }
        public SPT_APIDbContext() { }
    }
}
