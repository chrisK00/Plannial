using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Plannial.Core.Commands.AddCommands;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;
using Xunit;

namespace Plannial.Tests.HandlerTests
{
    public class AddSubjectGradeTests
    {
        private readonly Mock<ISubjectRepository> _subjectRepository = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<ILogger<AddSubjectGrade.Handler>> _logger = new();

        private readonly AddSubjectGrade.Handler _subject;

        public AddSubjectGradeTests()
        {
            _unitOfWork.Setup(_ => _.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
            _subject = new AddSubjectGrade.Handler(_subjectRepository.Object, _unitOfWork.Object, _logger.Object);
        }

        [Fact]
        public async Task AddSubjectGrade_AddsGradeToSubject()
        {
            var command = new AddSubjectGrade.Command("userid", 1, "A", DateTime.UtcNow, "Could have done better");
           // var existingGrade = new Grade { Id = 1, Value = "A" };

            var mockSubject = new Subject();

            _subjectRepository.Setup(_ => _.GetSubjectByIdAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSubject);

            // act
            await _subject.Handle(command, default);

            // assert

            mockSubject.Grade.Value.Should().Be(command.Grade);
        }
    }
}
