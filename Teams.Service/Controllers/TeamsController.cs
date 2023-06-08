using AutoMapper;
using Common;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teams.Service.Contracts.Repositories;
using Teams.Service.Dto.Team;
using Teams.Service.Events;
using Teams.Service.Models;

namespace Teams.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TeamsController : ControllerBase
{
    private readonly ITeamsRepository _teamsRepository;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public TeamsController(
        ITeamsRepository teamsRepository, 
        IMapper mapper, 
        IPublishEndpoint publishEndpoint)
    {
        _teamsRepository = teamsRepository;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<TeamReadDto>>> GetTeams()
    {
        var teams = await _teamsRepository.GetTeamsAsync();
        
        return Ok(_mapper.Map<IEnumerable<TeamReadDto>>(teams));
    }
    
    [HttpGet("{id}", Name = "GetTeamById")]
    public async Task<ActionResult<TeamReadDto>> GetTeamById(string id)
    {
        var team = await _teamsRepository.GetTeamAsync(id);
        
        if (team == null)
            return NotFound();
        
        if (team.UserId != HttpContext.GetUserId())
            return Forbid();
        
        return Ok(_mapper.Map<TeamReadDto>(team));
    }
    
    [HttpPost]
    public async Task<ActionResult<TeamReadDto>> CreateTeam(TeamCreateDto teamCreateDto)
    {
        var team = _mapper.Map<Team>(teamCreateDto);
        await _teamsRepository.CreateTeamAsync(team);
        
        var teamReadDto = _mapper.Map<TeamReadDto>(team);

        await _publishEndpoint.Publish(_mapper.Map<TeamCreated>(team));
        
        return CreatedAtRoute(nameof(GetTeamById), new { Id = teamReadDto.Id }, teamReadDto);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTeam(string id, TeamUpdateDto teamUpdateDto)
    {
        var team = await _teamsRepository.GetTeamAsync(id);
        
        if (team == null)
            return NotFound();
        
        if (team.UserId != HttpContext.GetUserId())
            return Forbid();
        
        _mapper.Map(teamUpdateDto, team);
        
        await _teamsRepository.UpdateTeamAsync(team);
        
        await _publishEndpoint.Publish(_mapper.Map<TeamUpdated>(team));
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTeam(string id)
    {
        var team = await _teamsRepository.GetTeamAsync(id);
        
        if (team == null)
            return NotFound();
        
        if (team.UserId != HttpContext.GetUserId())
            return Forbid();
        
        await _teamsRepository.DeleteTeamAsync(id);
        
        await _publishEndpoint.Publish(new TeamDeleted
        {
            Id = id
        });
        
        return NoContent();
    }
}