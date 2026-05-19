using Entities;
using Microsoft.EntityFrameworkCore;
namespace DataLayer
{
    public class WorkspaceDbContext : DbContext
    {
        public WorkspaceDbContext(DbContextOptions<WorkspaceDbContext> options) : base(options) { }

        public DbSet<WorkRequest> WorkRequests { get; set; }
        public DbSet<Note> Notes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // WorkRequests
            modelBuilder.Entity<WorkRequest>(entity =>
            {
                entity.ToTable("WorkRequests");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWID()");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .HasColumnType("NVARCHAR(MAX)");

                entity.Property(e => e.Priority)
                    .IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired();

                entity.Property(e => e.DueDate);

                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.UpdatedDate)
                    .HasDefaultValueSql("GETUTCDATE()");

                // Indexes
                entity.HasIndex(e => e.Status)
                    .HasDatabaseName("IX_WorkRequests_Status");

                entity.HasIndex(e => e.ClientName)
                    .HasDatabaseName("IX_WorkRequests_ClientName_Title");

                // Relationship
                entity.HasMany(e => e.Notes)
                    .WithOne(n => n.WorkRequest)
                    .HasForeignKey(n => n.WorkRequestId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Notes
            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("Notes");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWID()");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("NVARCHAR(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("GETUTCDATE()");
            });
        }
    }
}

