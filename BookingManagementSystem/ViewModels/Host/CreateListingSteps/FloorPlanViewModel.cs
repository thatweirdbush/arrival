using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class FloorPlanViewModel : BaseStepViewModel
{
    private readonly IRepository<Amenity> _amenitiesRepository;
    private readonly IPropertyService _propertyService;
    public PropertyAmenity? BedroomPlan { get; set; }
    public PropertyAmenity? BathroomPlan { get; set; }
    public PropertyAmenity? BedPlan { get; set; }
    public Property PropertyOnCreating => _propertyService.PropertyOnCreating!;

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

            // Initialize the amenity objects
            BedroomPlan = new PropertyAmenity()
            {
                PropertyId = PropertyOnCreating.Id,
                AmenityId = data.FirstOrDefault(x => x.Name == "Bedroom")!.Id
            };
            BathroomPlan = new PropertyAmenity()
            {
                PropertyId = PropertyOnCreating.Id,
                AmenityId = data.FirstOrDefault(x => x.Name == "Bathroom")!.Id
            };
            BedPlan = new PropertyAmenity()
            {
                PropertyId = PropertyOnCreating.Id,
                AmenityId = data.FirstOrDefault(x => x.Name == "Bed")!.Id
            };

            // Add the amenity objects to the property
            PropertyOnCreating.PropertyAmenities.Add(BedroomPlan);
            PropertyOnCreating.PropertyAmenities.Add(BathroomPlan);
            PropertyOnCreating.PropertyAmenities.Add(BedPlan);
        }
        else
        {
            // Assign the amenity objects to the corresponding amenity
            BedroomPlan = PropertyOnCreating.PropertyAmenities.FirstOrDefault(x => x.Amenity.Name == "Bedroom");
            BathroomPlan = PropertyOnCreating.PropertyAmenities.FirstOrDefault(x => x.Amenity.Name == "Bathroom");
            BedPlan = PropertyOnCreating.PropertyAmenities.FirstOrDefault(x => x.Amenity.Name == "Bed");
        }
    }

    public override void SaveProcess()
    {
        // No need to do save process here
    }


    public override void ValidateProcess()
    {
    }
}
