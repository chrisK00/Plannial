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
using Plannial.Core.Models.Entities;
using Xunit;

namespace Plannial.Tests.HandlerTests
{
    public class CommandsTests
    {
        private readonly Mock<ISubjectRepository> _subjectRepository = new Mock<ISubjectRepository>();
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<ILogger<AddSubject.Handler>> _addSubjectLogger = new Mock<ILogger<AddSubject.Handler>>();
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));

        public CommandsTests()
        {
            _unitOfWork.Setup(_ => _.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
        }

        [Fact]
        public async Task AddSubject_Sends_Back_Newly_Created_Subject()
        {

            var handler = new AddSubject.Handler(_mapper, _subjectRepository.Object, _unitOfWork.Object, _addSubjectLogger.Object);

            var command = new AddSubject.Command("Math", "Not so fun", Guid.NewGuid().ToString());

            var newSubject = await handler.Handle(command, default);

            newSubject.Should().NotBeNull();
            newSubject.Name.Should().BeEquivalentTo(command.Name);
            newSubject.Description.Should().BeEquivalentTo(command.Description);
        }

    }
}
