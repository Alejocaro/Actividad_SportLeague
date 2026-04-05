using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class SponsorRepository : GenericRepository<Sponsor>, ISponsorRepository
    {
        public SponsorRepository(LeagueDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByNameAsync(string name, int? excludeSponsorId = null)
        {
            var query = _dbSet.Where(s => s.Name.ToLower() == name.ToLower());
            if (excludeSponsorId.HasValue)
                query = query.Where(s => s.Id != excludeSponsorId.Value);
            return await query.AnyAsync();
        }
    }
}
