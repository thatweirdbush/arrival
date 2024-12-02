﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Contracts.Facades;
public interface IHomeFacade
{
    Task<IEnumerable<DestinationTypeSymbol>> GetAllDestinationTypeSymbolsAsync();
    Task<IEnumerable<Property>> GetAllPropertiesAsync();
    Task<IEnumerable<Property>> GetAvailableRoomsAsync(DateTimeOffset? checkIn, DateTimeOffset? checkOut);
    Task<List<string>> SearchLocationsAsync(string query, int maxRows = 10);
}
