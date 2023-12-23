using TableGenius.Api.Entities;
using TableGenius.Api.Entities.Default;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TableGenius.Api.Repo.Database.Configurations;

public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T: Base
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(e => e.Deleted).HasDefaultValue(false);
        builder.Property(e => e.CreateDate).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        builder.Property(e => e.ModDate).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
    }
}