using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels;

public partial class FAQViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IRepository<FAQ> _faqRepository;

    // List of content items
    //public IEnumerable<FAQ> FAQs { get; set; } = Enumerable.Empty<FAQ>();   // All
    public IEnumerable<FAQ> GeneralFAQs { get; set; } = Enumerable.Empty<FAQ>();   // General
    public IEnumerable<FAQ> BookingFAQs { get; set; } = Enumerable.Empty<FAQ>();    // Booking
    public IEnumerable<FAQ> PaymentFAQs { get; set; } = Enumerable.Empty<FAQ>();    // Payment
    public IEnumerable<FAQ> PricingFAQs { get; set; } = Enumerable.Empty<FAQ>();    // Pricing
    public IEnumerable<FAQ> CancellationsFAQs { get; set; } = Enumerable.Empty<FAQ>();  // Cancellations
    public IEnumerable<FAQ> PropertyPoliciesFAQs { get; set; } = Enumerable.Empty<FAQ>();   // PropertyPolicies

    public FAQViewModel(INavigationService navigationService, IRepository<FAQ> faqRepository)
    {
        _navigationService = navigationService;
        _faqRepository = faqRepository;
        OnNavigatedTo(0);
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Load FAQs data - All
        var faqs = await _faqRepository.GetAllAsync();

        // Load FAQs data - General
        GeneralFAQs = faqs.Where(f => f.FAQCategory == FAQCategory.General);

        // Load FAQs data - Booking
        BookingFAQs = faqs.Where(f => f.FAQCategory == FAQCategory.Booking);

        // Load FAQs data - Payment
        PaymentFAQs = faqs.Where(f => f.FAQCategory == FAQCategory.Payment);

        // Load FAQs data - Pricing
        PricingFAQs = faqs.Where(f => f.FAQCategory == FAQCategory.Pricing);

        // Load FAQs data - Cancellations
        CancellationsFAQs = faqs.Where(f => f.FAQCategory == FAQCategory.Cancellations);

        // Load FAQs data - PropertyPolicies
        PropertyPoliciesFAQs = faqs.Where(f => f.FAQCategory == FAQCategory.PropertyPolicies);
    }

    public void OnNavigatedFrom()
    {
    }
}
