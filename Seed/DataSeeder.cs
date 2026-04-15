using ArtGestion.Data;
using ArtGestion.Models;

namespace ArtGestion.Seed
{
    public static class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // =========================
            // REGIONS
            // =========================
            if (!context.Regions.Any())
            {
                var regions = new List<Region>
                {
                    new Region { Nom = "Littoral", Code = "LT" },
                    new Region { Nom = "Sud-Ouest", Code = "SW" }
                };

                context.Regions.AddRange(regions);
                context.SaveChanges();
            }

            // =========================
            // VILLES
            // =========================
            if (!context.Villes.Any())
            {
                var littoral = context.Regions.First(r => r.Nom == "Littoral");
                var sudOuest = context.Regions.First(r => r.Nom == "Sud-Ouest");

                var villes = new List<Ville>
                {
                    // Littoral
                    new Ville { Nom = "Douala", RegionId = littoral.Id },
                    new Ville { Nom = "Nkongsamba", RegionId = littoral.Id },
                    new Ville { Nom = "Edéa", RegionId = littoral.Id },

                    // Sud-Ouest
                    new Ville { Nom = "Buea", RegionId = sudOuest.Id },
                    new Ville { Nom = "Limbe", RegionId = sudOuest.Id },
                    new Ville { Nom = "Kumba", RegionId = sudOuest.Id }
                };

                context.Villes.AddRange(villes);
                context.SaveChanges();
            }

            // =========================
            // TYPES ENTREPRISE
            // =========================
            if (!context.TypesEntreprise.Any())
            {
                context.TypesEntreprise.AddRange(
                    new TypeEntreprise { Libelle = "Personne morale" },
                    new TypeEntreprise { Libelle = "Personne physique" },
                    new TypeEntreprise { Libelle = "Société publique" },
                    new TypeEntreprise { Libelle = "Société privée" }
                );
                context.SaveChanges();
            }

            // =========================
            // TYPES RESEAU
            // =========================
            if (!context.TypesReseau.Any())
            {
                context.TypesReseau.AddRange(
                    new TypeReseau { Libelle = "Mobile" },
                    new TypeReseau { Libelle = "Fixe" },
                    new TypeReseau { Libelle = "Internet" },
                    new TypeReseau { Libelle = "Fibre" },
                    new TypeReseau { Libelle = "Radio" }
                );
                context.SaveChanges();
            }

            // =========================
            // TYPES TITRE (IMPORTANT)
            // =========================
            if (!context.TypesTitre.Any())
            {
                context.TypesTitre.AddRange(
                    new TypeTitre { Libelle = "Licence", DureeValidite = 5, UniteDuree = "Ans" },
                    new TypeTitre { Libelle = "Autorisation", DureeValidite = 2, UniteDuree = "Ans" },
                    new TypeTitre { Libelle = "Agrément", DureeValidite = 3, UniteDuree = "Ans" }
                );
                context.SaveChanges();
            }

            // =========================
            // SERVICES (ART)
            // =========================
            if (!context.Services.Any())
            {
                context.Services.AddRange(
                    new Service { Code = "SAT", Nom = "Service d'appui technique" },
                    new Service { Code = "SSVTSE", Nom = "Service des Statistiques, de la Veille Technologique et de la Sécurité éléctronique" },
                    new Service { Code = "CCF", Nom = "Centre de Contrôle des Fréquences" }
                );
                context.SaveChanges();
            }
        }
    }
}