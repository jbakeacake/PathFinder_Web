using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Users;
using AutoMapper;
using Domain;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserForDetailsDto>>> List()
        {
            var usersFromRepo = await _mediator.Send(new List.Query());
            var usersForDetails = _mapper.Map<List<UserForDetailsDto>>(usersFromRepo);
            return usersForDetails;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserForDetailsDto>> Details(Guid id)
        {
            var userFromRepo = await _mediator.Send(new Details.Query{Id = id});
            var userForDetailsDto = _mapper.Map<UserForDetailsDto>(userFromRepo);
            return userForDetailsDto;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, Edit.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await _mediator.Send(new Delete.Command{Id = id});
        }
    }
}