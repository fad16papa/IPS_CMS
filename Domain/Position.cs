using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Position
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Deescription { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsEnable { get; set; }
        public ICollection<AppUser> AppUser { get; set; }
    }
}