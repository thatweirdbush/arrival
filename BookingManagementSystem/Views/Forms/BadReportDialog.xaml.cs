using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using BookingManagementSystem.Core.Models;
using System.Collections.ObjectModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BookingManagementSystem.Views.Forms;

public sealed partial class BadReportDialog : ContentDialog
{
    public string ReportReason => ReasonTextBox.Text;
    public string Description => DescriptionTextBox.Text;
    public EntityType Type => (EntityType)EntityTypeComboBox.SelectedItem;
    public IEnumerable<EntityType> EntityTypes { get; } = new List<EntityType>(GetEntityTypes());

    public BadReportDialog()
    {
        InitializeComponent();
    }

    public static IEnumerable<EntityType> GetEntityTypes()
    {
        return Enum.GetValues(typeof(EntityType)).Cast<EntityType>().ToList();
    }

    private void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        // Hide initial warning messages
        ReasonWarning.Visibility = Visibility.Collapsed;
        DescriptionWarning.Visibility = Visibility.Collapsed;

        var hasError = false;

        // Check ReasonTextBox
        if (string.IsNullOrWhiteSpace(ReasonTextBox.Text))
        {
            ReasonWarning.Visibility = Visibility.Visible;
            hasError = true;
        }

        // Check DescriptionTextBox
        if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
        {
            DescriptionWarning.Visibility = Visibility.Visible;
            hasError = true;
        }

        // If there is an error, prevent ContentDialog from closing
        if (hasError)
        {
            args.Cancel = true;
        }
    }
}
