using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var host = _configuration["ConnectionStrings:Host"];
            var port = _configuration["ConnectionStrings:Port"];
            var username = _configuration["ConnectionStrings:Username"];
            var password = _configuration["ConnectionStrings:Password"];
            var database = _configuration["ConnectionStrings:Database"];

            var connectionString = $"Host={host};Port={port};Username={username};Password={password};Database={database}";
            optionsBuilder.UseNpgsql(connectionString); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.Entity<Product>(
                    entity =>
                    {
                        entity.ToTable("products");

                        entity.HasKey(e => e.Id);
                        entity.HasIndex(e => e.Name).IsUnique();

                        entity.Property(e => e.Id)
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()")
                        .ValueGeneratedOnAdd();

                        entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(200);

                        entity.Property(e => e.Price)
                        .IsRequired()
                        .HasColumnType("decimal(10, 2)");

                        entity.Property(e => e.TVA)
                        .IsRequired()
                        .HasColumnType("decimal(5, 2)");
                    }
                );
        }
    }
}
