using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingManagementSystem.Core.Models;
public class ApplicationDbContext : DbContext
{
    // Core Models
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<BadReport> BadReports { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<FAQ> FAQs { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyPolicy> PropertyPolicies { get; set; }
    public DbSet<QnA> QnAs { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        var connectionString = "YOUR_CONNECTION_STRING";
        optionsBuilder.UseNpgsql(connectionString);
    }
}
