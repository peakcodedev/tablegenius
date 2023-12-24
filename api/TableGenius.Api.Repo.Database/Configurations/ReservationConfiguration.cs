﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableGenius.Api.Entities.Reservations;

namespace TableGenius.Api.Repo.Database.Configurations;

public class ReservationConfiguration : BaseConfiguration<Reservation>,
    IEntityTypeConfiguration<Reservation>
{
    public new void Configure(EntityTypeBuilder<Reservation> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.Name).IsRequired();
        builder.Property(b => b.BookingDate).IsRequired();
        builder.Property(b => b.Count).IsRequired();
        builder.Property(b => b.PhoneNumber).IsRequired(false);
        builder.Property(b => b.Comments).HasMaxLength(1024).IsRequired(false);
        builder.ToTable("Reservations");
    }
}