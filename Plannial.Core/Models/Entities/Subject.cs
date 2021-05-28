﻿using System.Collections.Generic;

namespace Plannial.Core.Models.Entities
{
    public class Subject : BaseOwnedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Grade Grade { get; set; }
    }
}
