using Microsoft.EntityFrameworkCore;
using zIndraTechnicalChallenge.Domain.MainContext.Aggregates.ProductAgg;
using zIndraTechnicalChallenge.Domain.MainContext.SeedWork;
using zIndraTechnicalChallenge.Infrastructure.Data.Context.Converters;

namespace zIndraTechnicalChallenge.Infrastructure.Data.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        #region IDbSet Members
        public virtual DbSet<Product> Products { get; set; }
        #endregion

        #region Model Creating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetNotDeleteCascade(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");
            builder.Properties<TimeOnly>()
                .HaveConversion<TimeOnlyConverter>()
                .HaveColumnType("time");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ProcessSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ProcessSave()
        {
            var dateNow = DateTime.Now;
            foreach (var item in ChangeTracker.Entries().Where(x => x.State == EntityState.Added && x.Entity is Entity))
            {
                var entity = item.Entity as Entity;
                entity.RegisterDate = dateNow;
                entity.UpdateDate = dateNow;
            }
            foreach (var item in ChangeTracker.Entries().Where(x => x.State == EntityState.Modified && x.Entity is Entity))
            {
                var entity = item.Entity as Entity;
                entity.UpdateDate = dateNow;
                item.Property(nameof(entity.RegisterDate)).IsModified = false;
            }
        }

        private static void SetNotDeleteCascade(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.ClientCascade;
        }
        #endregion
    }
}
