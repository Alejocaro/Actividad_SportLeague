using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(LeagueDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Player>> GetByTeamAsync(int teamId)
        {
            return await _dbSet
                .Where(p => p.TeamId == teamId) //Es el mismo Where de LINQ, pero en este caso se traduce a una consulta SQL que filtra por el TeamId
                .Include(p => p.Team) // Incluye la entidad relacionada Team en la consulta, lo que permite acceder a los datos del equipo asociado a cada jugador sin necesidad de realizar una consulta adicional.
                .ToListAsync();
        }

        public async Task<Player?> GetByTeamAndNumberAsync(int teamId, int number)
        {
            return await _dbSet
                .Where(p => p.TeamId == teamId && p.Number == number) // Filtra por TeamId y Number
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Player>> GetAllWithTeamAsync()
        {
            return await _dbSet
                .Include(p => p.Team)
                .ToListAsync();
        }

        public async Task<Player?> GetByIdWithTeamAsync(int id)
        {
            return await _dbSet
                .Where(p => p.Id == id)
                .Include(p => p.Team)
                .FirstOrDefaultAsync();
        }
    }
}
