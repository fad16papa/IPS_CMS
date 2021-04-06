using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsEnable { get; set; }
        public ICollection<AppUser> AppUser { get; set; }
    }
}