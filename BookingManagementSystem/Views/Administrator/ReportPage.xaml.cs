using BookingManagementSystem.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Administrator;

public sealed partial class ReportPage : Page
{
    public ReportViewModel ViewModel
    {
        get;
    }

    public ReportPage()
    {
        ViewModel = App.GetService<ReportViewModel>();
        InitializeComponent();
    }
}
