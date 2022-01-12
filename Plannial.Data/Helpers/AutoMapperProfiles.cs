using AutoMapper;
using Plannial.Data.Models.Entities;
using Plannial.Data.Models.Responses;

namespace Plannial.Data.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserResponse>();
            CreateMap<Subject, SubjectDetailResponse>();
            CreateMap<Subject, SubjectListResponse>().ForMember(dest => dest.Grade,
                opt => opt.MapFrom(src => src.Grade.Value));

            CreateMap<Grade, GradeDetailResponse>();
            CreateMap<Reminder, ReminderResponse>();
            CreateMap<Exam, ExamDetailResponse>();
            CreateMap<Exam, ExamListResponse>();
            CreateMap<Homework, HomeworkListResponse>();
            CreateMap<Homework, HomeworkDetailResponse>();
            CreateMap<Message, MessageResponse>();
            CreateMap<Reminder, ReminderResponse>();
        }
    }
}
