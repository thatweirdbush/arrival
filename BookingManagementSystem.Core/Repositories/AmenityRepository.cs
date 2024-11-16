using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Repositories;
public class AmenityRepository : Repository<Amenity>
{
    public AmenityRepository()
    {
        _entities.AddRange(
        [
            new() { Id = 1,
                    Name = "Wifi",
                    Type = AmenityType.GuestFavorite,
                    Description = "High-speed internet",
                    ImagePath = "wifi.svg"
            },
            new() { Id = 2,
                    Name = "TV",
                    Type = AmenityType.GuestFavorite,
                    Description = "Smart or Android TV with Netflix, Prime Video, etc.",
                    ImagePath = "tv.svg"
            },
            new() { Id = 3,
                    Name = "Kitchen",
                    Type = AmenityType.GuestFavorite,
                    Description = "Fully equipped kitchen with stove, oven, microwave, etc.",
                    ImagePath = "kitchen.svg"
            },
            new() { Id = 4,
                    Name = "Washer",
                    Type = AmenityType.GuestFavorite,
                    Description = "Washer and dryer",
                    ImagePath = "washer.svg"
            },
            new() { Id = 5,
                    Name = "Free parking",
                    Type = AmenityType.GuestFavorite,
                    Description = "Free parking on premises",
                    ImagePath = "parking.svg"
            },
            new() { Id = 6,
                    Name = "Paid parking",
                    Type = AmenityType.GuestFavorite,
                    Description = "Paid parking on premises",
                    ImagePath = "paid-parking.svg"
            },
            new() { Id = 7,
                    Name = "Air conditioning",
                    Type = AmenityType.GuestFavorite,
                    Description = "Central air conditioning",
                    ImagePath = "air-conditioning.svg"
            },
            new() { Id = 8,
                    Name = "Dedicated workspace",
                    Type = AmenityType.GuestFavorite,
                    Description = "A table or desk with space for a laptop and a chair that’s comfortable to work in",
                    ImagePath = "workspace.svg"
            },
            new() { Id = 9,
                    Name = "Pool",
                    Type = AmenityType.Standout,
                    Description = "Private or shared pool",
                    ImagePath = "pool.svg"
            },
            new() { Id = 10,
                    Name = "Hot tub",
                    Type = AmenityType.Standout,
                    Description = "Private king size hot tub",
                    ImagePath = "hot-tub.svg"
            },
            new() { Id = 11,
                    Name = "Patio",
                    Type = AmenityType.Standout,
                    Description = "Private or shared patio",
                    ImagePath = "patio.svg"
            },
            new() { Id = 12,
                    Name = "BBQ grill",
                    Type = AmenityType.Standout,
                    Description = "BBQ grill with coal and lighter fluid",
                    ImagePath = "bbq-grill.svg"
            },
            new() { Id = 13,
                    Name = "Outdoor dining area",
                    Type = AmenityType.Standout,
                    Description = "Outdoor dining area with candles and lights",
                    ImagePath = "outdoor-dining.svg"
            },
            new() { Id = 14,
                    Name = "Pool table",
                    Type = AmenityType.Standout,
                    Description = "Pool table with cues and balls",
                    ImagePath = "pool-table.svg"
            },
            new() { Id = 15,
                    Name = "Indoor fireplace",
                    Type = AmenityType.Standout,
                    Description = "Fireplace in the living room",
                    ImagePath = "indoor-fireplace.svg"
            },
            new() { Id = 16,
                    Name = "Piano",
                    Type = AmenityType.Standout,
                    Description = "Piano in the living room",
                    ImagePath = "piano.svg"
            },
            new() { Id = 17,
                    Name = "Exercise equipment",
                    Type = AmenityType.Standout,
                    Description = "Exercise equipment including treadmill, elliptical, weights, etc.",
                    ImagePath = "gym.svg"
            },
            new() { Id = 18,
                    Name = "Lake access",
                    Type = AmenityType.Standout,
                    Description = "Lake access with kayaks and canoes",
                    ImagePath = "lake-area.svg"
            },
            new() { Id = 19,
                    Name = "Beach access",
                    Type = AmenityType.Standout,
                    Description = "Beach access with chairs and umbrellas",
                    ImagePath = "beach-area.svg"
            },
            new() { Id = 20,
                    Name = "Ski-in/Ski-out",
                    Type = AmenityType.Standout,
                    Description = "Ski in/Ski out access to the slopes",
                    ImagePath = "ski.svg"
            },
            new() { Id = 21,
                    Name = "Outdoor shower",
                    Type = AmenityType.Standout,
                    Description = "Outdoor shower with hot water",
                    ImagePath = "shower.svg"
            },
            new() { Id = 22,
                    Name = "Smoke alarm",
                    Type = AmenityType.Safety,
                    Description = "Smoke alarm in all rooms",
                    ImagePath = "smoke-alarm.svg"
            },
            new() { Id = 23,
                    Name = "First aid kit",
                    Type = AmenityType.Safety,
                    Description = "First aid kit with bandages, gauze, etc.",
                    ImagePath = "first-aid-kit.svg"
            },
            new() { Id = 24,
                    Name = "Fire extinguisher",
                    Type = AmenityType.Safety,
                    Description = "Fire extinguisher in the kitchen",
                    ImagePath = "fire-extinguisher.svg"
            },
            new() { Id = 25,
                    Name = "Carbon oxide alarm",
                    Type = AmenityType.Safety,
                    Description = "Carbon monoxide alarm in all rooms",
                    ImagePath = "carbon-monoxide-alarm.svg"
            },
            new() { Id = 26,
                    Name = "Bedroom",
                    Type = AmenityType.RoomEssentials,
                    Description = "Bedroom with bed, pillows, and blankets",
                    ImagePath = "bedroom.svg"
            },
            new() { Id = 27,
                    Name = "Bathroom",
                    Type = AmenityType.RoomEssentials,
                    Description = "Bathroom with shower, toilet, and sink",
                    ImagePath = "bathroom.svg"
            },
            new() { Id = 28,
                    Name = "Living Room",
                    Type = AmenityType.RoomEssentials,
                    Description = "Living room with sofa, chairs, and coffee table",
                    ImagePath = "living-room.svg"
            },
            new() { Id = 29,
                    Name = "Dining Room",
                    Type = AmenityType.RoomEssentials,
                    Description = "Dining room with table, chairs, and cutlery",
                    ImagePath = "dining-room.svg"
            },
            new() { Id = 30,
                    Name = "Kitchen",
                    Type = AmenityType.RoomEssentials,
                    Description = "Kitchen with stove, oven, microwave, and fridge",
                    ImagePath = "kitchen.svg"
            },
            new() { Id = 31,
                    Name = "Laundry",
                    Type = AmenityType.RoomEssentials,
                    Description = "Laundry room with washer and dryer",
                    ImagePath = "laundry.svg"
            },
            new() { Id = 32,
                    Name = "Outdoor",
                    Type = AmenityType.RoomEssentials,
                    Description = "Outdoor area with patio, BBQ grill, and pool",
                    ImagePath = "outdoor.svg"
            },
            new() { Id = 33,
                    Name = "Bed",
                    Type = AmenityType.Household,
                    Description = "Bed with pillows, blankets, and sheets",
                    ImagePath = "bed.svg"
            },
        ]);
    }
}
