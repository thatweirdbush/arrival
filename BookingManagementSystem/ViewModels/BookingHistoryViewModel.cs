using System.Collections.ObjectModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Repositories;
using BookingManagementSystem.ViewModels.Account;
using BookingManagementSystem.ViewModels.Client;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookingManagementSystem.ViewModels;

public partial class BookingHistoryViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IRepository<Booking> _bookingRepository;
    private readonly IRepository<Property> _propertyRepository;

    [ObservableProperty]
    private bool isUpcomingBookingListEmpty;

    [ObservableProperty]
    private bool isPastBookingListEmpty;

    // List of content items
    public ObservableCollection<Booking> UpcomingBookings { get; set; } = [];
    public ObservableCollection<Booking> PastBookings { get; set; } = [];

    public BookingHistoryViewModel(
        INavigationService navigationService,
        IRepository<Booking> bookingRepository,
        IRepository<Property> propertyRepository)
    {
        _navigationService = navigationService;
        _bookingRepository = bookingRepository;
        _propertyRepository = propertyRepository;
    }

    [RelayCommand]
    private void OnItemClick(Booking? clickedItem)
    {
        if (clickedItem != null)
        {
            _navigationService.SetListDataItemForNextConnectedAnimation(clickedItem.Property);
            _navigationService.NavigateTo(typeof(RentalDetailViewModel).FullName!, clickedItem.PropertyId);
        }
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Load Booking data list
        var bookings = await _bookingRepository.GetFilteredAndSortedAsync(
            filter: b => b.UserId == LoginViewModel.CurrentUser!.Id,
            keySelector: b => b.CheckInDate,
            sortDescending: true);

        foreach (var booking in bookings)
        {
            if (booking.CheckOutDate < DateTime.Now)
            {
                PastBookings.Add(booking);
            }
            else
            {
                UpcomingBookings.Add(booking);
            }
        }

        // Initial check
        CheckBookingListCount();
    }

    public void OnNavigatedFrom()
    {
    }

    public void DeleteBookingRangeAsync(IEnumerable<Booking> bookings)
    {
        foreach (var booking in bookings)
        {
            _bookingRepository.DeleteAsync(booking.Id);
            UpcomingBookings.Remove(booking);
            PastBookings.Remove(booking);
        }
        _bookingRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(Property property)
    {
        await _propertyRepository.UpdateAsync(property);
        await _propertyRepository.SaveChangesAsync();
    }

    private void CheckBookingListCount()
    {
        IsUpcomingBookingListEmpty = !UpcomingBookings.Any();
        IsPastBookingListEmpty = !PastBookings.Any();
    }
}
