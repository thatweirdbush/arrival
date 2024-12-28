using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Services;
public class PropertyService : IPropertyService
{
    private readonly IRepository<Property> _propertyRepository;

    public Property PropertyOnCreating { get; set; }

    public PropertyService(IRepository<Property> propertyRepository)
    {
        _propertyRepository = propertyRepository;
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

    public Task<Property> GetPropertyInProgressAsync(int propertyId)
    {
        return _propertyRepository.GetByIdAsync(propertyId);
    }
}
