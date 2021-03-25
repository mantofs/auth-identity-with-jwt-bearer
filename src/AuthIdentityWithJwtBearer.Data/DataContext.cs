using System.Linq;
using AuthIdentityWithJwtBearer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthIdentityWithJwtBearer.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }

  }
}