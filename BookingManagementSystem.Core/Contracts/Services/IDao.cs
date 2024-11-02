using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Contracts.Services;
public interface IDao
{
    // Read
    Task<IEnumerable<Amenity>> GetAmenityListDataAsync();
    Task<IEnumerable<Booking>> GetBookingListDataAsync();
    Task<IEnumerable<FAQ>> GetFAQListDataAsync();
    Task<IEnumerable<Notification>> GetNotificationListDataAsync();
    Task<IEnumerable<Payment>> GetPaymentListDataAsync();
    Task<IEnumerable<Property>> GetPropertyListDataAsync();
    Task<IEnumerable<PropertyPolicy>> GetPropertyPolicyListDataAsync();
    Task<IEnumerable<QnA>> GetQnAListDataAsync();
    Task<IEnumerable<Review>> GetReviewListDataAsync();
    Task<IEnumerable<User>> GetUserListDataAsync();
    Task<IEnumerable<DestinationTypeSymbol>> GetDestinationTypeSymbolDataAsync();
    Task<IEnumerable<BadReport>> GetBadReportListDataAsync();
    Task<IEnumerable<Voucher>> GetVoucherListDataAsync();

    // Create
    Task AddAmenityAsync(Amenity amenity);
    Task AddBookingAsync(Booking booking);
    Task AddFAQAsyce(FAQ faq);
    Task AddNotificationAsync(Notification notification);
    Task AddPaymentAsync(Payment payment);
    Task AddPropertyAsync(Property property);
    Task AddPropertyPolicyAsync(PropertyPolicy propertyPolicy);
    Task AddQnAAsync(QnA qna);
    Task AddReviewAsync(Review review);
    Task AddUserAsync(User user);
    Task AddDestinationTypeSymbolAsync(DestinationTypeSymbol destinationTypeSymbol);
    Task AddBadReportAsync(BadReport badReport);
    Task AddVoucherAsync(Voucher voucher);
}
