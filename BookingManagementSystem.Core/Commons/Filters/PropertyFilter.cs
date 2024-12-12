using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Commons.Filters;
#nullable enable
public class PropertyFilter
{
    public DestinationType? DestinationType { get; set; }
    public bool? IsTrending { get; set; }
    public DateTimeOffset? CheckInDate { get; set; }
    public DateTimeOffset? CheckOutDate { get; set; }
    public string? Destination { get; set; }
    public int? MinGuests { get; set; }
    public int? PetsAllowed { get; set; }
    public int PageNumber { get; set; } = 1; // Default to first page
    public int PageSize { get; set; } = 10; // Default page size
}
