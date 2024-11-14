using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class FloorPlanViewModel : ObservableRecipient
{
    public Property? PropertyOnCreating = null;
    public Amenity? BedroomPlan = null;
    public Amenity? BathoomPlan = null;
    public Amenity? BedPlan = null;
    private readonly IRepository<Amenity> _amenitiesRepository;

    public FloorPlanViewModel(IRepository<Amenity> amenitiesRepository)
    {
        _amenitiesRepository = amenitiesRepository;
        LoadFloorPlans();
    }

    private async void LoadFloorPlans()
    {
        var data = await _amenitiesRepository.GetAllAsync();
        BedroomPlan = data.FirstOrDefault(x => x.Name == "Bedroom");
        BathoomPlan = data.FirstOrDefault(x => x.Name == "Bathroom");
        BedPlan = data.FirstOrDefault(x => x.Name == "Bed");
    }

    public void InitializePropertyFloorPlans()
    {
        if (PropertyOnCreating != null)
        {
            PropertyOnCreating.Amenities.Add(BedroomPlan);
            PropertyOnCreating.Amenities.Add(BathoomPlan);
            PropertyOnCreating.Amenities.Add(BedPlan);
        }
    }
}
