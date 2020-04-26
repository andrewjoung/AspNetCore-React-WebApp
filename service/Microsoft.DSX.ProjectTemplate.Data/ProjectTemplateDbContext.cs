using Microsoft.DSX.ProjectTemplate.Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Data
{
    public class ProjectTemplateDbContext : DbContext
    {
        public ProjectTemplateDbContext(DbContextOptions<ProjectTemplateDbContext> options)
            : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            SetupAuditTrail();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetupAuditTrail();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            SetupAuditTrail();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            SetupAuditTrail();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureIndexes(modelBuilder);

            ConfigureRelationships(modelBuilder);

            ConfigurePropertyConversion(modelBuilder);

            ConfigureSeedData(modelBuilder);
        }

        private void SetupAuditTrail()
        {
            // automatically stamp date fields on every context save
            var dtNow = DateTime.Now;
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.Entity is AuditModel<int>)
                {
                    var entity = entry.Entity as AuditModel<int>;
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedDate = entity.UpdatedDate = dtNow;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entity.UpdatedDate = dtNow;
                    }
                }
            }
        }

        private void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Library>()
                .OwnsOne(lib => lib.Address);
        }

        private void ConfigurePropertyConversion(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(b => b.Metadata)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v));
        }

        private void ConfigureSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasData(
                    new Group()
                    {
                        Id = 1,
                        Name = "Surface",
                        IsActive = true
                    },
                    new Group()
                    {
                        Id = 2,
                        Name = "HoloLens",
                        IsActive = true
                    },
                    new Group()
                    {
                        Id = 3,
                        Name = "Xbox",
                        IsActive = true
                    }
                );

            // Seed database with new entity
            modelBuilder.Entity<Library>(b =>
            {
                b.HasData(
                    new Library()
                    {
                        Id = 1,
                        Name = "Bellevue Library"
                    }
                );
                b.OwnsOne(e => e.Address).HasData(new
                {
                    LibraryId = 1,
                    LocationAddressLine1 = "1111 110th Ave NE",
                    LocationStateProvince = "WA",
                    LocationCity = "Bellevue",
                    LocationZipCode = "98004",
                    LocationCountry = "US"
                });
            });

            // Seed database with new entity
            modelBuilder.Entity<Library>(b =>
            {
                b.HasData(
                    new Library()
                    {
                        Id = 2,
                        Name = "Newcastle Library"
                    }
                );
                b.OwnsOne(e => e.Address).HasData(new
                {
                    LibraryId = 2,
                    LocationAddressLine1 = "12901 Newcastle Way",
                    LocationStateProvince = "WA",
                    LocationCity = "Newcastle",
                    LocationZipCode = "98056",
                    LocationCountry = "US"
                });
            });
        }

        private void ConfigureIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Project>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Library>()
                .HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
