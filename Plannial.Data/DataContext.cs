using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Plannial.Data.Models.Entities;
using Plannial.Data.Models.EntityConfigs;

namespace Plannial.Core.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Grade> Grades { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new SubjectEntityTypeConfig().Configure(builder.Entity<Subject>());
            new ExamEntityTypeConfig().Configure(builder.Entity<Exam>());
            new HomeworkEntityTypeConfig().Configure(builder.Entity<Homework>());
            new ReminderEntityTypeConfig().Configure(builder.Entity<Reminder>());
            new MessageEntityTypeConfig().Configure(builder.Entity<Message>());
        }
    }
}
