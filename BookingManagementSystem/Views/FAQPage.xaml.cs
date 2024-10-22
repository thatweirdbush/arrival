using BookingManagementSystem.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views;

public sealed partial class FAQPage : Page
{
    public FAQViewModel ViewModel
    {
        get;
    }

    public FAQPage()
    {
        ViewModel = App.GetService<FAQViewModel>();
        InitializeComponent();
    }
}
