using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using Plannial.Core.Interfaces;
using Plannial.Core.Mappers;
using Plannial.Core.Queries;
using Xunit;

namespace Plannial.Tests.HandlerTests
{
    public class GetReminderTests
    {
        private readonly Mock<IReminderRepository> _mockReminderRepository = new();
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));
        private readonly GetReminder.Handler _subject;

        public GetReminderTests()
        {
            _subject = new GetReminder.Handler(_mockReminderRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetReminder_ReturnsNull_IfReminderDoesNotExist()
        {
            var query = new GetReminder.Query(Guid.NewGuid().ToString(), 1);
            var result = await _subject.Handle(query,default);

            result.Should().BeNull();
        }
    }
}
