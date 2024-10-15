using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Models;
public class Question: INotifyPropertyChanged
{
    public int ID
    {
        get; set;
    }

    public string Title
    {
        get; set;
    }

    public string Description
    {
        get; set;
    }

    /// <summary>
    /// Can be converted from to SymbolIcon
    /// Finished is "Accept"
    /// Unfinished is "Cancel"
    /// </summary>
    public bool IsAnswered
    {
        get; set;
    }

    public string DifficultyLevel
    {
        get; set;
    }

    public List<Answer> Answers
    {
        get; set;
    }

    public event PropertyChangedEventHandler PropertyChanged;
}

public class Answer : INotifyPropertyChanged
{
    public int QuestionID
    {
        get; set;
    }
    public string Text
    {
        get; set;
    }
    public bool IsCorrect
    {
        get; set;
    }

    public event PropertyChangedEventHandler PropertyChanged;
}