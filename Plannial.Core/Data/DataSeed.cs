using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Plannial.Core.Entities;

namespace Plannial.Core.Data
{
    public static class DataSeed
    {
        public static async Task SeedAsync(DataContext context)
        {

            var users = new Faker<AppUser>()
                 .RuleFor(x => x.Email, x => x.Person.Email)
                 .RuleFor(x => x.Password, "Password123.")
                 .Generate(3).ToList();

            //when adding a id will be generated
            await context.AddRangeAsync(users);

            var examGenerator = CreateExamGenerator();
            var homeworkGenerator = CreateHomeworkGenerator();

            var subjects = new Faker<Subject>()
                .RuleFor(x => x.UserId, x => users[x.Random.Number(0, users.Count - 1)].Id)
                .RuleFor(x => x.Name, x => x.Random.Word())
                .RuleFor(x => x.Exams, examGenerator.Generate(2))
                .RuleFor(x => x.Homeworks, homeworkGenerator.Generate(2))
                .RuleFor(x => x.Description, x => x.Lorem.Sentence())
                .Generate(3).ToList();

            //! TODO fake reminders
      
            await context.AddRangeAsync(subjects);
            await context.SaveChangesAsync();
        }

        private static Faker<Exam> CreateExamGenerator()
        {
            return new Faker<Exam>()
                 .RuleFor(x => x.Name, x => x.Random.Word())
                 .RuleFor(x => x.Description, x => x.Lorem.Sentence());
        }

        private static Faker<Homework> CreateHomeworkGenerator()
        {
            return new Faker<Homework>()
                .RuleFor(x => x.Name, x => x.Random.Word())
                .RuleFor(x => x.Description, x => x.Lorem.Sentence());
        }

    }
}
