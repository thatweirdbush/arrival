using BookingManagementSystem.Core.Services;
using BookingManagementSystem.ViewModels.Client;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Client;

public sealed partial class ChatPage : Page
{

    public ChatViewModel ViewModel
    {
        get;
    }

    public ChatPage()
    {
        ViewModel = App.GetService<ChatViewModel>();
        InitializeComponent();

        // Gắn ViewModel vào DataContext của trang
        DataContext = ViewModel;
    }
}
