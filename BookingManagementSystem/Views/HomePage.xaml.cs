using System.Security.Cryptography;
using System.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;
using BookingManagementSystem.ViewModels;
using BookingManagementSystem.Core.Models;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Globalization.DateTimeFormatting;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BookingManagementSystem.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class HomePage : Page
{
    public HomeViewModel ViewModel
    {
        get;
    }

    // List of Auto SuggestBox Destinations
    private List<string> Destinations
    {
        get;
    } = new()
    {
        "District 1, Ho Chi Minh City",
        "District 2, Ho Chi Minh City",
        "Hai Ba Trung, Hanoi",
        "Ba Dinh, Hanoi",
        "Vung Tau, Ba Ria - Vung Tau",
        "Quy Nhon, Binh Dinh",
        "Ha Long, Quang Ninh",
        "Hoi An, Quang Nam",
        "Nha Trang, Khanh Hoa",
        "Da Lat, Lam Dong",
        "Phu Quoc, Kien Giang",
        "Phan Thiet, Binh Thuan",
        "Hue, Thua Thien Hue",
        "Sapa, Lao Cai",
        "Con Dao, Ba Ria - Vung Tau",
        "Mui Ne, Binh Thuan",
        "Tam Dao, Vinh Phuc",
        "Cat Ba, Hai Phong",
        "Phong Nha, Quang Binh",
        "Bac Ha, Lao Cai",
        "Cua Lo, Nghe An",
        "Phu Quoc, Kien Giang",
    };

    public HomePage()
    {
        ViewModel = App.GetService<HomeViewModel>();
        InitializeComponent();
    }

    private void btnToggleSwitchWrapper_Click(object sender, RoutedEventArgs e)
    {
        // Toggle the switch
        ToggleSwitchDisplayTax.IsOn = !ToggleSwitchDisplayTax.IsOn;
    }

    private void btnCheckInCalendarWrapper_Click(object sender, RoutedEventArgs e)
    {
        // Show the calendar flyout
        CheckInCalendar.IsCalendarOpen = true;
    }

    private void btnCheckOutCalendarWrapper_Click(object sender, RoutedEventArgs e)
    {
        // Show the calendar flyout
        CheckOutCalendar.IsCalendarOpen = true;
    }

    private void btnSearchDestinationWrapper_Click(object sender, RoutedEventArgs e)
    {
        // Focus on AutoSuggestBox
        DestinationAutoSuggestBox.Focus(FocusState.Programmatic);
    }

    private void btnFavourite_Click(object sender, RoutedEventArgs e)
    {
        // Toggle the favourite button
        // Change the image source to the filled heart icon
        if ((sender as FrameworkElement).DataContext is Smartphone smartphone)
        {
            smartphone.IsFavourite = !smartphone.IsFavourite;
        }
    }

    // Handle text change and present suitable items
    private void DestinationAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        // Since selecting an item will also change the text,
        // only listen to changes caused by user entering text.
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        {
            var suitableItems = new List<string>();
            var splitText = sender.Text.ToLower().Split(" ");
            foreach (var item in Destinations)
            {
                var found = splitText.All((key) =>
                {
                    return item.ToLower().Contains(key);
                });
                if (found)
                {
                    suitableItems.Add(item);
                }
            }
            if (suitableItems.Count == 0)
            {
                suitableItems.Add("No results found");
            }
            sender.ItemsSource = suitableItems;
        }
    }

    // Handle user selecting an item, not implemented yet
    private void DestinationAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        //// Show ContentDialog with the selected item
        //var dialog = new ContentDialog
        //{
        //    XamlRoot = Content.XamlRoot,
        //    Title = "Selected item",
        //    Content = args.SelectedItem,
        //    PrimaryButtonText = "Ok",
        //    CloseButtonText = "Close"
        //};

        //await dialog.ShowAsync();
    }

    private void DestinationAutoSuggestBox_Drop(object sender, DragEventArgs e)
    {

    }
}
