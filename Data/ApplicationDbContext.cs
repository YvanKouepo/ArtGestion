using ArtGestion.Helpers;
using ArtGestion.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArtGestion.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Alerte> Alertes => Set<Alerte>();
        public DbSet<LogAction> LogsActions => Set<LogAction>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<Utilisateur> Utilisateurs => Set<Utilisateur>();
        public DbSet<Region> Regions => Set<Region>();
        public DbSet<Ville> Villes => Set<Ville>();
        public DbSet<TypeTitre> TypesTitre => Set<TypeTitre>();
        public DbSet<TypeEntreprise> TypesEntreprise => Set<TypeEntreprise>();
        public DbSet<TypeReseau> TypesReseau => Set<TypeReseau>();
        public DbSet<Exploitant> Exploitants => Set<Exploitant>();
        public DbSet<TitreExploitation> TitresExploitation => Set<TitreExploitation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Region>().HasData(
    new Region { Id = 1, Nom = "Littoral", Code = "LT", Actif = true },
    new Region { Id = 2, Nom = "Sud-Ouest", Code = "SW", Actif = true }
);

modelBuilder.Entity<Ville>().HasData(
    new Ville { Id = 1, Nom = "Douala", RegionId = 1, Actif = true },
    new Ville { Id = 2, Nom = "Buea", RegionId = 2, Actif = true }
);

modelBuilder.Entity<TypeEntreprise>().HasData(
    new TypeEntreprise { Id = 1, Libelle = "Opérateur", Actif = true },
    new TypeEntreprise { Id = 2, Libelle = "Fournisseur", Actif = true }
);

modelBuilder.Entity<Service>().HasData(
    new Service { Id = 1, Code = "SSVTSE", Nom = "Service du Suivi des Services, de la Veille Technologique et des Statistiques Économiques", Actif = true }
);

modelBuilder.Entity<Utilisateur>().HasData(
    new Utilisateur
    {
        Id = 1,
        NomComplet = "Admin SSVTSE",
        Login = "admin",
        MotDePasseHash = "temp-hash",
        Email = "admin@art.local",
        ServiceId = 1,
        Role = "Admin",
        EstResponsableService = true,
        Actif = true,
        DateCreation = DateTime.UtcNow
    }
);

modelBuilder.Entity<TypeTitre>().HasData(
    new TypeTitre
    {
        Id = 1,
        Libelle = "Licence",
        DureeValidite = 5,
        UniteDuree = "Ans",
        Actif = true
    }
);

           modelBuilder.Entity<Exploitant>().HasData(
    new Exploitant
    {
        Id = 1,
        CodeExploitant = "EXP001",
        NomExploitant = "MTN Cameroun",
        RegionId = 1,
        VilleId = 1,
        ServiceId = 1,
        UtilisateurId = 1,
        TypeEntrepriseId = 1
    },
    new Exploitant
    {
        Id = 2,
        CodeExploitant = "EXP002",
        NomExploitant = "Orange Cameroun",
        RegionId = 1,
        VilleId = 1,
        ServiceId = 1,
        UtilisateurId = 1,
        TypeEntrepriseId = 1
    },
    new Exploitant
    {
        Id = 3,
        CodeExploitant = "EXP003",
        NomExploitant = "Nexttel",
        RegionId = 2,
        VilleId = 2,
        ServiceId = 1,
        UtilisateurId = 1,
        TypeEntrepriseId = 2
    }
);

