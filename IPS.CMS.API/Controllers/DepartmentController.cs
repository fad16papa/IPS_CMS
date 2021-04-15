using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Departments;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IPS.CMS.API.Controllers
{
    public class DepartmentController : BaseController
    {
        [Authorize(Roles = ("SuperAdmin"))]
        [HttpGet]
        public async Task<ActionResult<List<Department>>> List()
        {
            return await Mediator.Send(new ListDeprtment.Query());
        }

        [Authorize(Roles = ("SuperAdmin"))]
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> Details(Guid id)
        {
            return await Mediator.Send(new DetailsDepartment.Query { Id = id });
        }

        [Authorize(Roles = ("SuperAdmin"))]
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateDepartment.CommandCreateDepartment command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(Roles = ("SuperAdmin"))]
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, EditDepartment.CommandEditDepartment command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }
    }
}