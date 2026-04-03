namespace SportsLeague.Domain.Entities
{
    public class TournamentTeam : AuditBase
    {
        public int TournamentId { get; set; } //FK
        public int TeamId { get; set; } //FK
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow; //Fecha que se registra un equipo a un torneo

        // Navigation Properties
        public Tournament Tournament { get; set; } = null!;
        public Team Team { get; set; } = null!;
    }
}