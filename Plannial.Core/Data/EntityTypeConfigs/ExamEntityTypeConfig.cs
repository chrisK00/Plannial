using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Data.EntityTypeConfigs
{
    public class ExamEntityTypeConfig : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasOne<AppUser>()
               .WithMany()
               .HasForeignKey(x => x.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Subject>()
               .WithMany()
               .HasForeignKey(x => x.SubjectId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
