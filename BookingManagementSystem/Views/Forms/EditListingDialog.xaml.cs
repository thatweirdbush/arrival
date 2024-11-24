
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.ViewModels;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.ViewModels.Host;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BookingManagementSystem.Views.Forms;
public sealed partial class EditListingDialog : ContentDialog
{
    public enum DialogResult
    {
        None,
        Edit,
        Remove,
    }
    public Property Property
    {
        get; set;
    }
    public DialogResult Result { get; private set; } = DialogResult.None;
    public EditListingDialog(Property property)
    {
        Property = property;
        InitializeComponent();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        // Close the dialog
        Hide();
    }

    private void EditListingButton_Click(object sender, RoutedEventArgs e)
    {
        // Navigate to the Edit Listing Page
        Result = DialogResult.Edit;
        Hide();
    }

    private void RemoveListingButton_Click(object sender, RoutedEventArgs e)
    {
        // Remove the listing
        Result = DialogResult.Remove;
        Hide();
    }
}
