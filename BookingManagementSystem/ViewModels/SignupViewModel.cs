using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Services;

namespace BookingManagementSystem.ViewModels;

public partial class SignupViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IDao _dao;

    public IEnumerable<User> Users { get; private set; } = Enumerable.Empty<User>();

    public SignupViewModel(INavigationService navigationService, IDao dao)
    {
        _navigationService = navigationService;
        _dao = dao;
        OnNavigatedTo(_dao);
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Load user data list
        var users = await _dao.GetUserListDataAsync();
        Users = users;
    }

    public void OnNavigatedFrom()
    {
    }

    public bool SignupAuthentication(string username, string password)
    {
        // Tìm người dùng có username tương ứng
        var user = Users.FirstOrDefault(u => u.Username.Equals(username));
        if (user == null)
        {
            user = new User();
            user.Username = username;
            user.Password = password;

            _dao.AddUserAsync(user);

            return true;
        }

        return false; // Đăng ky thất bại
    }
}

