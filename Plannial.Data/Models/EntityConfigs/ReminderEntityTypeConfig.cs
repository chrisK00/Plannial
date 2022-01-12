using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plannial.Data.Models.Entities;

namespace Plannial.Data.Models.EntityConfigs
{
    public class ReminderEntityTypeConfig : IEntityTypeConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            builder.Property(x => x.Priority)
                .HasConversion<string>();

            builder.HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasQueryFilter(x => x.DeletedDate == null);
        }
    }
}
