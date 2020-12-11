using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BadFoodApi.Models
{
  public class BadFoodApiContextFactory : IDesignTimeDbContextFactory<BadFoodApiContext>
  {

    BadFoodApiContext IDesignTimeDbContextFactory<BadFoodApiContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<BadFoodApiContext>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseMySql(connectionString);

      return new BadFoodApiContext(builder.Options);
    }
  }
}