using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface IMatchLineupService
    {
        Task<MatchLineup> AddAsync(int matchId, MatchLineup lineup);
        Task<IEnumerable<MatchLineup>> GetByMatchAsync(int matchId);
        Task<IEnumerable<MatchLineup>> GetByMatchAndTeamAsync(int matchId, int teamId);
        Task DeleteAsync(int matchId, int lineupId);
    }
}
