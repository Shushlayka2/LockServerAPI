using LockServerAPI.Models.Code;
using LockServerAPI.Models.Lock;
using LockServerAPI.Models.User;
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

        public virtual DbSet<Code> Codes { get; set; }
        public virtual DbSet<Lock> Locks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack")
                .HasPostgresExtension("pgcrypto");

            modelBuilder.Entity<Code>(entity =>
            {
                entity.ToTable("codes", "referencedata");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CodeVal)
                    .IsRequired()
                    .HasColumnName("code");

                entity.Property(e => e.LockId)
                    .IsRequired()
                    .HasColumnName("lock_id");

                entity.Property(e => e.Config)
                    .IsRequired()
                    .HasColumnName("config");
            });

            modelBuilder.Entity<Lock>(entity =>
            {
                entity.ToTable("locks", "referencedata");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DeviceId)
                    .IsRequired()
                    .HasColumnName("device_id");

                entity.Property(e => e.Config)
                    .IsRequired()
                    .HasColumnName("config");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users", "referencedata");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");
            });
        }
    }
}
