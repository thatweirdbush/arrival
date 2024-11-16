using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Repositories;

namespace BookingManagementSystem.Core.Services;
public class PropertyService : IPropertyService
{
    private readonly IRepository<Property> _propertyRepository;

    public Property PropertyOnCreating
    {
        get; set;
    }

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
        }
        else
        {
            // Add new property
            await _propertyRepository.AddAsync(property);
        }
    }

    public async Task<Property> GetPropertyInProgressAsync(int propertyId)
    {
        return await _propertyRepository.GetByIdAsync(propertyId);
    }
}
