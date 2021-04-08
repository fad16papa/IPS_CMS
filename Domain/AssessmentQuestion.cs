using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AssessmentQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string Name { get; set; }
        public Guid AssessmentTypeId { get; set; }
        public virtual AssessmentType AssessmentType { get; set; }
        public int Points { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsEnable { get; set; }
    }
}