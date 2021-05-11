using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Plannial.Core.Commands;
using Plannial.Core.Helpers;
using Plannial.Core.Interfaces;
using Xunit;

namespace Plannial.Tests.HandlerTests
{
    public class AddHomeworkTests
    {
        private readonly Mock<ISubjectRepository> _subjectRepository = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<ILogger<AddHomework.Handler>> _addHomeworkLogger = new();
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));

        private readonly AddHomework.Handler _subject;

        public AddHomeworkTests()
        {
            _unitOfWork.Setup(_ => _.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
            _subject = new AddHomework.Handler(_mapper, _unitOfWork.Object, _subjectRepository.Object, _addHomeworkLogger.Object);
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
    }
}
