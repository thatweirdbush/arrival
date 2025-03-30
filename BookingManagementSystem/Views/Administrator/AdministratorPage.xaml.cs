using BookingManagementSystem.ViewModels.Administrator;
using BookingManagementSystem.Views.Client;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace BookingManagementSystem.Views.Administrator;

public sealed partial class AdministratorPage : Page
{
    public AdministratorViewModel ViewModel
    {
        get;
    }

    public AdministratorPage()
    {
        ViewModel = App.GetService<AdministratorViewModel>();
        InitializeComponent();
    }

    private int previousSelectedIndex = 0;
    private int previousSelectedMenuItemIndex = 0;

    private void SelectorBar_SelectionChanged
                 (SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
    {
        var selectedItem = sender.SelectedItem;
        var currentSelectedIndex = sender.Items.IndexOf(selectedItem);
        System.Type pageType;

        switch (currentSelectedIndex)
        {
            case 0:
                pageType = typeof(ListingRequestPage);
                break;
            case 1:
                pageType = typeof(ListingRequestPage);
                break;
            case 2:
                pageType = typeof(ReportPage);
                break;
            case 3:
                pageType = typeof(FAQPage);
                break;
            case 4:
                return;
            default:
                pageType = typeof(ListingRequestPage);
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

        if (clickedItem.Tag.ToString() == "reports")
        {
            pageType = typeof(ReportPage);
        }
        else if (clickedItem.Tag.ToString() == "reservations")
        {
            pageType = typeof(FAQPage);
        }
        else if (clickedItem.Tag.ToString() == "earnings")
        {
            pageType = typeof(FAQPage);
        }
        else if (clickedItem.Tag.ToString() == "vouchers")
        {
            pageType = typeof(FAQPage);
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
