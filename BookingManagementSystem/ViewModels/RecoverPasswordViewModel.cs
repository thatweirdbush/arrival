using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.ViewModels;
public partial class RecoverPasswordViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IDao _dao;

    public IEnumerable<User> Users { get; private set; } = Enumerable.Empty<User>();

    public RecoverPasswordViewModel(INavigationService navigationService, IDao dao)
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

    public bool RecoverPasswordAuthentication(string username, string password)
    {
        // Tìm người dùng có username tương ứng
        var user = Users.FirstOrDefault(u => u.Username.Equals(username));
        if (user != null)
        {
            user.Password = password;

            return true;
        }
        return false;
    }
}
