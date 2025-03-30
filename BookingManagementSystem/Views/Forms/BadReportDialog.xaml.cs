using BookingManagementSystem.Core.Models;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

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
