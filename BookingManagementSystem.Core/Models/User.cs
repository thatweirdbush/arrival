using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Models;
public enum Role
{
    Guest,
    Host,
    Admin
}

public enum Language
{
    English,
    Spanish,
    French,
    German,
    Italian,
    Portuguese,
    Dutch,
    Russian,
    Chinese,
    Japanese,
    Korean,
    Arabic,
    Hindi,
    Bengali,
    Punjabi,
    Urdu,
    Persian,
    Turkish,
    Vietnamese,
    Thai,
    Indonesian,
    Filipino,
    Malay,
    Swahili,
    Hausa,
    Yoruba,
    Igbo,
    Zulu
}

public class User : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public int Id
    {
        get; set;
    }
    public string FirstName
    {
        get; set;
    }
    public string LastName
    {
        get; set;
    }
    public string FullName => $"{FirstName} {LastName}";
    public string ShortBio
    {
        get; set;
    }
    public string Email
    {
        get; set;
    }
    public string Phone
    {
        get; set;
    }
    public string Address
    {
        get; set;
    }
    public string GovernmentId
    {
        get; set;
    }
    public string Role
    {
        get; set;
    } // E.g., Guest, Host, Admin
    public List<Language> Languages
    {
        get; set;
    } // Languages can be spoken by the user
    public DateTime CreatedAt
    {
        get; set;
    }
    public DateTime UpdatedAt
    {
        get; set;
    }
    public int YearsHosting => DateTime.Now.Year - CreatedAt.Year;
    private bool _isEliteHost
    {
        get; set;
    }
    public bool IsEliteHost => _isEliteHost;
    public string Username
    {
        get; set;
    }
    public string Password
    {
        get; set;
    }
    public string PasswordHash
    {
        get; set;
    }

    // Elite host promotion and demotion
    public void PromoteToElite()
    {
        _isEliteHost = true;
    }

    public void DemoteFromElite()
    {
        _isEliteHost = false;
    }
}
