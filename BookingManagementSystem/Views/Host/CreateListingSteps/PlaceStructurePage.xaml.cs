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

    private void PropertyTypeGridView_ItemClick(object sender, ItemClickEventArgs e)
    {
        // Save to ViewModel.SelectedType
        ViewModel.SelectedType = ((PropertyTypeIcon)e.ClickedItem).PropertyType;
        ViewModel.PropertyOnCreating.Type = ViewModel.SelectedType;
    }
}
