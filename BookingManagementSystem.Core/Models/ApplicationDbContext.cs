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

    public async Task AddUsersDB()
    {
        var userRepository = new UserRepository();
        var users = await userRepository.GetAllAsync();
        await Users.AddRangeAsync(users);
        await SaveChangesAsync();
    }

    public async Task AddPropertyDB()
    {
        var propertyRepository = new PropertyRepository();
        var properties = await propertyRepository.GetAllAsync();
        await Properties.AddRangeAsync(properties);
        await SaveChangesAsync();
    }

    public async Task AddQnADB()
    {
        var qnaRepository = new QnARepository();
        var qnas = await qnaRepository.GetAllAsync();
        await QnAs.AddRangeAsync(qnas);
        await SaveChangesAsync();
    }

    public async Task AddReviewDB()
    {
        var reviewRepository = new ReviewRepository();
        var reviews = await reviewRepository.GetAllAsync();
        await Reviews.AddRangeAsync(reviews);
        await SaveChangesAsync();
    }

    public async Task AddPropertyPolicyDB()
    {
        var policyRepository = new PropertyPolicyRepository();
        var propertyPolicies = await policyRepository.GetAllAsync();
        await PropertyPolicies.AddRangeAsync(propertyPolicies);
        await SaveChangesAsync();
    }

    public async Task AddVoucherDB()
    {
        var voucherRepository = new VoucherRepository();
        var vouchers = await voucherRepository.GetAllAsync();
        await Vouchers.AddRangeAsync(vouchers);
        await SaveChangesAsync();
    }

    public async Task AddFAQDB()
    {
        var faqRepository = new FAQRepository();
        var faqs = await faqRepository.GetAllAsync();
        await FAQs.AddRangeAsync(faqs);
        await SaveChangesAsync();
    }

    public async Task AddAmenityDB()
    {
        var amenityRepository = new AmenityRepository();
        var amenities = await amenityRepository.GetAllAsync();
        await Amenities.AddRangeAsync(amenities);
        await SaveChangesAsync();
    }

    public async Task AddBadReportDB()
    {
        var badReportRepository = new BadReportRepository();
        var badreports = await badReportRepository.GetAllAsync();
        await BadReports.AddRangeAsync(badreports);
        await SaveChangesAsync();
    }

    public async Task AddBookingDB()
    {
        var bookingRepository = new BookingRepository();   
        var bookings = await bookingRepository.GetAllAsync();
        await Bookings.AddRangeAsync(bookings);
        await SaveChangesAsync();
    }

    public async Task AddNotificationDB()
    {
        var notificationRepository = new NotificationRepository();
        var notifications = await notificationRepository.GetAllAsync();
        await Notifications.AddRangeAsync(notifications);
        await SaveChangesAsync();
    }

    public async Task AddPaymentDB()
    {
        var paymentRepository = new PaymentRepository();
        var payments = await paymentRepository.GetAllAsync();
        await Payments.AddRangeAsync(payments);
        await SaveChangesAsync();
    }
   
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
