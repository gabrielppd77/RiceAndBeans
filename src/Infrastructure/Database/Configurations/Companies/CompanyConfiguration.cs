using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

using Domain.Companies;
using Domain.Users;

namespace Infrastructure.Database.Configurations.Companies;

internal sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Path).IsUnique();

        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Path).IsRequired().HasMaxLength(150);

        builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
    }
}