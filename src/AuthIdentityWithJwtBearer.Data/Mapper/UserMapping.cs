using AuthIdentityWithJwtBearer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthIdentityWithJwtBearer.Data.Mapper
{
  public class UserMapping : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder.HasKey(u => u.Id);
      builder.Property(u => u.Username)
      .IsRequired()
      .HasColumnType("varchar(30)");

      builder.Property(u => u.Password)
      .IsRequired()
      .HasColumnType("varchar(30)");

      builder.Property(u => u.Role)
      .IsRequired()
      .HasColumnType("varchar(30)");

      builder.ToTable("Users");
    }
  }
}