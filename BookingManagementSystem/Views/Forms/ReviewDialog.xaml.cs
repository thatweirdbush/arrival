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
using System.ComponentModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BookingManagementSystem.Views.Forms;

public sealed partial class ReviewDialog : ContentDialog, INotifyPropertyChanged
{
    public double RatingValue
    {
        get; set;
    }
    public string Comment
    {
        get; set;
    } = string.Empty; // Initialize Comment to avoid CS8618

    public ReviewDialog(double ratingValue)
    {
        InitializeComponent();
        RatingValue = ratingValue;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
