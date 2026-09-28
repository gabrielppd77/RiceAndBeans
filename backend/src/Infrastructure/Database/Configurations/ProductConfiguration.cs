using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.Price).HasColumnType("numeric(12,2)");

        builder.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId);
        builder.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId);
    }
}