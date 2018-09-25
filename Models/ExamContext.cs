using Microsoft.EntityFrameworkCore;

namespace Exam.Models
{
  public class ExamContext : DbContext
  {
    public ExamContext(DbContextOptions<ExamContext> options) : base(options) {}
    public DbSet<User> Users {get; set;}
    public DbSet<Idea> Ideas {get; set;}
    public DbSet<Like> Likes {get; set;}
  }
}