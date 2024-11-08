using System.Collections.ObjectModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.DTOs;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels;

public partial class BookingHistoryViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IRepository<Booking> _bookingRepository;
    private readonly IRepository<Property> _propertyRepository;

    // List of content items
    public ObservableCollection<BookingPropertyViewModel> Bookings { get; set; } = [];

    [ObservableProperty]
    private bool isBookingListEmpty;

    public BookingHistoryViewModel(INavigationService navigationService, IRepository<Booking> bookingRepository, IRepository<Property> propertyRepository)
    {
        _navigationService = navigationService;
        _bookingRepository = bookingRepository;
        _propertyRepository = propertyRepository;

        // Subscribe to CollectionChanged event
        Bookings.CollectionChanged += (s, e) => CheckBookingListCount();

        // Initial check
        CheckBookingListCount();

        OnNavigatedTo(0);
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Load BadReport data list
        var bookings = await _bookingRepository.GetAllAsync();
        foreach (var booking in bookings)
        {
            var property = await _propertyRepository.GetByIdAsync(booking.PropertyId);
            var bookingPropertyViewModel = new BookingPropertyViewModel
            {
                Booking = booking,
                Property = property
            };
            Bookings.Insert(0, bookingPropertyViewModel);
        }
    }

    public void OnNavigatedFrom()
    {
    }

    public void DeleteBookingAsync(BookingPropertyViewModel bookingPropertyViewModel)
    {
        _bookingRepository.DeleteAsync(bookingPropertyViewModel.Booking.Id);
        Bookings.Remove(bookingPropertyViewModel);
    }

    private void CheckBookingListCount()
    {
        IsBookingListEmpty = Bookings.Count == 0;
    }
}
