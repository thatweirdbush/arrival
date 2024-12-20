﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Models;
public class Notification : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public override string ToString() => $"Id: {Id}, User Id: {UserId}, " +
        $"Message: {Message}, Date Sent: {DateSent}, Is Read: {IsRead}";
    public int Id
    {
        get; set;
    }
    // Id of the user receiving the notification
    public int UserId
    {
        get; set;
    }
    public string SenderName
    {
        get; set;
    } = "Arrival";
    public string Title
    {
        get; set;
    }
    public string Message
    {
        get; set;
    }
    public string ImagePath
    {
        get; set;
    } = "ms-appx:///Assets/homescreen.jpg";
    public DateTime DateSent
    {
        get; set;
    } = DateTime.Now;
    public bool IsRead
    {
        get; set;
    } = false;
}