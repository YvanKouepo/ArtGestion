using ArtGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtGestion.Data
{
    public class ApplicationDbContext : DbContext
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