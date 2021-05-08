using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Data.EntityTypeConfigs
{
    public class CategoryEntityTypeConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
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
