using System.Collections.ObjectModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.DTOs;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Client;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookingManagementSystem.ViewModels;

public partial class BookingHistoryViewModel : ObservableRecipient, INavigationAware
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
    }

    [RelayCommand]
    private void OnItemClick(BookingPropertyViewModel? clickedItem)
    {
        if (clickedItem != null)
        {
            _navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
            _navigationService.NavigateTo(typeof(RentalDetailViewModel).FullName!, clickedItem.Property.Id);
        }
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Load Booking data list
        var bookings = await _bookingRepository.GetAllAsync();
        var properties = await _propertyRepository.GetAllAsync();
        foreach (var booking in bookings)
        {
            var property = properties.FirstOrDefault(p => p.Id == booking.PropertyId);
            var bookingPropertyViewModel = new BookingPropertyViewModel
            {
                Booking = booking,
                Property = property
            };
            Bookings.Add(bookingPropertyViewModel);
        }

        // Subscribe to CollectionChanged event
        Bookings.CollectionChanged += (s, e) => CheckBookingListCount();

        // Initial check
        CheckBookingListCount();
    }

    public void OnNavigatedFrom()
    {
        Bookings.CollectionChanged -= (s, e) => CheckBookingListCount();
    }

    public void DeleteBookingAsync(BookingPropertyViewModel bookingPropertyViewModel)
    {
        _bookingRepository.DeleteAsync(bookingPropertyViewModel.Booking.Id);
        _bookingRepository.SaveChangesAsync();
        Bookings.Remove(bookingPropertyViewModel);
    }

    private void CheckBookingListCount()
    {
        IsBookingListEmpty = !Bookings.Any();
    }
}
