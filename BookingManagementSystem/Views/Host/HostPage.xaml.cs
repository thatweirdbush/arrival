using BookingManagementSystem.ViewModels.Host;
using BookingManagementSystem.Views.Account;
using BookingManagementSystem.Views.Client;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace BookingManagementSystem.Views.Host;

public sealed partial class HostPage : Page
{
    public HostViewModel ViewModel
    {
        get;
    }

    public HostPage()
    {
        ViewModel = App.GetService<HostViewModel>();
        InitializeComponent();
    }

    private int previousSelectedIndex = 0;
    private int previousSelectedMenuItemIndex = 0;

    private void SelectorBar_SelectionChanged
                 (SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
    {
        var selectedItem = sender.SelectedItem;
        var selectedTag = selectedItem.Tag.ToString();
        var currentSelectedIndex = sender.Items.IndexOf(selectedItem);
        System.Type pageType;

        switch (selectedTag)
        {
            case "today":
                pageType = typeof(LoginPage);
                break;
            case "calendar":
                pageType = typeof(SignupPage);
                break;
            case "listings":
                pageType = typeof(ListingPage);
                break;
            case "messages":
                pageType = typeof(MapPage);
                break;
            case "menu":
                return;
            default:
                pageType = typeof(HostPage);
                break;
        }

        var slideNavigationTransitionEffect =
                currentSelectedIndex - previousSelectedIndex > 0 ?
                    SlideNavigationTransitionEffect.FromRight :
                    SlideNavigationTransitionEffect.FromLeft;

        ContentFrame.Navigate(pageType, null, new SlideNavigationTransitionInfo()
        {
            Effect = slideNavigationTransitionEffect
        });

        previousSelectedIndex = currentSelectedIndex;
    }

    private void MenuFlyoutItem_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var clickedItem = sender as MenuFlyoutItem;
        if (clickedItem == null || clickedItem.Tag == null)
        {
            return;
        }

        var currentSelectedIndex = MenuFlyoutButton.Items.IndexOf(clickedItem);
        var pageType = typeof(FAQPage);

        if (clickedItem.Tag.ToString() == "elite")
        {
            pageType = typeof(FAQPage);
        }
        else if (clickedItem.Tag.ToString() == "reservations")
        {
            pageType = typeof(ReservationsPage);
        }
        else if (clickedItem.Tag.ToString() == "earnings")
        {
            pageType = typeof(FAQPage);
        }
        else if (clickedItem.Tag.ToString() == "vouchers")
        {
            pageType = typeof(FAQPage);
        }
        else if (clickedItem.Tag.ToString() == "create-new-listing")
        {
            pageType = typeof(CreateListingPage);
        }
        else if (clickedItem.Tag.ToString() == "guidebooks")
        {
            pageType = typeof(FAQPage);
        }

        var slideNavigationTransitionEffect =
                currentSelectedIndex - previousSelectedMenuItemIndex > 0 ?
                    SlideNavigationTransitionEffect.FromRight :
                    SlideNavigationTransitionEffect.FromLeft;

        ContentFrame.Navigate(pageType, null, new SlideNavigationTransitionInfo()
        {
            Effect = slideNavigationTransitionEffect
        });

        previousSelectedMenuItemIndex = currentSelectedIndex;
        SelectorBar.SelectedItem = SelectorBarItemPage5;
    }
}
