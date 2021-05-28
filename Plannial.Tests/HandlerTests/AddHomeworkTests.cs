using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Plannial.Core.Commands.AddCommands;
using Plannial.Core.Interfaces;
using Plannial.Core.Mappers;
using Xunit;

namespace Plannial.Tests.HandlerTests
{
    public class AddHomeworkTests
    {
        private readonly Mock<ISubjectRepository> _subjectRepository = new();
        private readonly Mock<IHomeworkRepository> _homeworkRepository = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<ILogger<AddHomework.Handler>> _addHomeworkLogger = new();
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));

        private readonly AddHomework.Handler _subject;

        public AddHomeworkTests()
        {
            _unitOfWork.Setup(_ => _.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
            _subject = new AddHomework.Handler(_mapper, _unitOfWork.Object, _subjectRepository.Object, _addHomeworkLogger.Object, _homeworkRepository.Object);
        }

        [Fact]
        public async Task AddHomework_ThrowsUnauthorized_When_UserDoesNotOwnSubject()
        {
            var command = new AddHomework.Command("fluentassertions week 2", "", DateTime.UtcNow, 1, Guid.NewGuid().ToString());

            _subjectRepository.Setup(_ => _.GetSubjectByIdAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);

            await _subject.Invoking(_ => _.Handle(command, default))
                .Should().ThrowAsync<UnauthorizedAccessException>();
        }

       /* [Fact]
        public async Task AddHomework_AddsHomeworkToTheSubject_WhenSubjectExists()
        {
            var existingSubject = new Subject();

            _subjectRepository.Setup(_ => _.GetSubjectByIdAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingSubject);

            await _subject.Handle(new AddHomework.Command("Read up on penguins", string.Empty, DateTime.Now, 1,
                Guid.NewGuid().ToString()), default);

            existingSubject.Homeworks.Count.Should().BeGreaterThan(0);
        }*/

       /* [Fact]
        public async Task AddHomework_Returns_NewHomework_IfSubject_Exists()
        {
            var homeworkToAdd = new AddHomework.Command("Read up on penguins", string.Empty, DateTime.Now, 1, Guid.NewGuid().ToString());

            _subjectRepository.Setup(_ => _.GetSubjectByIdAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Subject());

            var newHomework = await _subject.Handle(homeworkToAdd, default);

            newHomework.Should().NotBeNull();
            newHomework.Name.Should().BeSameAs(homeworkToAdd.Name);
        }*/
    }
}
