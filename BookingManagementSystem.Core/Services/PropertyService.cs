using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Services;
#nullable enable
public class PropertyService : IPropertyService
{
    private readonly IRepository<Property> _propertyRepository;
    private readonly IRepository<CountryInfo> _countryInfoRepository;

    public Property? PropertyOnCreating { get; set; }

    public PropertyService(IRepository<Property> propertyRepository, IRepository<CountryInfo> countryInfoRepository)
    {
        _propertyRepository = propertyRepository;
        _countryInfoRepository = countryInfoRepository;
    }

    public async Task SavePropertyAsync(Property property)
    {
        var existingProperty = await _propertyRepository.GetByIdAsync(property.Id);
        if (existingProperty != null)
        {
            // Update the existing property
            await _propertyRepository.UpdateAsync(property);
            await _propertyRepository.SaveChangesAsync();
        }
        else
        {
            // Add new property
            await _propertyRepository.AddAsync(property);
            await _propertyRepository.SaveChangesAsync();
        }
    }

    public Task<Property?> GetPropertyInProgressAsync(int propertyId)
    {
        return _propertyRepository.GetQueryable()
            .Include(p => p.PropertyAmenities)
            .ThenInclude(pa => pa.Amenity)
            .Include(p => p.Country)
            .Where(p => p.CountryId == null || p.Country != null)
            .FirstOrDefaultAsync(p => p.Id == propertyId);
    }

    public Task<CountryInfo?> GetCountryAsync(int countryId)
    {
        return _countryInfoRepository.GetByIdAsync(countryId);
    }

    public async Task AddCountryAsync(CountryInfo country)
    {
        await _countryInfoRepository.AddAsync(country);
        await _countryInfoRepository.SaveChangesAsync();
    }

    public async Task UpdateCountryAsync(CountryInfo country)
    {
        await _countryInfoRepository.UpdateAsync(country);
        await _countryInfoRepository.SaveChangesAsync();
    }

    public Task AddPropertyAmenityAsync(PropertyAmenity propertyAmenity)
    {
        var existingAmenity = PropertyOnCreating?.PropertyAmenities
            .FirstOrDefault(pa => pa.AmenityId == propertyAmenity.AmenityId);

        if (existingAmenity != null)
        {
            // Update the existing amenity
            existingAmenity.Quantity = propertyAmenity.Quantity;
        }
        else
        {
            // Add new amenity
            PropertyOnCreating?.PropertyAmenities.Add(propertyAmenity);
        }
        // No need to save changes here because SavePropertyAsync will be called
        return Task.CompletedTask;
    }
}
