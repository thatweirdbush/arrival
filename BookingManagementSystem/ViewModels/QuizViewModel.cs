using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.ViewModels;

public partial class QuizViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;

    [ObservableProperty]
    private Question? selected;

    public ObservableCollection<Question> Questions { get; set; } = new()
    {
        new Question()
        {
            ID = 0,
            Title = "Question 1",
            Description = "\"We cannot solve problems with the kind of ... we employed when we came up with them.\" - Albert Einstein",
            IsAnswered = false,
            DifficultyLevel = "Easy",
            Answers = new List<Answer>()
            {
                new() { QuestionID = 0, Text = "thinking", IsCorrect = true },
                new() { QuestionID = 0, Text = "telling", IsCorrect = false }
            }
        },
        new Question()
        {
            ID = 1,
            Title = "Question 2",
            Description = "\"Success is not final; failure is not fatal: It is the ... to continue that counts.\" - Winston Churchill",
            IsAnswered = false,
            DifficultyLevel = "Hard",
            Answers = new List<Answer>()
            {
                new() { QuestionID = 1, Text = "power", IsCorrect = false },
                new() { QuestionID = 1, Text = "courage", IsCorrect = true }
            }
        },
        new Question()
        {
            ID = 2,
            Title = "Question 3",
            Description = "\"Don’t let ... take up too much of today.\" - Will Rogers",
            IsAnswered = false,
            DifficultyLevel = "Hard",
            Answers = new List<Answer>()
            {
                new() { QuestionID = 2, Text = "laziness", IsCorrect = false },
                new() { QuestionID = 2, Text = "yesterday", IsCorrect = true }
            }
        },
        new Question()
        {
            ID = 3,
            Title = "Question 4",
            Description = "\"Either you run the day or the day ... you.\" - Jim Rohn",
            IsAnswered = false,
            DifficultyLevel = "Medium",
            Answers = new List<Answer>()
            {
                new() { QuestionID = 3, Text = "runs", IsCorrect = true },
                new() { QuestionID = 3, Text = "hits", IsCorrect = false }
            }
        },
        new Question()
        {
            ID = 4,
            Title = "Question 5",
            Description = "\"We don’t just sit around and ... for other people. We just make, and we do.\" - Arlan Hamilton",
            IsAnswered = false,
            DifficultyLevel = "Hard",
            Answers = new List<Answer>()
            {
                new() { QuestionID = 4, Text = "hope", IsCorrect = false },
                new() { QuestionID = 4, Text = "wait", IsCorrect = true }

            }
        },
        new Question()
        {
            ID = 5,
            Title = "Question 6",
            Description = "\"The successful man will profit from his ... and try again in a different way.\" - Dale Carnegie",
            IsAnswered = false,
            DifficultyLevel = "Medium",
            Answers = new List<Answer>()
            {
                new() { QuestionID = 5, Text = "cars", IsCorrect = false },
                new() { QuestionID = 5, Text = "mistakes", IsCorrect = true }
            }
        },
        new Question()
        {
            ID = 6,
            Title = "Question 7",
            Description = "\"You can’t be that ... standing at the top of the waterslide, overthinking it. You have to go down the chute.\" - Tina Fey",
            IsAnswered = false,
            DifficultyLevel = "Easy",
            Answers = new List<Answer>()
            {
                new() { QuestionID = 6, Text = "kid", IsCorrect = true },
                new() { QuestionID = 6, Text = "man", IsCorrect = false }
            }
        },
        new Question()
        {
            ID = 7,
            Title = "Question 8",
            Description = "\"People often say that ... doesn’t last. Well, neither does bathing—that’s why we recommend it daily.\" - Zig Ziglar",
            IsAnswered = false,
            DifficultyLevel = "Hard",
            Answers = new List<Answer>()
            {
                new() { QuestionID = 7, Text = "happiness", IsCorrect = false },
                new() { QuestionID = 7, Text = "motivation", IsCorrect = true }
            }
        },
        new Question()
        {
            ID = 8,
            Title = "Question 9",
            Description = "\"If you believe something needs to exist, if it’s something you want to use yourself, don’t let anyone ever ... you from doing it.\" - Tobias Lutke",
            IsAnswered = false,
            DifficultyLevel = "Easy",
            Answers = new List<Answer>()
            {
                new() { QuestionID = 8, Text = "hide", IsCorrect = false },
                new() { QuestionID = 8, Text = "stop", IsCorrect = true }
            }
        },
        new Question()
        {
            ID = 9,
            Title = "Question 10",
            Description = "\"Never give up on a ... just because of the time it will take to accomplish it. The time will pass anyway.\" - Earl Nightingale",
            IsAnswered = false,
            DifficultyLevel = "Medium",
            Answers = new List<Answer>()
            {
                new() { QuestionID = 9, Text = "dream", IsCorrect = true },
                new() { QuestionID = 9, Text = "person", IsCorrect = false }
            }
        }
    };

    public QuizViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public void OnNavigatedTo(object parameter)
    {
        //Questions.Clear();

        //var data = await _sampleDataService.GetListDetailsDataAsync();

        //foreach (var item in data)
        //{
        //    Questions.Add(item);
        //}
    }

    public void OnNavigatedFrom()
    {
    }

    public void EnsureItemSelected()
    {
        Selected ??= Questions.First();
    }

    public void ChangeSelectionByIndex(int index)
    {
        Selected ??= Questions[index];
    }

    public void GetNextQuestion()
    {
        if (Selected != null)
        {
            var index = Selected.ID;
            if (index < Questions.Count - 1)
            {
                Selected = Questions[index + 1];
            }
        }
    }

    public bool IsLastQuestion()
    {
        return Selected != null && Selected.ID == Questions.Count - 1;
    }

    public void MarkAsAnswered()
    {
        //if (Selected != null)
        //{
        //    Selected.Status = Selected.Status == "Accept" ? "Cancel" : "Accept";
        //}
        Selected.IsAnswered = true;
    }
}
