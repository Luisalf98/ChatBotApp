using ChatBotApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatBotApp.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
  }
}
