using TableGenius.Api.Entities.Company;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TableGenius.Api.Repo.Database.Configurations;

public class EmployeeConfiguration : BaseConfiguration<Employee>,
    IEntityTypeConfiguration<Employee>
{
    public new void Configure(EntityTypeBuilder<Employee> builder)
    {
        base.Configure(builder);
        builder.HasOne(e => e.Company).WithMany(e => e.Employees).HasForeignKey(p => p.CompanyId)
            .IsRequired(false);
        builder.HasOne(e => e.User).WithOne().HasForeignKey<Employee>(p => p.UserId)
            .IsRequired(false);
    }
}