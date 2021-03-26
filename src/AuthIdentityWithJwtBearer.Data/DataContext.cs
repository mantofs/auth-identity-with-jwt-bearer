using System.Linq;
using AuthIdentityWithJwtBearer.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

  public class AuthContext : IdentityDbContext
  {
    public AuthContext(DbContextOptions<AuthContext> opt) : base(opt) { }
  }
}