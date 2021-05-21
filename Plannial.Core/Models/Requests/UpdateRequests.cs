﻿using System;

namespace Plannial.Core.Models.Requests
{
    public record UpdateExamRequest(string Name, string Description, DateTime DueDate);
    public record UpdateHomeworkRequest(string Name, string Description, DateTime DueDate);
}
