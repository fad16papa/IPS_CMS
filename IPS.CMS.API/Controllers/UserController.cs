using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.User;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IPS.CMS.API.Controllers
{
    public class UserController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<AppUser>>> List()
        {
            return await Mediator.Send(new ListUser.Query());
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(Register.CommandRegisterUser command)
        {
            return await Mediator.Send(command);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(Login.Query query)
        {
            return await Mediator.Send(query);
        }

    }
}
