using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Helpers;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services
{
    public class MatchLineupService : IMatchLineupService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMatchLineupRepository _lineupRepository;
        private readonly MatchValidationHelper _validationHelper;
        private readonly ILogger<MatchLineupService> _logger;

        public MatchLineupService(
            IMatchRepository matchRepository,
            IMatchLineupRepository lineupRepository,
            MatchValidationHelper validationHelper,
            ILogger<MatchLineupService> logger)
        {
            _matchRepository = matchRepository;
            _lineupRepository = lineupRepository;
            _validationHelper = validationHelper;
            _logger = logger;
        }

        public async Task<MatchLineup> AddAsync(int matchId, MatchLineup lineup)
        {
            var match = await _matchRepository.GetByIdAsync(matchId);
            if (match == null)
                throw new KeyNotFoundException(
                    $"No se encontró el partido con ID {matchId}");

            if (match.Status != MatchStatus.Scheduled)
                throw new InvalidOperationException(
                    "Solo se pueden registrar alineaciones en partidos Scheduled");

            var player = await _validationHelper.ValidatePlayerInMatchAsync(
                lineup.PlayerId, match);

            if (await _lineupRepository.ExistsByMatchAndPlayerAsync(matchId, lineup.PlayerId))
                throw new InvalidOperationException(
                    "El jugador ya está registrado en la alineación de este partido");

            if (lineup.IsStarter)
            {
                var startersCount = await _lineupRepository
                    .CountStartersByMatchAndTeamAsync(matchId, player.TeamId);
                if (startersCount >= 11)
                    throw new InvalidOperationException(
                        "El equipo ya tiene 11 titulares registrados en este partido");
            }

            lineup.MatchId = matchId;

            _logger.LogInformation(
                "Adding lineup entry: Match {MatchId}, Player {PlayerId}, Starter {IsStarter}",
                matchId, lineup.PlayerId, lineup.IsStarter);
            return await _lineupRepository.CreateAsync(lineup);
        }

        public async Task<IEnumerable<MatchLineup>> GetByMatchAsync(int matchId)
        {
            var match = await _matchRepository.GetByIdAsync(matchId);
            if (match == null)
                throw new KeyNotFoundException(
                    $"No se encontró el partido con ID {matchId}");
            return await _lineupRepository.GetByMatchAsync(matchId);
        }

        public async Task<IEnumerable<MatchLineup>> GetByMatchAndTeamAsync(
            int matchId, int teamId)
        {
            var match = await _matchRepository.GetByIdAsync(matchId);
            if (match == null)
                throw new KeyNotFoundException(
                    $"No se encontró el partido con ID {matchId}");

            if (teamId != match.HomeTeamId && teamId != match.AwayTeamId)
                throw new InvalidOperationException(
                    "El equipo no participa en este partido");

            return await _lineupRepository.GetByMatchAndTeamAsync(matchId, teamId);
        }

        public async Task DeleteAsync(int matchId, int lineupId)
        {
            var lineup = await _lineupRepository.GetByIdAsync(lineupId);
            if (lineup == null || lineup.MatchId != matchId)
                throw new KeyNotFoundException(
                    $"No se encontró la alineación con ID {lineupId}");

            await _lineupRepository.DeleteAsync(lineupId);
        }
    }
}
