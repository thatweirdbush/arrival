using System.Collections.ObjectModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Client;

public partial class FAQViewModel : ObservableRecipient, INavigationAware
{
    private readonly IRepository<FAQ> _faqRepository;

    // List of content items
    public ObservableCollection<FAQ> GeneralFAQs { get; set; } = [];   // General
    public ObservableCollection<FAQ> BookingFAQs { get; set; } = [];    // Booking
    public ObservableCollection<FAQ> PaymentFAQs { get; set; } = [];    // Payment
    public ObservableCollection<FAQ> PricingFAQs { get; set; } = [];    // Pricing
    public ObservableCollection<FAQ> CancellationsFAQs { get; set; } = [];  // Cancellations
    public ObservableCollection<FAQ> PropertyPoliciesFAQs { get; set; } = [];   // PropertyPolicies

    public FAQViewModel(IRepository<FAQ> faqRepository)
    {
        _faqRepository = faqRepository;
    }

    public void OnNavigatedTo(object parameter)
    {
        _ = LoadFAQDataLists();
    }

    public async Task LoadFAQDataLists()
    {
        // Load FAQs data - All
        var faqs = await _faqRepository.GetAllAsync();
        foreach (var faq in faqs.Where(f => f.FAQCategory == FAQCategory.General))
        {
            GeneralFAQs.Add(faq);
        }
        foreach (var faq in faqs.Where(f => f.FAQCategory == FAQCategory.Booking))
        {
            BookingFAQs.Add(faq);
        }
        foreach (var faq in faqs.Where(f => f.FAQCategory == FAQCategory.Payment))
        {
            PaymentFAQs.Add(faq);
        }
        foreach (var faq in faqs.Where(f => f.FAQCategory == FAQCategory.Pricing))
        {
            PricingFAQs.Add(faq);
        }
        foreach (var faq in faqs.Where(f => f.FAQCategory == FAQCategory.Cancellations))
        {
            CancellationsFAQs.Add(faq);
        }
        foreach (var faq in faqs.Where(f => f.FAQCategory == FAQCategory.PropertyPolicies))
        {
            PropertyPoliciesFAQs.Add(faq);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
