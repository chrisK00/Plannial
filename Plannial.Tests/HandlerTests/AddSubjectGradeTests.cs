using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plannial.Tests.HandlerTests
{
    public class AddSubjectGradeTests
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
    }
}