modelBuilder.Entity<TitreExploitation>().HasData(
    new TitreExploitation
    {
        Id = 1,
        ExploitantId = 1,
        TypeTitreId = 1,
        ReferenceTitre = "TITRE-001",
        DateSignature = new DateTime(2025, 10, 1, 0, 0, 0, DateTimeKind.Utc),
        DateExpiration = new DateTime(2026, 10, 1, 0, 0, 0, DateTimeKind.Utc),
        Statut = "Actif",
        NombreTitres = 1,
        ServiceId = 1,
        UtilisateurId = 1,
        Actif = true
    },
    new TitreExploitation
    {
        Id = 2,
        ExploitantId = 2,
        TypeTitreId = 1,
        ReferenceTitre = "TITRE-002",
        DateSignature = new DateTime(2021, 4, 1, 0, 0, 0, DateTimeKind.Utc),
        DateExpiration = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
        Statut = "Bientôt expiré",
        NombreTitres = 2,
        ServiceId = 1,
        UtilisateurId = 1,
        Actif = true
    },
    new TitreExploitation
    {
        Id = 3,
        ExploitantId = 3,
        TypeTitreId = 1,
        ReferenceTitre = "TITRE-003",
        DateSignature = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc),
        DateExpiration = new DateTime(2025, 4, 10, 0, 0, 0, DateTimeKind.Utc),
        Statut = "Expiré",
        NombreTitres = 1,
        ServiceId = 1,
        UtilisateurId = 1,
        Actif = false
    }
);

            modelBuilder.Entity<Alerte>()
                .HasIndex(a => new { a.TitreExploitationId, a.TypeAlerte })
                .IsUnique();
            modelBuilder.Entity<Alerte>()
                .HasOne(a => a.TitreExploitation)
                .WithMany()
                .HasForeignKey(a => a.TitreExploitationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Alerte>()
                .HasOne(a => a.Service)
                .WithMany()
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Alerte>()
                .HasOne(a => a.UtilisateurResponsable)
                .WithMany()
                .HasForeignKey(a => a.UtilisateurResponsableId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LogAction>()
                 .HasOne(l => l.Service)
                 .WithMany()
                 .HasForeignKey(l => l.ServiceId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LogAction>()
                .HasOne(l => l.Utilisateur)
                .WithMany()
                .HasForeignKey(l => l.UtilisateurId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Service>()
                .HasIndex(s => s.Code)
                .IsUnique();

            modelBuilder.Entity<Utilisateur>()
                .HasIndex(u => u.Login)
                .IsUnique();

            modelBuilder.Entity<Exploitant>()
                .HasIndex(e => e.CodeExploitant);

            modelBuilder.Entity<TitreExploitation>()
                .HasIndex(t => t.ReferenceTitre);

            modelBuilder.Entity<Ville>()
                .HasOne(v => v.Region)
                .WithMany(r => r.Villes)
                .HasForeignKey(v => v.RegionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Exploitant>()
                .HasOne(e => e.Region)
                .WithMany(r => r.Exploitants)
                .HasForeignKey(e => e.RegionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Exploitant>()
                .HasOne(e => e.Ville)
                .WithMany(v => v.Exploitants)
                .HasForeignKey(e => e.VilleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Exploitant>()
                .HasOne(e => e.TypeEntreprise)
                .WithMany(t => t.Exploitants)
                .HasForeignKey(e => e.TypeEntrepriseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Exploitant>()
                .HasOne(e => e.Service)
                .WithMany(s => s.Exploitants)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Exploitant>()
                .HasOne(e => e.Utilisateur)
                .WithMany(u => u.ExploitantsCrees)
                .HasForeignKey(e => e.UtilisateurId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TitreExploitation>()
                .HasOne(t => t.Exploitant)
                .WithMany(e => e.TitresExploitation)
                .HasForeignKey(t => t.ExploitantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TitreExploitation>()
                .HasOne(t => t.TypeTitre)
                .WithMany(tt => tt.TitresExploitation)
                .HasForeignKey(t => t.TypeTitreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TitreExploitation>()
                .HasOne(t => t.TypeReseau)
                .WithMany(tr => tr.TitresExploitation)
                .HasForeignKey(t => t.TypeReseauId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TitreExploitation>()
                .HasOne(t => t.Service)
                .WithMany(s => s.TitresExploitation)
                .HasForeignKey(t => t.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TitreExploitation>()
                .HasOne(t => t.Utilisateur)
                .WithMany(u => u.TitresCrees)
                .HasForeignKey(t => t.UtilisateurId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}