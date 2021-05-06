﻿using AutoMapper;
using Plannial.Core.Entities;
using Plannial.Core.Responses;

namespace Plannial.Core.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserResponse>();
            CreateMap<Subject, SubjectResponse>();
            CreateMap<Reminder, ReminderResponse>();
            CreateMap<Exam, ExamResponse>();
            CreateMap<Homework, HomeworkResponse>();
        }
    }
}
