using Microsoft.EntityFrameworkCore;
using Plannial.Core.Data.EntityTypeConfigs;
using Plannial.Core.Entities;

namespace Plannial.Core.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Exam>().Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<Homework>().Property(x => x.Name)
              .HasMaxLength(255)
              .IsRequired();

            builder.Entity<AppUser>().Property(x => x.Id)
                .ValueGeneratedOnAdd();

            new SubjectEntityTypeConfig().Configure(builder.Entity<Subject>());
            new ReminderEntityTypeConfig().Configure(builder.Entity<Reminder>());
            new MessageEntityTypeConfig().Configure(builder.Entity<Message>());
        }
    }
}
