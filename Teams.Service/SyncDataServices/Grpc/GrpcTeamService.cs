using AutoMapper;
using Grpc.Core;
using Teams.Service.Contracts.Repositories;

namespace Teams.Service.SyncDataServices.Grpc;

public class GrpcTeamService : GrpcTeam.GrpcTeamBase
{
    private readonly ITeamsRepository _teamsRepository;
    private readonly IMapper _mapper;

    public GrpcTeamService(ITeamsRepository teamsRepository, IMapper mapper)
    {
        _teamsRepository = teamsRepository;
        _mapper = mapper;
    }

    public override async Task<TeamResponse> GetAllTeams(GetAllRequest request, ServerCallContext context)
    {
        var response = new TeamResponse();
        var teams = await _teamsRepository.GetTeamsAsync();

        response.Team.AddRange(_mapper.Map<IEnumerable<GrpcTeamModel>>(teams));

        return response;
    }
}