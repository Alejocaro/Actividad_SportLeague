using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class TournamentSponsorRepository : GenericRepository<TournamentSponsor>, ITournamentSponsorRepository
    {
        public TournamentSponsorRepository(LeagueDbContext context) : base(context)
        {
        }

        public async Task<TournamentSponsor?> GetByTournamentAndSponsorAsync(int tournamentId, int sponsorId)
        {
            return await _dbSet
                .Where(ts => ts.TournamentId == tournamentId && ts.SponsorId == sponsorId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TournamentSponsor>> GetByTournamentIdAsync(int tournamentId)
        {
            return await _dbSet
                .Where(ts => ts.TournamentId == tournamentId)
                .Include(ts => ts.Sponsor)
                .ToListAsync();
        }

        public async Task<IEnumerable<TournamentSponsor>> GetBySponsorIdAsync(int sponsorId)
        {
            return await _dbSet
                .Where(ts => ts.SponsorId == sponsorId)
                .Include(ts => ts.Tournament)
                .Include(ts => ts.Sponsor)
                .ToListAsync();
        }

        public async Task<TournamentSponsor?> GetByIdWithTournamentAndSponsorAsync(int id)
        {
            return await _dbSet
                .Include(ts => ts.Tournament)
                .Include(ts => ts.Sponsor)
                .FirstOrDefaultAsync(ts => ts.Id == id);
        }
    }
}
