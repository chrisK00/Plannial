using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plannial.Data.Models.Entities;

namespace Plannial.Data.Models.EntityConfigs
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
