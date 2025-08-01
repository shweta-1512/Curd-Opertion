using Microsoft.EntityFrameworkCore;
using SawggerDemo.Model;

namespace SawggerDemo.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Student> Students { get; set; }
}
