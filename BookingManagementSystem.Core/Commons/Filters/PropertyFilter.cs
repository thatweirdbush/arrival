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
    public DateTimeOffset? CheckInDate { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? CheckOutDate { get; set; } = DateTimeOffset.UtcNow.AddDays(5);
    public DestinationType? DestinationType { get; set; }
    public string? Destination { get; set; }
    public int? MinGuests { get; set; } = 1;
    public int? PetsAllowed { get; set; } = 0;
    public int PageNumber { get; set; } = 1; // Default to first page
    public int PageSize { get; set; } = 10; // Default page size
}
