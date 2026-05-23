using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsLeague.API.DTOs.Request;
using SportsLeague.API.DTOs.Response;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers
{
    [ApiController]
    [Route("api/match/{matchId}/lineup")]
    public class MatchLineupController : ControllerBase
    {
        private readonly IMatchLineupService _lineupService;
        private readonly IMapper _mapper;

        public MatchLineupController(
            IMatchLineupService lineupService,
            IMapper mapper)
        {
            _lineupService = lineupService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<MatchLineupDTO>> Add(
            int matchId, CreateMatchLineupDTO dto)
        {
            try
            {
                var lineup = _mapper.Map<MatchLineup>(dto);
                var created = await _lineupService.AddAsync(matchId, lineup);
                var lineups = await _lineupService.GetByMatchAsync(matchId);
                var createdLineup = lineups.FirstOrDefault(l => l.Id == created.Id);
                return CreatedAtAction(
                    nameof(Get),
                    new { matchId },
                    _mapper.Map<MatchLineupDTO>(createdLineup));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchLineupDTO>>> Get(int matchId)
        {
            try
            {
                var lineups = await _lineupService.GetByMatchAsync(matchId);
                return Ok(_mapper.Map<IEnumerable<MatchLineupDTO>>(lineups));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("team/{teamId}")]
        public async Task<ActionResult<IEnumerable<MatchLineupDTO>>> GetByTeam(
            int matchId, int teamId)
        {
            try
            {
                var lineups = await _lineupService
                    .GetByMatchAndTeamAsync(matchId, teamId);
                return Ok(_mapper.Map<IEnumerable<MatchLineupDTO>>(lineups));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int matchId, int id)
        {
            try
            {
                await _lineupService.DeleteAsync(matchId, id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
