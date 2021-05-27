using AutoMapper;
using Plannial.Core.Models.Entities;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserResponse>();
            CreateMap<Subject, SubjectResponse>().ForMember(dest => dest.Grade,
                opt => opt.MapFrom(src => src.Grade.Value));

            CreateMap<Reminder, ReminderResponse>();
            CreateMap<Exam, ExamResponse>();
            CreateMap<Homework, HomeworkResponse>();
            CreateMap<Message, MessageResponse>();
            CreateMap<Reminder, ReminderResponse>();
        }
    }
}
