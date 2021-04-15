using System.Threading.Tasks;
using Application.UserRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IPS.CMS.API.Controllers
{
    public class RoleController : BaseController
    {
        [Authorize(Roles = ("SuperAdmin"))]
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateUserRole.CommandCreateUserRole command)
        {
            return await Mediator.Send(command);
        }
    }
}