using BookingManagementSystem.Core.Services;
using BookingManagementSystem.ViewModels.Client;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

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

        ViewModel.ScrollToBottomRequested += ChatViewModel_ScrollToBottomRequested;
    }


    private void OnSuggestedQuestionTapped(object sender, TappedRoutedEventArgs e)
    {
        if (sender is TextBlock textBlock)
        {
            var question = textBlock.Text;
            var viewModel = DataContext as ChatViewModel;
            viewModel?.SelectSuggestedQuestionCommand.Execute(question);
        }
    }

    // Auto Scroll to bottom when user receive message from bot
    private void ScrollToBottom()
    {
        var scrollViewer = GetScrollViewer(MessagesListView);
        if (scrollViewer != null)
        {
            scrollViewer.ChangeView(null, scrollViewer.ExtentHeight, null);
        }
    }

    // Lấy đối tượng ScrollViewer từ ListView
    private ScrollViewer? GetScrollViewer(DependencyObject obj)
    {
        if (obj is ScrollViewer)
            return obj as ScrollViewer;

        for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
        {
            var child = VisualTreeHelper.GetChild(obj, i);
            var result = GetScrollViewer(child);
            if (result != null)
                return result;
        }
        return null;
    }

    private void ChatViewModel_ScrollToBottomRequested()
    {
        ScrollToBottom();
    }
}
