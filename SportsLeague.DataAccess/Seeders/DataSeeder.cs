using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;

namespace SportsLeague.DataAccess.Seeders
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(LeagueDbContext context)
        {
            if (await context.Teams.AnyAsync())
                return;

            var teams = new List<Team>
            {
                new() { Name = "Atlético Nacional", City = "Medellín", Stadium = "Atanasio Girardot" },
                new() { Name = "Independiente Medellín", City = "Medellín", Stadium = "Atanasio Girardot" },
                new() { Name = "América de Cali", City = "Cali", Stadium = "Pascual Guerrero" },
                new() { Name = "Deportivo Cali", City = "Cali", Stadium = "Deportivo Cali" },
                new() { Name = "Junior FC", City = "Barranquilla", Stadium = "Metropolitano" },
                new() { Name = "Millonarios FC", City = "Bogotá", Stadium = "El Campín" },
                new() { Name = "Independiente Santa Fe", City = "Bogotá", Stadium = "El Campín" },
                new() { Name = "Deportes Tolima", City = "Ibagué", Stadium = "Manuel Murillo Toro" },
                new() { Name = "Atlético Bucaramanga", City = "Bucaramanga", Stadium = "Alfonso López" },
                new() { Name = "Once Caldas", City = "Manizales", Stadium = "Palogrande" },
                new() { Name = "Deportivo Pasto", City = "Pasto", Stadium = "Departamental Libertad" },
                new() { Name = "Deportivo Pereira", City = "Pereira", Stadium = "Hernán Ramírez Villegas" },
                new() { Name = "Águilas Doradas", City = "Rionegro", Stadium = "Alberto Grisales" },
                new() { Name = "Boyacá Chicó FC", City = "Tunja", Stadium = "La Independencia" },
                new() { Name = "Jaguares de Córdoba", City = "Montería", Stadium = "Jaraguay" },
                new() { Name = "Alianza Valledupar FC", City = "Valledupar", Stadium = "Armando Maestre" },
                new() { Name = "Fortaleza FC", City = "Bogotá", Stadium = "Metropolitano de Techo" },
                new() { Name = "Llaneros FC", City = "Villavicencio", Stadium = "Bello Horizonte" },
                new() { Name = "Cúcuta Deportivo", City = "Cúcuta", Stadium = "General Santander" },
                new() { Name = "Internacional de Bogotá", City = "Bogotá", Stadium = "Metropolitano de Techo" },
            };

            context.Teams.AddRange(teams);
            await context.SaveChangesAsync();

            var playersData = new (string First, string Last, PlayerPosition Pos, int Number)[][]
            {
                new[]
                {
                    ("David", "Ospina", PlayerPosition.Goalkeeper, 1),
                    ("William", "Tesillo", PlayerPosition.Defender, 3),
                    ("Edwin", "Cardona", PlayerPosition.Midfielder, 10),
                    ("Alfredo", "Morelos", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("Salvador", "Ichazo", PlayerPosition.Goalkeeper, 1),
                    ("Andrés", "Cadavid", PlayerPosition.Defender, 4),
                    ("Adrián", "Arregui", PlayerPosition.Midfielder, 5),
                    ("Luciano", "Pons", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("Joel", "Graterol", PlayerPosition.Goalkeeper, 1),
                    ("Jorge", "Segura", PlayerPosition.Defender, 3),
                    ("Rodrigo", "Ureña", PlayerPosition.Midfielder, 8),
                    ("Adrián", "Ramos", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("Pedro", "Gallese", PlayerPosition.Goalkeeper, 1),
                    ("Fernando", "Álvarez", PlayerPosition.Defender, 4),
                    ("Kevin", "Velasco", PlayerPosition.Midfielder, 10),
                    ("Juan", "Dinenno", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("Mauro", "Silveira", PlayerPosition.Goalkeeper, 1),
                    ("Edwin", "Herrera", PlayerPosition.Defender, 4),
                    ("Fabián", "Ángel", PlayerPosition.Midfielder, 8),
                    ("Carlos", "Bacca", PlayerPosition.Forward, 7),
                },
                new[]
                {
                    ("Guillermo", "De Amores", PlayerPosition.Goalkeeper, 1),
                    ("Omar", "Bertel", PlayerPosition.Defender, 4),
                    ("Daniel", "Cataño", PlayerPosition.Midfielder, 10),
                    ("Leonardo", "Castro", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("Leandro", "Castellanos", PlayerPosition.Goalkeeper, 1),
                    ("Elvis", "Mosquera", PlayerPosition.Defender, 3),
                    ("Daniel", "Giraldo", PlayerPosition.Midfielder, 5),
                    ("Hugo", "Rodallega", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("William", "Cuesta", PlayerPosition.Goalkeeper, 1),
                    ("Jersson", "González", PlayerPosition.Defender, 3),
                    ("Junior", "Hernández", PlayerPosition.Midfielder, 10),
                    ("Tatay", "Torres", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("Juan Camilo", "Chaverra", PlayerPosition.Goalkeeper, 1),
                    ("José", "Ortiz", PlayerPosition.Defender, 4),
                    ("Sherman", "Cárdenas", PlayerPosition.Midfielder, 10),
                    ("Sebastián", "Pons", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("Gerardo", "Ortiz", PlayerPosition.Goalkeeper, 1),
                    ("Edisson", "Palomino", PlayerPosition.Defender, 3),
                    ("Sebastián", "Gómez", PlayerPosition.Midfielder, 5),
                    ("Dayro", "Moreno", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("Diego", "Martínez", PlayerPosition.Goalkeeper, 1),
                    ("Camilo", "Ayala", PlayerPosition.Defender, 4),
                    ("Ray", "Vanegas", PlayerPosition.Midfielder, 10),
                    ("Jown", "Cardona", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("Harlen", "Castillo", PlayerPosition.Goalkeeper, 1),
                    ("David", "González", PlayerPosition.Defender, 3),
                    ("Brayan", "León", PlayerPosition.Midfielder, 8),
                    ("Jonier", "Mosquera", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("José Fernando", "Cuadrado", PlayerPosition.Goalkeeper, 1),
                    ("Éder", "Chaux", PlayerPosition.Defender, 4),
                    ("Juan Pablo", "Ramírez", PlayerPosition.Midfielder, 10),
                    ("Cristian", "Subero", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("Ernesto", "Hernández", PlayerPosition.Goalkeeper, 1),
                    ("Carlos", "Henao", PlayerPosition.Defender, 3),
                    ("Brayan", "Moreno", PlayerPosition.Midfielder, 8),
                    ("Juan David", "Valencia", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("Diego", "Novoa", PlayerPosition.Goalkeeper, 1),
                    ("Geovan", "Montes", PlayerPosition.Defender, 4),
                    ("Larry", "Vásquez", PlayerPosition.Midfielder, 5),
                    ("Pablo", "Bueno", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("Luis", "Delgado", PlayerPosition.Goalkeeper, 1),
                    ("Marvin", "Vallecilla", PlayerPosition.Defender, 3),
                    ("Juan", "Sánchez", PlayerPosition.Midfielder, 8),
                    ("Jeison", "Medina", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("Carlos", "Mosquera", PlayerPosition.Goalkeeper, 1),
                    ("Nicolás", "Giraldo", PlayerPosition.Defender, 4),
                    ("Jhonier", "Viveros", PlayerPosition.Midfielder, 10),
                    ("Óscar", "Vanegas", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("José Huber", "Escobar", PlayerPosition.Goalkeeper, 1),
                    ("Cristian", "Arrieta", PlayerPosition.Defender, 3),
                    ("Jhon", "Pajoy", PlayerPosition.Midfielder, 8),
                    ("Brayan", "Gil", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("Norberto", "Araujo", PlayerPosition.Goalkeeper, 1),
                    ("Jefry", "Díaz", PlayerPosition.Defender, 4),
                    ("Juan Camilo", "Portilla", PlayerPosition.Midfielder, 10),
                    ("Edwar", "López", PlayerPosition.Forward, 9),
                },
                new[]
                {
                    ("Neto", "Volpi", PlayerPosition.Goalkeeper, 1),
                    ("Nicolás", "Hernández", PlayerPosition.Defender, 3),
                    ("Carlos Darwin", "Quintero", PlayerPosition.Midfielder, 10),
                    ("Facundo", "Boné", PlayerPosition.Forward, 9),
                },
            };

            var players = new List<Player>();
            for (int i = 0; i < teams.Count; i++)
            {
                foreach (var pd in playersData[i])
                {
                    players.Add(new Player
                    {
                        FirstName = pd.First,
                        LastName = pd.Last,
                        Number = pd.Number,
                        Position = pd.Pos,
                        BirthDate = new DateTime(1995, 1, 1).AddMonths(players.Count),
                        TeamId = teams[i].Id
                    });
                }
            }

            context.Players.AddRange(players);
            await context.SaveChangesAsync();

            var referees = new List<Referee>
            {
                new() { FirstName = "Wilmar", LastName = "Roldán", Nationality = "Colombia" },
                new() { FirstName = "Andrés", LastName = "Rojas", Nationality = "Colombia" },
                new() { FirstName = "Carlos", LastName = "Betancur", Nationality = "Colombia" },
                new() { FirstName = "Jhon", LastName = "Hinestroza", Nationality = "Colombia" },
            };

            context.Referees.AddRange(referees);
            await context.SaveChangesAsync();

            var tournament = new Tournament
            {
                Name = "Liga BetPlay 2026-I",
                Season = "2026-I",
                StartDate = new DateTime(2026, 1, 16),
                EndDate = new DateTime(2026, 6, 5),
                Status = TournamentStatus.InProgress
            };

            context.Tournaments.Add(tournament);
            await context.SaveChangesAsync();

            foreach (var team in teams)
            {
                context.TournamentTeams.Add(new TournamentTeam
                {
                    TournamentId = tournament.Id,
                    TeamId = team.Id
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
