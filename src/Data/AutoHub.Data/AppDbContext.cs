namespace AutoHub.Data
{
    using System.Reflection;

    using AutoHub.Data.Common.Models;
    using AutoHub.Data.Models;
    using AutoHub.Data.Models.Base;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(AppDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Advert> Adverts { get; set; }

        public DbSet<BodyStyle> BodyStyles { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Condition> Conditions { get; set; }

        public DbSet<Engine> Engines { get; set; }

        public DbSet<EuroStandard> EuroStandards { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<RemoteProvider> RemoteProviders { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Transmission> Transmissions { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Advert>()
                .HasIndex(a => new { a.RemoteProviderId, a.RemoteId })
                .IsUnique();

            builder
                .Entity<Brand>()
                .HasMany(brand => brand.Models)
                .WithOne(model => model.Brand)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Region>()
                .HasMany(region => region.Towns)
                .WithOne(town => town.Region)
                .OnDelete(DeleteBehavior.Restrict);

            RenameDefaultIdentityModels(builder);

            SetUniqueConstraints(
                                builder,
                                typeof(BodyStyle),
                                typeof(Brand),
                                typeof(Color),
                                typeof(Condition),
                                typeof(Engine),
                                typeof(EuroStandard),
                                typeof(Region),
                                typeof(RemoteProvider),
                                typeof(Transmission));

            this.ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletable).IsAssignableFrom(et.ClrType));

            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));

            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletable
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        private static void SetUniqueConstraints(ModelBuilder modelBuilder, params Type[] types)
        {
            foreach (Type type in types)
            {
                if (type.IsSubclassOf(typeof(BaseNameableOneToManyAdvertsModel)))
                {
                    modelBuilder.Entity(type).HasIndex(nameof(BaseNameableOneToManyAdvertsModel.Name)).IsUnique();
                }

                if (type.IsSubclassOf(typeof(BaseTypeableOneToManyAdvertsModel)))
                {
                    modelBuilder.Entity(type).HasIndex(nameof(BaseTypeableOneToManyAdvertsModel.Type)).IsUnique();
                }
            }
        }

        private static void RenameDefaultIdentityModels(ModelBuilder builder)
        {
            builder.Entity<AppUser>(entity => entity.ToTable("Users"));

            builder.Entity<AppRole>(entity => entity.ToTable("Roles"));

            builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable("UserRoles"));

            builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable("UserClaims"));

            builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable("UserLogins"));

            builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable("RoleClaims"));

            builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("UserTokens"));
        }

        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditable &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditable)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
