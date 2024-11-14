using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class PlaceStructurePage : Page
{
    public PlaceStructureViewModel ViewModel
    {
        get;
    }

    public PlaceStructurePage()
    {
        ViewModel = App.GetService<PlaceStructureViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is Property property)
        {
            ViewModel.PropertyOnCreating = property;
        }
        base.OnNavigatedTo(e);
    }

    private void PropertyTypeGridView_ItemClick(object sender, ItemClickEventArgs e)
    {
        // Save to ViewModel.SelectedType
        ViewModel.SelectedType = ((PropertyTypeIcon)e.ClickedItem).PropertyType;
        if (ViewModel.PropertyOnCreating != null)
        {
            ViewModel.PropertyOnCreating.Type = ViewModel.SelectedType;
        }
    }
}
