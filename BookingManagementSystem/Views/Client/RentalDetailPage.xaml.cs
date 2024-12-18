using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using BookingManagementSystem.ViewModels.Client;
using BookingManagementSystem.Contracts.Services;
using CommunityToolkit.WinUI.UI.Animations;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Views.Forms;

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

    private void btnClearDates_Click(object sender, RoutedEventArgs e)
    {
        CalendarView.SelectedDates.Clear();
    }

    private void btnFavourite_Click(object sender, RoutedEventArgs e)
    {
        // Toggle the favourite button
        // Change the image source to the filled heart icon
        if (sender is FrameworkElement frameworkElement
            && frameworkElement.DataContext is Property property)
        {
            property.IsFavourite = !property.IsFavourite;
        }
    }

    private async void PropertyRatingControl_ValueChanged(RatingControl sender, object args)
    {
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
                UserId = 1,  // Hardcoded user id for now
                Rating = rating,
                Comment = comment,
                PropertyId = ViewModel.Item?.Id ?? 0,
            };

            // Show the successful dialog
            _ = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Review submission result",
                Content = $"Your review has been submitted successfully!\n\n" +
                $"Rating: {review.Rating} star(s)\n" +
                $"Comment: {review.Comment}\n" +
                $"Property Id: {review.PropertyId}\n",
                CloseButtonText = "Ok",
                DefaultButton = ContentDialogButton.Close
            }.ShowAsync();

            // Update real value for the RatingControl
            PropertyRatingControl.Value = rating;

            // Add to database
            await ViewModel.AddReviewAsync(review);

            // Add the review to the Reviews list
            ViewModel.Reviews.Insert(0, review);
        }
    }

    private async void btnReport_Click(object sender, RoutedEventArgs e)
    {
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
                UserId = 1,  // Hardcoded user id for now
                ReportReason = reportReason,
                Description = description,
                EntityType = entityType,
                EntityId = ViewModel.Item?.Id ?? 0,
            };

            // Add to database
            await ViewModel.AddBadReportAsync(badReport);

            // Show confirmation after sending report
            _ = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Report result",
                Content = $"Thank you for reporting this item.\n" +
                $"We will review this and inform you as soon as possible!\n\n" +
                $"Reason: {badReport.ReportReason} \n" +
                $"Description: {badReport.Description} \n" +
                $"Entity Type: {badReport.EntityType} \n" +
                $"Entity Id: {badReport.EntityId} \n" +
                $"Report Date: {badReport.ReportDate}",
                CloseButtonText = "Ok",
                DefaultButton = ContentDialogButton.Close
            }.ShowAsync();
        }
    }

    private async void btnSubmitQuestion_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(tbAskPropertyQuestion.Text))
        {
            _ = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Field is required",
                Content = "Please enter a question before submitting!",
                CloseButtonText = "Ok",
                DefaultButton = ContentDialogButton.Close
            }.ShowAsync();
            return;
        }

        if (ViewModel.Item != null)
        {
            // Create a new QnA object
            var qna = new QnA
            {
                Question = tbAskPropertyQuestion.Text,
                PropertyId = ViewModel.Item.Id
            };

            // Add to database
            await ViewModel.AddQnAAsync(qna);

            // Add the question to the QnA list
            ViewModel.QnAs.Insert(0, qna);

            // Show the successful dialog
            _ = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Question submission result",
                Content = "Your question has been submitted successfully!",
                CloseButtonText = "Ok",
                DefaultButton = ContentDialogButton.Close
            }.ShowAsync();
        }

        // Clear the question text box
        tbAskPropertyQuestion.Text = "";
    }
}
