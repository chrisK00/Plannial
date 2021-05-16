using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Data.EntityTypeConfigs
{
    public class GradeEntityTypeConfig : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasMany<Subject>()
                .WithOne(x => x.Grade);

            builder.Property(x => x.Value)
                .HasMaxLength(1)
                .HasColumnType("char");
        }
    }
}
