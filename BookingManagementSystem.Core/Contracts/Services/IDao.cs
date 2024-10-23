using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Contracts.Services;
public interface IDao
{
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
}
