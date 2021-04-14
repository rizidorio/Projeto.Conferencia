using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapper
{
    public class ProdutcMapper : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Code).IsUnique();
            builder.Property(p => p.Code).HasMaxLength(5).IsRequired();
            builder.Property(p => p.Name).HasMaxLength(80).IsRequired();
            builder.Property(p => p.Cust).HasColumnType("decimal(15,2)").IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(15,4)").IsRequired();
            builder.Property(p => p.Ncm).HasMaxLength(8).IsRequired();
            builder.Property(p => p.Reference).HasMaxLength(30).IsRequired();
            builder.Property(p => p.DateRegister).IsRequired();
        }
    }
}