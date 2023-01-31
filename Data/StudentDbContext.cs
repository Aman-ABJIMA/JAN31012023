using Microsoft.EntityFrameworkCore;
using WebApplication_Student.Model;

namespace WebApplication_Student.Data
{
    public class StudentDbContext:DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext>options):base(options) 
        {
        
        }
        public DbSet<Student> Students { get; set; }
    }
}
