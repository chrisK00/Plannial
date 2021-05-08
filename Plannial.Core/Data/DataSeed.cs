using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Data
{
    public static class DataSeed
    {
        public static async Task SeedAsync(DataContext context)
        {
            if (await context.Users.AnyAsync())
            {
                return;
            }

            var users = new Faker<AppUser>()
                 .RuleFor(x => x.Email, x => x.Person.Email)
                 .RuleFor(x => x.Password, "Password123.")
                 .Generate(2).ToList();

            users.Add(new AppUser { Email = "christian@gmail.com", Password = "Password123" });

            //when adding a id will be generated
            await context.AddRangeAsync(users);

            var subjects = new Faker<Subject>()
                .RuleFor(x => x.UserId, x => users[x.Random.Number(0, users.Count - 1)].Id)
                .RuleFor(x => x.Name, x => x.Random.Word())
                .RuleFor(x => x.Description, x => x.Lorem.Sentence())
                .Generate(4).ToList();

            foreach (var item in subjects)
            {
                var exam = CreateExamGenerator().Generate();
                var homework = CreateHomeworkGenerator().Generate();

                exam.UserId = item.UserId;
                homework.UserId = item.UserId;
                item.Exams.Add(exam);
                item.Homeworks.Add(homework);
            }

            //! TODO fake reminders

            await context.AddRangeAsync(subjects);
            await context.SaveChangesAsync();
        }

        private static Faker<Exam> CreateExamGenerator()
        {
            return new Faker<Exam>()
                 .RuleFor(x => x.Name, x => x.Random.Word())
                 .RuleFor(x => x.Description, x => x.Lorem.Sentence())
                 .RuleFor(x => x.DueDate, x => x.Date.Future());
        }

        private static Faker<Homework> CreateHomeworkGenerator()
        {
            return new Faker<Homework>()
                .RuleFor(x => x.Name, x => x.Random.Word())
                .RuleFor(x => x.Description, x => x.Lorem.Sentence())
                .RuleFor(x => x.DueDate, x => x.Date.Future());
        }
    }
}
