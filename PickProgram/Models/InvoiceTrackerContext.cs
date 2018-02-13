using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PickProgram.Models
{
    public partial class InvoiceTrackerContext : DbContext
    {
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceStatus> InvoiceStatus { get; set; }
        public virtual DbSet<PickLocation> PickLocation { get; set; }

        public InvoiceTrackerContext(DbContextOptions<InvoiceTrackerContext> options)
: base(options)
        { }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(local);Database=InvoiceTracker;Trusted_Connection=True;");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.InvoiceNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AssignedEmployee)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.AssignedEmployeeId)
                    .HasConstraintName("FK_Invoice_Employee");

                entity.HasOne(d => d.PickLocation)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.PickLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_PickLocation");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_InvoiceStatus");
            });

            modelBuilder.Entity<InvoiceStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PickLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.Property(e => e.LocationDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
