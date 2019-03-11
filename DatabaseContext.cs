using LockServerAPI.Models.Codes;
using LockServerAPI.Models.Locks;
using Microsoft.EntityFrameworkCore;

namespace LockServerAPI
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Codes> Codes { get; set; }
        public virtual DbSet<Locks> Locks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack")
                .HasPostgresExtension("pgcrypto");

            modelBuilder.Entity<Codes>(entity =>
            {
                entity.ToTable("codes", "referencedata");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code");

                entity.Property(e => e.LockId)
                    .IsRequired()
                    .HasColumnName("lock_id");
            });

            modelBuilder.Entity<Locks>(entity =>
            {
                entity.ToTable("locks", "referencedata");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DeviceId)
                    .IsRequired()
                    .HasColumnName("device_id");
            });
        }
    }
}
