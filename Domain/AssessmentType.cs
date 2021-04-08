using System;
using System.Collections.Generic;

namespace Domain
{
    public class AssessmentType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsEnable { get; set; }
        public virtual ICollection<AssessmentQuestion> AssessmentQuestion { get; set; }
    }
}