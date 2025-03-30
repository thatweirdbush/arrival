using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Repositories;
public class PropertyTypeIconRepository
{
    private readonly List<PropertyTypeIcon> _icons;
    public PropertyTypeIconRepository()
    {
        _icons = new List<PropertyTypeIcon>(
        [
            new() { PropertyType = PropertyType.House, Name = "House", ImagePath = "house.svg" },
            new() { PropertyType = PropertyType.Apartment, Name = "Apartment", ImagePath = "apartment.svg" },
            new() { PropertyType = PropertyType.Barn, Name = "Barn", ImagePath = "barn.svg" },
            new() { PropertyType = PropertyType.BedAndBreakfast, Name = "Bed & breakfast", ImagePath = "bed-breakfast.svg" },
            new() { PropertyType = PropertyType.Boat, Name = "Boat", ImagePath = "boat.svg" },
            new() { PropertyType = PropertyType.Cabin, Name = "Cabin", ImagePath = "cabin.svg" },
            new() { PropertyType = PropertyType.CamperRV, Name = "Camper", ImagePath = "campervan.svg" },
            new() { PropertyType = PropertyType.CasaParticular, Name = "Casa particular", ImagePath = "casa-particular.svg" },
            new() { PropertyType = PropertyType.Castle, Name = "Castle", ImagePath = "castle.svg" },
            new() { PropertyType = PropertyType.Cave, Name = "Cave", ImagePath = "cave.svg" },
            new() { PropertyType = PropertyType.Container, Name = "Container", ImagePath = "container.svg" },
            new() { PropertyType = PropertyType.CycladicHome, Name = "Cycladic home", ImagePath = "cycladic-home.svg" },
            new() { PropertyType = PropertyType.Dammuso, Name = "Dammuso", ImagePath = "dammuso.svg" },
            new() { PropertyType = PropertyType.Dome, Name = "Dome", ImagePath = "dome.svg" },
            new() { PropertyType = PropertyType.Farm, Name = "Farm", ImagePath = "farm.svg" },
            new() { PropertyType = PropertyType.Guesthouse, Name = "Guesthouse", ImagePath = "guesthouse.svg" },
            new() { PropertyType = PropertyType.Hotel, Name = "Hotel", ImagePath = "hotel.svg" },
            new() { PropertyType = PropertyType.Houseboat, Name = "Houseboat", ImagePath = "houseboat.svg" },
            new() { PropertyType = PropertyType.Kezhan, Name = "Kezhan", ImagePath = "kezhan.svg" },
            new() { PropertyType = PropertyType.Minsu, Name = "Minsu", ImagePath = "minsu.svg" },
            new() { PropertyType = PropertyType.Riad, Name = "Riad", ImagePath= "riad.svg" },
            new() { PropertyType = PropertyType.Ryokan, Name = "Ryokan", ImagePath = "ryokan.svg" },
            new() { PropertyType = PropertyType.Tent, Name = "Tent", ImagePath = "tent.svg" },
            new() { PropertyType = PropertyType.TinyHome, Name = "Tiny home", ImagePath = "tiny-home.svg" },
            new() { PropertyType = PropertyType.Tower, Name = "Tower", ImagePath = "tower.svg" },
            new() { PropertyType = PropertyType.Treehouse, Name = "Treehouse", ImagePath = "treehouse.svg" },
            new() { PropertyType = PropertyType.Trullo, Name = "Trullo", ImagePath = "trullo.svg" },
            new() { PropertyType = PropertyType.Windmill, Name = "Windmill", ImagePath = "windmill.svg" },
            new() { PropertyType = PropertyType.Yurt, Name = "Yurt", ImagePath = "yurt.svg" }
        ]);
    }
    public Task<IEnumerable<PropertyTypeIcon>> GetAllAsync()
    {
        return Task.FromResult(_icons.AsEnumerable());
    }
}
