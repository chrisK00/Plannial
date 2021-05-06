﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plannial.Core.Entities;

namespace Plannial.Core.EntityTypeConfigs
{
    public class ReminderEntityTypeConfig : IEntityTypeConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {

            builder.Property(x => x.Priority)
                .HasConversion<string>();

            builder.HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Category)
                .WithMany();
        }
    }
}