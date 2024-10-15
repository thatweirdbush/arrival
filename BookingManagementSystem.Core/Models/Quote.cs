using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Models;
public class Quote
{
    // Define XAML Image's Path Declaration
    public static string MS_APPX = "ms-appx:///Assets/quote-images/";

    // Define Quote Properties
    public int Id
    {
        get; set;
    }
    public string Text
    {
        get; set;
    }
    public string Author
    {
        get; set;
    }
    public string ImagePath
    {
        get; set;
    }

    public Quote(int id, string text, string author, string imagePath)
    {
        Id = id;
        Text = text;
        Author = author;
        ImagePath = $"{MS_APPX}{imagePath}";
    }

    public override string ToString()
    {
        return $"\"{Text}\" - {Author}";
    }
}
