using System;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUserRole : IdentityRole
    {
        public DateTime DateCreated { get; set; }
        public bool IsEnable { get; set; }
    }
}