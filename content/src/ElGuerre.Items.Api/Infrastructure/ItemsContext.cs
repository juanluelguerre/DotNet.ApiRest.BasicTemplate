using ElGuerre.Items.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ElGuerre.Items.Api.Infrastructure
{
    public class ItemsContext : DbContext
    {
        public ItemsContext() : base()
        {
        }

        public ItemsContext(DbContextOptions<ItemsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Shadows Properties to Audit columns: https://docs.microsoft.com/en-us/ef/core/modeling/shadow-properties
            // Set Shadows Properties for all entities (tables) in the DB Model.
            modelBuilder.Model.GetEntityTypes().ToList()
               .ForEach(entityType =>
               {
                   modelBuilder.Entity(entityType.ClrType).Property("Id").UseHiLo(string.Format("{0}SequenceHiLo", entityType.ClrType.Name));
                   modelBuilder.Entity(entityType.ClrType).Property<DateTime?>("LastUpdated");
                   modelBuilder.Entity(entityType.ClrType).Property<DateTime>("CreationDate");
               });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Startup).Assembly);
        }

        public virtual DbSet<ItemEntity> Items { get; set; }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            System.Collections.Generic.IEnumerable<EntityEntry> modifiedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (EntityEntry entry in modifiedEntries)
            {
                // Working with Audit (shadows properties).
                if (entry.State == EntityState.Modified || entry.State == EntityState.Added)
                {
                    entry.Property("LastUpdated").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreationDate").CurrentValue = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }

    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<ItemsContext>
    {
        public ItemsContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddUserSecrets<Startup>()
               .AddJsonFile("appsettings.json")
               .Build();

            DbContextOptionsBuilder<ItemsContext> builder = new DbContextOptionsBuilder<ItemsContext>();

            builder.UseSqlServer(configuration.GetConnectionString($"{Program.AppName}:{nameof(AppSettings.DBConnectionString)}"));

            return new ItemsContext(builder.Options);
        }
    }
}