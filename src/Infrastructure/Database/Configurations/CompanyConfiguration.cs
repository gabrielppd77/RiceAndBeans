using Domain.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Path).IsUnique();

        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.Path).HasMaxLength(150);
        builder.Property(x => x.UrlImage).HasMaxLength(150);

        builder.HasOne(x => x.User).WithOne(x => x.Company).HasForeignKey<Company>(x => x.UserId);
    }
}