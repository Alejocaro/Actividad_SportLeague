using System.Net.Mail;
using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services
{
    public class SponsorService : ISponsorService
    {
        private readonly ISponsorRepository _sponsorRepository;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ITournamentSponsorRepository _tournamentSponsorRepository;
        private readonly ILogger<SponsorService> _logger;

        public SponsorService(
            ISponsorRepository sponsorRepository,
            ITournamentRepository tournamentRepository,
            ITournamentSponsorRepository tournamentSponsorRepository,
            ILogger<SponsorService> logger)
        {
            _sponsorRepository = sponsorRepository;
            _tournamentRepository = tournamentRepository;
            _tournamentSponsorRepository = tournamentSponsorRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Sponsor>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all sponsors");
            return await _sponsorRepository.GetAllAsync();
        }

        public async Task<Sponsor?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving sponsor with ID: {SponsorId}", id);
            var sponsor = await _sponsorRepository.GetByIdAsync(id);
            if (sponsor == null)
                _logger.LogWarning("Sponsor with ID {SponsorId} not found", id);
            return sponsor;
        }

        public async Task<Sponsor> CreateAsync(Sponsor sponsor)
        {
            if (await _sponsorRepository.ExistsByNameAsync(sponsor.Name))
            {
                _logger.LogWarning("Duplicate sponsor name: {Name}", sponsor.Name);
                throw new InvalidOperationException(
                    $"Ya existe un patrocinador con el nombre '{sponsor.Name}'");
            }

            if (!IsValidEmail(sponsor.ContactEmail))
            {
                throw new InvalidOperationException(
                    "El correo de contacto no tiene un formato válido");
            }

            _logger.LogInformation("Creating sponsor: {Name}", sponsor.Name);
            return await _sponsorRepository.CreateAsync(sponsor);
        }

        public async Task UpdateAsync(int id, Sponsor sponsor)
        {
            var existing = await _sponsorRepository.GetByIdAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Sponsor with ID {SponsorId} not found for update", id);
                throw new KeyNotFoundException($"No se encontró el patrocinador con ID {id}");
            }

            if (await _sponsorRepository.ExistsByNameAsync(sponsor.Name, id))
            {
                throw new InvalidOperationException(
                    $"Ya existe un patrocinador con el nombre '{sponsor.Name}'");
            }

            if (!IsValidEmail(sponsor.ContactEmail))
            {
                throw new InvalidOperationException(
                    "El correo de contacto no tiene un formato válido");
            }

            existing.Name = sponsor.Name;
            existing.ContactEmail = sponsor.ContactEmail;
            existing.Phone = sponsor.Phone;
            existing.WebsiteUrl = sponsor.WebsiteUrl;
            existing.Category = sponsor.Category;

            _logger.LogInformation("Updating sponsor with ID: {SponsorId}", id);
            await _sponsorRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            if (!await _sponsorRepository.ExistsAsync(id))
            {
                _logger.LogWarning("Sponsor with ID {SponsorId} not found for deletion", id);
                throw new KeyNotFoundException($"No se encontró el patrocinador con ID {id}");
            }

            _logger.LogInformation("Deleting sponsor with ID: {SponsorId}", id);
            await _sponsorRepository.DeleteAsync(id);
        }

        public async Task<TournamentSponsor> LinkToTournamentAsync(
            int sponsorId, int tournamentId, decimal contractAmount)
        {
            if (contractAmount <= 0)
            {
                throw new InvalidOperationException(
                    "El monto del contrato debe ser mayor a 0");
            }

            if (!await _sponsorRepository.ExistsAsync(sponsorId))
            {
                throw new KeyNotFoundException(
                    $"No se encontró el patrocinador con ID {sponsorId}");
            }

            if (!await _tournamentRepository.ExistsAsync(tournamentId))
            {
                throw new KeyNotFoundException(
                    $"No se encontró el torneo con ID {tournamentId}");
            }

            var duplicate = await _tournamentSponsorRepository
                .GetByTournamentAndSponsorAsync(tournamentId, sponsorId);
            if (duplicate != null)
            {
                throw new InvalidOperationException(
                    "Este patrocinador ya está vinculado a este torneo");
            }

            var link = new TournamentSponsor
            {
                SponsorId = sponsorId,
                TournamentId = tournamentId,
                ContractAmount = contractAmount,
                JoinedAt = DateTime.UtcNow
            };

            _logger.LogInformation(
                "Linking sponsor {SponsorId} to tournament {TournamentId}",
                sponsorId, tournamentId);
            await _tournamentSponsorRepository.CreateAsync(link);

            var created = await _tournamentSponsorRepository.GetByIdWithTournamentAndSponsorAsync(link.Id);
            return created
                   ?? throw new InvalidOperationException("No se pudo recuperar la vinculación creada");
        }

        public async Task<IEnumerable<TournamentSponsor>> GetTournamentLinksBySponsorAsync(int sponsorId)
        {
            if (!await _sponsorRepository.ExistsAsync(sponsorId))
            {
                throw new KeyNotFoundException(
                    $"No se encontró el patrocinador con ID {sponsorId}");
            }

            return await _tournamentSponsorRepository.GetBySponsorIdAsync(sponsorId);
        }

        public async Task UnlinkFromTournamentAsync(int sponsorId, int tournamentId)
        {
            if (!await _sponsorRepository.ExistsAsync(sponsorId))
            {
                throw new KeyNotFoundException(
                    $"No se encontró el patrocinador con ID {sponsorId}");
            }

            var link = await _tournamentSponsorRepository
                .GetByTournamentAndSponsorAsync(tournamentId, sponsorId);
            if (link == null)
            {
                throw new KeyNotFoundException(
                    "No existe vinculación entre este patrocinador y el torneo indicado");
            }

            _logger.LogInformation(
                "Unlinking sponsor {SponsorId} from tournament {TournamentId}",
                sponsorId, tournamentId);
            await _tournamentSponsorRepository.DeleteAsync(link.Id);
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                var trimmed = email.Trim();
                var addr = new MailAddress(trimmed);
                return string.Equals(addr.Address, trimmed, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}
