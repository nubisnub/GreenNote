using GreenNote.StudentInfo.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenNote.StudentInfo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ClassInfo> ClassInfos        { get; set; }
    }
}
