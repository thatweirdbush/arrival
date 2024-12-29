using BookingManagementSystem.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BookingManagementSystem.Core.Models;
public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options) { }

    // Core Models
    public virtual DbSet<Amenity> Amenities { get; set; }
    public virtual DbSet<BadReport> BadReports { get; set; }
    public virtual DbSet<Booking> Bookings { get; set; }
    public virtual DbSet<CountryInfo> CountryInfo { get; set; }
    public virtual DbSet<FAQ> FAQs { get; set; }
    public virtual DbSet<Notification> Notifications { get; set; }
    public virtual DbSet<Payment> Payments { get; set; }
    public virtual DbSet<Property> Properties { get; set; }
    public virtual DbSet<PropertyAmenity> PropertyAmenities { get; set; }
    public virtual DbSet<PropertyPolicy> PropertyPolicies { get; set; }
    public virtual DbSet<QnA> QnAs { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Amenity>(entity =>
        {
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Type).HasDefaultValue(AmenityType.Other);
        });

        modelBuilder.Entity<BadReport>(entity =>
        {
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.HandledByAdmin).WithMany(p => p.BadReportHandledByAdmins).HasForeignKey(d => d.HandledByAdminId);

            entity.HasOne(d => d.User).WithMany(p => p.BadReportUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Status).HasDefaultValue(BookingStatus.Pending);

            entity.HasOne(d => d.Property).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<CountryInfo>(entity =>
        {
            entity.ToTable("CountryInfo");
        });

        modelBuilder.Entity<FAQ>(entity =>
        {
            entity.ToTable("FAQs");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.FAQCategory).HasColumnName("FAQCategory");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Status).HasDefaultValue(PaymentStatus.Pending);

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasIndex(e => e.CountryId, "IX_Properties_CountryId");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.IsFavourite).HasDefaultValue(false);
            entity.Property(e => e.IsPetFriendly).HasDefaultValue(false);
            entity.Property(e => e.IsPriority).HasDefaultValue(false);
            entity.Property(e => e.IsRequested).HasDefaultValue(false);
            entity.Property(e => e.LastEditedStep).HasDefaultValueSql("'-1'::integer");
            entity.Property(e => e.MaxGuests).HasDefaultValue(1);
            entity.Property(e => e.Status).HasDefaultValue(PropertyStatus.Listed);

            entity.HasOne(d => d.Country).WithMany(p => p.Properties).HasForeignKey(d => d.CountryId);

            entity.HasOne(d => d.Host).WithMany(p => p.Properties)
                .HasForeignKey(d => d.HostId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<PropertyAmenity>(entity =>
        {
            entity.HasKey(e => new { e.PropertyId, e.AmenityId }).HasName("PropertyAmenities_pkey");

            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.Amenity).WithMany(p => p.PropertyAmenities)
                .HasForeignKey(d => d.AmenityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Property).WithMany(p => p.PropertyAmenities)
                .HasForeignKey(d => d.PropertyId);
        });

        modelBuilder.Entity<PropertyPolicy>(entity =>
        {
            entity.HasIndex(e => e.PropertyId, "IX_PropertyPolicies_PropertyId");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Property).WithMany(p => p.PropertyPolicies).HasForeignKey(d => d.PropertyId);
        });

        modelBuilder.Entity<QnA>(entity =>
        {
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Customer).WithMany(p => p.QnACustomers)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Host).WithMany(p => p.QnAHosts).HasForeignKey(d => d.HostId);

            entity.HasOne(d => d.Property).WithMany(p => p.QnAs)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Property).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.IsEliteHost).HasDefaultValue(false);
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

/// <summary>
/// A design-time factory can be especially useful if you need to configure the DbContext differently for design time than at run time.
/// Use when Entity Framework needs to create a DbContext at design time without starting the entire application.
/// </summary>
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Load configuration from appsettings.json or User Secrets
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets<ApplicationDbContextFactory>()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
