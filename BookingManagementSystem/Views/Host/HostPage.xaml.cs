using BookingManagementSystem.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace BookingManagementSystem.Views;

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
        var currentSelectedIndex = sender.Items.IndexOf(selectedItem);
        System.Type pageType;

        switch (currentSelectedIndex)
        {
            case 0:
                pageType = typeof(LoginPage);
                break;
            case 1:
                pageType = typeof(SignupPage);
                break;
            case 2:
                pageType = typeof(FAQPage);
                break;
            case 3:
                pageType = typeof(MapPage);
                break;
            case 4:
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
