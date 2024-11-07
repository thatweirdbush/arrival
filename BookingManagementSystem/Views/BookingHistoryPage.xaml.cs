using BookingManagementSystem.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views;

public sealed partial class BookingHistoryPage : Page
{
    public BookingHistoryViewModel ViewModel
    {
        get;
    }

    public BookingHistoryPage()
    {
        ViewModel = App.GetService<BookingHistoryViewModel>();
        InitializeComponent();
    }
}
