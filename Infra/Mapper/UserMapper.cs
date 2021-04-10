using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapper
{
    public class UserMapper : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Login).IsUnique();
            builder.Property(u => u.Login).HasMaxLength(20).IsRequired();
            builder.Property(u => u.Password).HasMaxLength(100).IsRequired();
        }
    }
}
