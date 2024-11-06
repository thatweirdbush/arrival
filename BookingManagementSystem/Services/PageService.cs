using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.ViewModels;
using BookingManagementSystem.ViewModels.Account;
using BookingManagementSystem.ViewModels.Administrator;
using BookingManagementSystem.ViewModels.Client;
using BookingManagementSystem.ViewModels.Host;
using BookingManagementSystem.ViewModels.Payment;
using BookingManagementSystem.Views;
using BookingManagementSystem.Views.Account;
using BookingManagementSystem.Views.Administrator;
using BookingManagementSystem.Views.Client;
using BookingManagementSystem.Views.Host;
using BookingManagementSystem.Views.Payment;

using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Services;

public class PageService : IPageService
{
    private readonly Dictionary<string, Type> _pages = new();

    public PageService()
    {
        Configure<LoginViewModel, LoginPage>();
        Configure<MainViewModel, MainPage>();
        Configure<HomeViewModel, HomePage>();
        Configure<SettingsViewModel, SettingsPage>();
        Configure<MapViewModel, MapPage>();
        Configure<RentalDetailViewModel, RentalDetailPage>();
        Configure<HostViewModel, HostPage>();
        Configure<FAQViewModel, FAQPage>();
        Configure<PaymentViewModel, PaymentPage>();
        Configure<AdministratorViewModel, AdministratorPage>();
        Configure<ReportViewModel, ReportPage>();
        Configure<ListingRequestViewModel, ListingRequestPage>();
    }

    public Type GetPageType(string key)
    {
        Type? pageType;
        lock (_pages)
        {
            if (!_pages.TryGetValue(key, out pageType))
            {
                throw new ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?");
            }
        }

        return pageType;
    }

    private void Configure<VM, V>()
        where VM : ObservableObject
        where V : Page
    {
        lock (_pages)
        {
            var key = typeof(VM).FullName!;
            if (_pages.ContainsKey(key))
            {
                throw new ArgumentException($"The key {key} is already configured in PageService");
            }

            var type = typeof(V);
            if (_pages.ContainsValue(type))
            {
                throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == type).Key}");
            }

            _pages.Add(key, type);
        }
    }
}
