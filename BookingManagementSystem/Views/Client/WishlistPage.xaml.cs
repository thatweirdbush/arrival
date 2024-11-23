using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Client;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Client;

public sealed partial class WishlistPage : Page
{
    public WishlistViewModel ViewModel
    {
        get;
    }

    public WishlistPage()
    {
        ViewModel = App.GetService<WishlistViewModel>();
        InitializeComponent();
    }

    private void EditListing_Click(object sender, RoutedEventArgs e)
    {
    }

    private void CancelEditing_Click(object sender, RoutedEventArgs e)
    {
    }

    private void RemoveListing_Click(object sender, RoutedEventArgs e)
    {
    }

    private void RemoveAllLissting_Click(object sender, RoutedEventArgs e)
    {
    }

    private void btnGetStarted_Click(object sender, RoutedEventArgs e)
    {
    }

    private void SearchListing_Click(object sender, RoutedEventArgs e)
    {
    }

    private void OnCommandBarElementClicked(object sender, RoutedEventArgs e)
    {
    }

    private void CloseSearchBoxButton_Click(object sender, RoutedEventArgs e)
    {
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

    private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
    }

    private void SearchBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
    }
}
