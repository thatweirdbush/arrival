using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Models;

public enum ReportStatus
{
    Pending,
    Approved,
    Rejected
}

public enum EntityType
{
    Property,
    User,
    Review
}

public class BadReport : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public override string ToString() => $"Bad Report Id: {Id}, User Id: {UserId}," +
        $" Report Date: {ReportDate}," +
        $" Report Description: {Description}";
    public int Id
    {
        get; set;
    }
    public int UserId
    {
        get; set;
    }
    public int EntityId
    {
        get; set;
    }
    public DateTime ReportDate
    {
        get; set;
    } = DateTime.Now.ToUniversalTime();  // Default report date is the current date and time

    // The reason or category of the report (e.g., "Spam", "Inappropriate Content", "Harassment")
    public string ReportReason
    {
        get; set;
    }
    public string Description
    {
        get; set;
    }
    public ReportStatus Status 
    { 
        get; set; 
    } = ReportStatus.Pending;  // Default status is "Pending"

    // Type of the entity being reported (e.g., Property, User, Review)
    public EntityType EntityType
    {
        get; set;
    } = EntityType.Property;  // Default entity type is "Property"

    // Admin or moderator who handled the report
    public int? HandledByAdminId
    {
        get; set;
    }  // Nullable, only set when the report is handled

    // Date and time the report was handled
    public DateTime? HandledDate
    {
        get; set;
    }  // Nullable, only set when the report is handled

    // Notes from the admin or moderator who handled the report
    public string AdminNotes
    {
        get; set;
    }
}
