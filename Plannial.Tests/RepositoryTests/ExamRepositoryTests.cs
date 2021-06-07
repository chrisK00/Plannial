﻿using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Plannial.Core.Data;
using Plannial.Core.Interfaces;
using Plannial.Core.Repositories;
using Xunit;

namespace Plannial.Tests.RepositoryTests
{
    public class ExamRepositoryTests
    {
        private readonly string _userId = Guid.NewGuid().ToString();
        private readonly int _subjectId = 1;
        private readonly int _examId;

        private readonly IExamRepository _subject;

        public ExamRepositoryTests()
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("exams");
            var context = new DataContext(contextOptions.Options);
            _subject = new ExamRepository(context);

            //random exams
            var exams = DataSeed.CreateExamGenerator()
                .RuleFor(x => x.UserId, Guid.NewGuid().ToString())
                .RuleFor(x => x.SubjectId, x => x.Random.Number())
                .Generate(5);

            //user owned exams
            var userExams = DataSeed.CreateExamGenerator()
                .RuleFor(x => x.UserId, _userId)
                .RuleFor(x => x.SubjectId, _subjectId)
                .Generate(5);

            context.Exams.AddRange(exams);
            context.Exams.AddRange(userExams);
            context.SaveChanges();

            _examId = userExams.FirstOrDefault().Id;
        }

        [Fact]
        public async Task GetExam_ReturnsExam_When_Exists()
        {
            var exam = await _subject.GetExamAsync(_examId, _userId);
            exam.Should().NotBeNull();
            exam.UserId.Should().Be(_userId);
        }

        [Fact]
        public async Task GetExams_ReturnsExams_With_TheSameSubjectId()
        {
            var exams = await _subject.GetExamsAsync(_userId, _subjectId);

            exams.Should().OnlyContain(x => x.SubjectId == _subjectId);
        }
    }
}
