using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Companies;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IPS.CMS.API.Controllers
{
    public class CompanyController : BaseController
    {
        [Authorize(Roles = ("SuperAdmin"))]
        [HttpGet]
        public async Task<ActionResult<List<Company>>> List()
        {
            return await Mediator.Send(new ListCompany.Query());
        }

        [Authorize(Roles = ("SuperAdmin"))]
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> Details(Guid id)
        {
            return await Mediator.Send(new DetailsCompany.Query { Id = id });
        }

        [Authorize(Roles = ("SuperAdmin"))]
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateCompany.CommandCreateCompany command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(Roles = ("SuperAdmin"))]
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, EditCompany.CommandEditCompany command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }
    }
}