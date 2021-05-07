using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plannial.Core.Entities;

namespace Plannial.Core.Data.EntityTypeConfigs
{
    public class MessageEntityTypeConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(x => x.Content)
                .IsRequired();

            builder.HasOne<AppUser>()
                .WithMany(x => x.MessagesRecieved)
                .HasForeignKey(x => x.RecipientId)
                .IsRequired();

            builder.HasOne<AppUser>()
                .WithMany(x => x.MessagesSent)
                .HasForeignKey(x => x.SenderId)
                .IsRequired();
        }
    }
}
