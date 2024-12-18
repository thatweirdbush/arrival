using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BookingManagementSystem.Core.Models;
public class ApplicationDbContext : DbContext
{
    // Core Models
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<BadReport> BadReports { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<CountryInfo> CountryInfo { get; set; }
    public DbSet<FAQ> FAQs { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyPolicy> PropertyPolicies { get; set; }
    public DbSet<QnA> QnAs { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }
   
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options) {}
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
