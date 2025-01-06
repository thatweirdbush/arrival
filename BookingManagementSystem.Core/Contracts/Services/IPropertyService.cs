using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Services;

namespace BookingManagementSystem.Core.Contracts.Services;
#nullable enable
public interface IPropertyService
{
    Property? PropertyOnCreating { get; set; }
    Task SavePropertyAsync(Property property);
    Task<Property?> GetPropertyInProgressAsync(int propertyId);
    Task<CountryInfo?> GetCountryAsync(int countryId);
    Task AddCountryAsync(CountryInfo country);
    Task UpdateCountryAsync(CountryInfo country);
    Task AddPropertyAmenityAsync(PropertyAmenity propertyAmenity);
}
