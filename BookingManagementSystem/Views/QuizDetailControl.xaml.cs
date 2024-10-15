    using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Views;

public sealed partial class QuizDetailControl : UserControl
{
    public Question? ListDetailsMenuItem
    {
        get => GetValue(ListDetailsMenuItemProperty) as Question;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }

    public static List<Answer> SelectedAnswers { get; set; } = new();
    public static Button? SelectedButton
    {
        get; set;
    }

    public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(Question), typeof(QuizDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

    public QuizDetailControl()
    {
        InitializeComponent();
    }

    private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is QuizDetailControl control)
        {
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }

    private void btnNextQuestion_Click(object sender, RoutedEventArgs e)
    {
        // Update current item's IsAnswered and SymbolIcon
        QuizPage.QuizDetailViewModel?.MarkAsAnswered();

        // If reach to le last question, show result by the ContentDialog
        if (QuizPage.QuizDetailViewModel.IsLastQuestion())
        {
            var dialog = new ContentDialog
            {
                XamlRoot = Content.XamlRoot,
                Title = "Result",
                Content = $"Your score: {CalculateScore()}",
                CloseButtonText = "Ok"
            };

            _ = dialog.ShowAsync();
            return;
        }

        // Reset button highlight
        SelectedButton?.SetValue(BackgroundProperty, App.Current.Resources["ThumbBorderThemeBrush"]);

        // Navigate to the next question
        QuizPage.QuizDetailViewModel?.GetNextQuestion();
    }

    private void btnAnswer_Click(object sender, RoutedEventArgs e)
    {
        // Get the selected answer from Button Click
        SelectedButton = (sender as Button);
        var selectedAnswer = SelectedButton?.DataContext as Answer;

        // Add the selected answer to the list of selected answers, if already selected remove it
        if (SelectedAnswers.Contains(selectedAnswer))
        {
            SelectedAnswers.Remove(selectedAnswer);

            // Remove the highlight from the selected answer
            SelectedButton?.SetValue(BackgroundProperty, App.Current.Resources["ThumbBorderThemeBrush"]);
        }
        else
        {
            SelectedAnswers.Add(selectedAnswer);

            // Highlight the selected answer
            SelectedButton?.SetValue(BackgroundProperty, App.Current.Resources["SystemControlBackgroundAccentBrush"]);
        }
    }

    // Calculate the score
    public int CalculateScore()
    {
        var score = 0;
        foreach (var answer in SelectedAnswers)
        {
            if (answer.IsCorrect)
            {
                score++;
            }
        }
        return score;
    }
}
