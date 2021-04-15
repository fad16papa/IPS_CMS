using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Positions;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IPS.CMS.API.Controllers
{
    public class PositionController : BaseController
    {
        [Authorize(Roles = ("SuperAdmin"))]
        [HttpGet]
        public async Task<ActionResult<List<Position>>> List()
        {
            return await Mediator.Send(new ListPosition.Query());
        }

        [Authorize(Roles = ("SuperAdmin"))]
        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> Details(Guid id)
        {
            return await Mediator.Send(new DetailsPosition.Query { Id = id });
        }

        [Authorize(Roles = ("SuperAdmin"))]
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreatePosition.CommandCreatePosition command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(Roles = ("SuperAdmin"))]
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, EditPosition.CommandEditPosition command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }
    }
}