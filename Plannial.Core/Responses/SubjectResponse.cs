﻿using System.Collections.Generic;

namespace Plannial.Core.Responses
{
    public class SubjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ExamResponse> Exams { get; set; } = new List<ExamResponse>();
        public ICollection<HomeworkResponse> Homeworks { get; set; } = new List<HomeworkResponse>();
    }
}