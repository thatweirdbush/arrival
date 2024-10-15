using CommunityToolkit.WinUI.UI.Controls;

using Microsoft.UI.Xaml.Controls;

using BookingManagementSystem.ViewModels;

namespace BookingManagementSystem.Views;

public sealed partial class QuizPage : Page
{
    public QuizViewModel ViewModel
    {
        get;
    }

    public static QuizViewModel? QuizDetailViewModel
    {
        get; set;
    }

    public QuizPage()
    {
        ViewModel = App.GetService<QuizViewModel>();
        InitializeComponent();
    }

    private void OnViewStateChanged(object sender, ListDetailsViewState e)
    {
        if (e == ListDetailsViewState.Both)
        {
            ViewModel.EnsureItemSelected();
            QuizDetailViewModel = ViewModel;
        }
    }
}
