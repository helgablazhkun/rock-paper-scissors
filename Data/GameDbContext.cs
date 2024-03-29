using Microsoft.EntityFrameworkCore;
namespace rock_paper_scissors.Data
{
  public class GameDbContext : DbContext
  {
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }
    public DbSet<Game> Games { get; set; }
  }
}