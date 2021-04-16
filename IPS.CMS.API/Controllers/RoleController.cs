using System.Collections.Generic;
using System.Threading.Tasks;
using Application.UserRole;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IPS.CMS.API.Controllers
{
    public class RoleController : BaseController
    {
        [Authorize(Roles = ("SuperAdmin"))]
        [HttpGet]
        public async Task<ActionResult<List<AppUserRole>>> List()
        {
            return await Mediator.Send(new ListRole.Query());
        }

        [Authorize(Roles = ("SuperAdmin"))]
        [HttpPost]
        public async Task<ActionResult<Role>> Register(CreateUserRole.CommandCreateUserRole command)
        {
            return await Mediator.Send(command);
        }
    }
}