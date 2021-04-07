using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Companies;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IPS.CMS.API.Controllers
{
    public class CompanyController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Company>>> List()
        {
            return await Mediator.Send(new ListCompany.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> Details(Guid id)
        {
            return await Mediator.Send(new DetailsCompany.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateCompany.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, EditCompany.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }
    }
}