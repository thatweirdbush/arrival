using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using BookingManagementSystem.ViewModels.Client;
using BookingManagementSystem.Contracts.Services;
using CommunityToolkit.WinUI.UI.Animations;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Views.Forms;
using BookingManagementSystem.ViewModels.Account;

namespace BookingManagementSystem.Views.Client;
public sealed partial class RentalDetailPage : Page
{
    public RentalDetailViewModel ViewModel
    {
        get;
    }

    public RentalDetailPage()
    {
        ViewModel = App.GetService<RentalDetailViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        this.RegisterElementForConnectedAnimation("animationKeyContentGrid", itemHero);

        CalendarView.CalendarViewDayItemChanging += CalendarViewDayItemChanging;

        // Observe the ViewModel's Item and update the UI
        ViewModel.PropertyChanged += (s, args) =>
        {
            if (args.PropertyName == nameof(ViewModel.Item) && ViewModel.Item != null)
            {
                var query = $"{ViewModel.Item.Latitude}+{ViewModel.Item.Longitude}";
                MapWebView2.Source = new Uri($"https://www.google.com/maps/place/{query}");

                RentalDetailInfoBar.IsOpen = true;
                RentalDetailInfoBar.Message = ViewModel.Item.ToString();
            }
        };
    }

    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
        base.OnNavigatingFrom(e);
        if (e.NavigationMode == NavigationMode.Back)
        {
            var navigationService = App.GetService<INavigationService>();

            if (ViewModel.Item != null)
            {
                navigationService.SetListDataItemForNextConnectedAnimation(ViewModel.Item);
            }
        }
    }

    private void CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
    {
        if (args.Item.Date < DateTimeOffset.Now.Date)
        {
            // Disable past dates
            args.Item.IsEnabled = false;
        }
    }

    private void btnClearDates_Click(object sender, RoutedEventArgs e)
    {
        CalendarView.SelectedDates.Clear();
    }

    private async void btnFavourite_Click(object sender, RoutedEventArgs e)
    {
        // Check if the user is logged in
        if (LoginViewModel.CurrentUser == null)
        {
            await ShowContentDialogAsync("Login required", "Please login to submit this question!");
            return;
        }

        // Toggle the favourite button
        // Change the image source to the filled heart icon
        if (sender is FrameworkElement frameworkElement
            && frameworkElement.DataContext is Property property)
        {
            property.IsFavourite = !property.IsFavourite;
            await ViewModel.UpdateAsync(property);
        }
    }

    private async void PropertyRatingControl_ValueChanged(RatingControl sender, object args)
    {
        // Check if the user is logged in
        if (LoginViewModel.CurrentUser == null)
        {
            await ShowContentDialogAsync("Login required", "Please login to submit this question!");
            return;
        }

        // Create an instance of ReviewDialog with the current rating value
        var dialog = new ReviewDialog(PropertyRatingControl.Value)
        {
            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            XamlRoot = XamlRoot
        };

        // Show the form as a dialog box
        if (await dialog.ShowAsync() == ContentDialogResult.Primary)
        {
            var rating = dialog.RatingValue;
            var comment = dialog.Comment;

            // Create a new Review object
            var review = new Review
            {
                UserId = LoginViewModel.CurrentUser?.Id ?? 0,
                Rating = rating,
                Comment = comment,
                PropertyId = ViewModel.Item?.Id ?? 0,
            };

            // Update real value for the RatingControl
            PropertyRatingControl.Value = rating;

            // Add to database
            await ViewModel.AddReviewAsync(review);
        }
    }

    private async void btnReport_Click(object sender, RoutedEventArgs e)
    {
        // Check if the user is logged in
        if (LoginViewModel.CurrentUser == null)
        {
            await ShowContentDialogAsync("Login required", "Please login to submit this question!");
            return;
        }

        // Create an instance of BadReportDialog
        var reportDialog = new BadReportDialog
        {
            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            XamlRoot = XamlRoot
        };

        // Display the form as a dialog box
        if (await reportDialog.ShowAsync() == ContentDialogResult.Primary)
        {
            // Get data from BadReportDialog and process the report
            var reportReason = reportDialog.ReportReason;
            var description = reportDialog.Description;
            var entityType = reportDialog.Type;

            // Create a new BadReport object
            var badReport = new BadReport
            {
                UserId = LoginViewModel.CurrentUser?.Id ?? 0,
                ReportReason = reportReason,
                Description = description,
                EntityType = entityType,
                EntityId = ViewModel.Item?.Id ?? 0,
            };

            // Add to database and update the UI
            await ViewModel.AddBadReportAsync(badReport);

            // Show confirmation after sending report
            await ShowContentDialogAsync("Report result", "We will review this and inform you as soon as possible!");
        }
    }

    private async void btnSubmitQuestion_Click(object sender, RoutedEventArgs e)
    {
        // Check if the question is empty
        if (string.IsNullOrWhiteSpace(tbAskPropertyQuestion.Text))
        {
            await ShowContentDialogAsync("Field is required", "Please enter a question before submitting!");
            return;
        }

        // Check if the user is logged in
        if (LoginViewModel.CurrentUser == null)
        {
            await ShowContentDialogAsync("Login required", "Please login to submit this question!");
            return;
        }

        // Check if the property is fully loaded
        if (ViewModel.Item == null)
        {
            await ShowContentDialogAsync("Submit Error", "The property is not loaded yet!");
            return;
        }

        // Create a new QnA object
        var qna = new QnA
        {
            Question = tbAskPropertyQuestion.Text,
            PropertyId = ViewModel.Item.Id,
            CustomerId = LoginViewModel.CurrentUser.Id,
            HostId = ViewModel.Item.HostId,
        };

        // Add to database and update the UI
        await ViewModel.AddQnAAsync(qna);

        // Clear the question text box
        tbAskPropertyQuestion.Text = string.Empty;
    }

    private async Task ShowContentDialogAsync(string title, string content)
    {
        var dialog = new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = title,
            Content = content,
            CloseButtonText = "Ok",
            DefaultButton = ContentDialogButton.Close
        };

        await dialog.ShowAsync();
    }
}
