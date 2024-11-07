using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.DTOs;
public class BookingPropertyViewModel
{
    public Booking Booking
    {
        get; set;
    }
    public Property Property
    {
        get; set;
    }

    // Properties needed from both Booking and Property for binding
    public string Name => Property?.Name;
    public string Location => Property?.Location;
    public decimal PricePerNight => Property?.PricePerNight ?? 0;
    public string ImageThumbnail => Property?.ImagePaths?.FirstOrDefault();
    public bool IsPriority => Property?.IsPriority ?? false;
    public bool IsFavourite => Property?.IsFavourite ?? false;
}

