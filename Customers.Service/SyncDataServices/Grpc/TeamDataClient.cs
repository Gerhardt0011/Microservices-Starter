using AutoMapper;
using Customers.Service.Models;
using Customers.Service.Settings;
using Grpc.Net.Client;
using Teams.Service;

namespace Customers.Service.SyncDataServices.Grpc;

public class TeamDataClient : ITeamDataClient
{
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;

    public TeamDataClient(IConfiguration config, IMapper mapper)
    {
        _config = config;
        _mapper = mapper;
    }

    public IEnumerable<Team>? GetAllTeams()
    {
        var grpcSettings = _config.GetSection(nameof(GrpcSettings)).Get<GrpcSettings>();
        Console.WriteLine($"gRPC Team Service: {grpcSettings!.GrpcTeam}");

        var channel = GrpcChannel.ForAddress(grpcSettings!.GrpcTeam);
        var client = new GrpcTeam.GrpcTeamClient(channel);
        var request = new GetAllRequest();

        try
        {
            var reply = client.GetAllTeams(request);
            var teams = _mapper.Map<IEnumerable<Team>>(reply.Team);

            return teams;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not call the gRPC Server: {ex.Message}");
            return null;
        }
    }
}