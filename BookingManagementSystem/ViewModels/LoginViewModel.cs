using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.ViewModels;

public partial class LoginViewModel : ObservableRecipient
{

    private ObservableCollection<User> _users
    {
        get;
    } = new()
    {
        new User()
        {
            Username = "admin",
            Password = "123"
        }, 
        new User()
        {
            Username = "customer",
            Password = "123"
        }
    };

    public ObservableCollection<User> Users => _users;

    // Check if user has correct password for account
    public bool Login(string username, string password)
    {
        var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        return user != null;
    }

    public LoginViewModel()
    {
    }
}
