using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plannial.Core.Entities;

namespace Plannial.Core.EntityTypeConfigs
{
    public class SubjectEntityTypeConfig : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasOne<AppUser>()
             .WithMany()
             .HasForeignKey(x => x.UserId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
