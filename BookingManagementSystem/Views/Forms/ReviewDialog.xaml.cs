using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Forms;

public sealed partial class ReviewDialog : ContentDialog
{
    public double RatingValue { get; set; }
    public string? Comment { get; set; }

    public ReviewDialog(double ratingValue)
    {
        InitializeComponent();
        RatingValue = ratingValue;
    }
}
