using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class FloorPlanViewModel : BaseStepViewModel
{
    private readonly IRepository<Amenity> _amenitiesRepository;
    private readonly IPropertyService _propertyService;
    public Amenity? BedroomPlan;
    public Amenity? BathroomPlan;
    public Amenity? BedPlan;
    public Property PropertyOnCreating => _propertyService.PropertyOnCreating;

    public FloorPlanViewModel(IPropertyService propertyService, IRepository<Amenity> amenitiesRepository)
    {
        _propertyService = propertyService;
        _amenitiesRepository = amenitiesRepository;

        // Initialize core properties
        LoadFloorPlans();

        // User can skip this step too
        IsStepCompleted = true;
    }

    private async void LoadFloorPlans()
    {
        if (PropertyOnCreating.PropertyAmenities.Count == 0)
        {
            var data = await _amenitiesRepository.GetAllAsync();

            BedroomPlan = data.FirstOrDefault(x => x.Name == "Bedroom");
            BathroomPlan = data.FirstOrDefault(x => x.Name == "Bathroom");
            BedPlan = data.FirstOrDefault(x => x.Name == "Bed");
        }
        else
        {
            BedroomPlan = PropertyOnCreating.PropertyAmenities
                .Select(pa => pa.Amenity)
                .FirstOrDefault(x => x.Name == "Bedroom");
            BathroomPlan = PropertyOnCreating.PropertyAmenities
                .Select(pa => pa.Amenity)
                .FirstOrDefault(x => x.Name == "Bathroom");
            BedPlan = PropertyOnCreating.PropertyAmenities
                .Select(pa => pa.Amenity)
                .FirstOrDefault(x => x.Name == "Bed");
        }
    }

    public override void SaveProcess()
    {
        // Add amenities to the property if the quantity is greater than 0
        if (BedroomPlan?.Quantity > 0)
        {
            PropertyOnCreating.PropertyAmenities.Add(new PropertyAmenity
            {
                Property = PropertyOnCreating,
                Amenity = BedroomPlan,
                Quantity = BedroomPlan.Quantity
            });
        }
        if (BathroomPlan?.Quantity > 0)
        {
            PropertyOnCreating.PropertyAmenities.Add(new PropertyAmenity
            {
                Property = PropertyOnCreating,
                Amenity = BathroomPlan,
                Quantity = BathroomPlan.Quantity
            });
        }
        if (BedPlan?.Quantity > 0)
        {
            PropertyOnCreating.PropertyAmenities.Add(new PropertyAmenity
            {
                Property = PropertyOnCreating,
                Amenity = BedPlan,
                Quantity = BedPlan.Quantity
            });
        }
    }

    public override void ValidateProcess()
    {
    }
}
