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
    public Amenity? BathoomPlan;
    public Amenity? BedPlan;
    public Property PropertyOnCreating => _propertyService.PropertyOnCreating;

    public FloorPlanViewModel(IPropertyService propertyService, IRepository<Amenity> amenitiesRepository)
    {
        _propertyService = propertyService;
        _amenitiesRepository = amenitiesRepository;
        LoadFloorPlans();

        // User can skip this step too
        IsStepCompleted = true;
    }

    private async void LoadFloorPlans()
    {
        var data = await _amenitiesRepository.GetAllAsync();
        BedroomPlan = data.FirstOrDefault(x => x.Name == "Bedroom");
        BathoomPlan = data.FirstOrDefault(x => x.Name == "Bathroom");
        BedPlan = data.FirstOrDefault(x => x.Name == "Bed");
    }

    public override void SaveProcess()
    {
        // Add amenities to the property if the quantity is greater than 0
        if (BedroomPlan?.Quantity > 0)
        {
            PropertyOnCreating.Amenities.Add(BedroomPlan);
        }
        if (BathoomPlan?.Quantity > 0)
        {
            PropertyOnCreating.Amenities.Add(BathoomPlan);
        }
        if (BedPlan?.Quantity > 0)
        {
            PropertyOnCreating.Amenities.Add(BedPlan);
        }
    }

    public override void ValidateProcess()
    {
    }
}
