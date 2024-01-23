﻿using EcoLink.Domain.Entities.Representation;
using EcoLink.Application.Representatives.Queries.GetRepresentatives;
using EcoLink.Application.RepresentationApps.Commands.CreateRepresentationApps;
using EcoLink.Application.RepresentationApps.Queries.GetRepresentationApp;

namespace EcoLink.WebApi.Controllers.Representative;

public class RepresentationAppsController(IMediator mediator) : BaseController
{
    [HttpPost("create-with-return")]
    [ProducesResponseType(typeof(RepresentationApp), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateRepresentationAppWithReturnCommand command)
        => Ok(await mediator.Send(new CreateRepresentationAppWithReturnCommand(command)));

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(RepresentationApp), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id)
        => Ok(await mediator.Send(new GetRepresentationAppByIdQuery(id)));

    [HttpGet("get-all-by-user-id/{userId:long}")]
    [ProducesResponseType(typeof(IEnumerable<RepresentationApp>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByUserId(long userId)
        => Ok(await mediator.Send(new GetRepresentationAppByIdQuery(userId)));
}
