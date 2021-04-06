using System;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public Guid PositionId { get; set; }
        public virtual Position Position { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}