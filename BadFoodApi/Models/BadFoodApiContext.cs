using Microsoft.EntityFrameworkCore;

namespace BadFoodApi.Models
{
  public class BadFoodApiContext : DbContext
  {
    public BadFoodApiContext(DbContextOptions<BadFoodApiContext> options)
        : base(options)
    {
    }
    public DbSet<Food> Foods { get; set; }
  }
}