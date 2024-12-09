using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Repositories;
public class DestinationTypeSymbolRepository
{
    private readonly List<DestinationTypeSymbol> _icons;
    public DestinationTypeSymbolRepository()
    {
        _icons = new List<DestinationTypeSymbol>(
        [
            new(){
                Name = "All",
                DestinationType = DestinationType.All,
                ImagePath = "all-icon.png"
            },
            new(){
                Name = "Amazing Pools",
                DestinationType = DestinationType.AmazingPools,
                ImagePath = "amazing-pools.jpg"
            },
            new(){
                Name = "Amazing Views",
                DestinationType = DestinationType.AmazingViews,
                ImagePath = "amazing-views.jpg"
            },
            new(){
                Name = "Beach",
                DestinationType = DestinationType.Beach,
                ImagePath = "beach.jpg"
            },
            new(){
                Name = "Countryside",
                DestinationType = DestinationType.Countryside,
                ImagePath = "countryside.jpg"
            },
            new(){
                Name = "Farm",
                DestinationType = DestinationType.Farm,
                ImagePath = "farm.jpg"
            },
            new(){
                Name = "Islands",
                DestinationType = DestinationType.Islands,
                ImagePath = "islands.jpg"
            },
            new(){
                Name = "Lakefront",
                DestinationType = DestinationType.Lakefront,
                ImagePath = "lakefront.jpg"
            },
            new(){
                Name = "Luxe",
                DestinationType = DestinationType.Luxe,
                ImagePath = "luxe.jpg"
            },
            new(){
                Name = "Mansions",
                DestinationType = DestinationType.Mansions,
                ImagePath = "mansions.jpg"
            },
            new(){
                Name = "Room",
                DestinationType = DestinationType.Room,
                ImagePath = "room.jpg"
            },
            new(){
                Name = "National Parks",
                DestinationType = DestinationType.NationalParks,
                ImagePath = "national-parks.jpg"
            },
            new(){
                Name = "Tiny Homes",
                DestinationType = DestinationType.TinyHomes,
                ImagePath = "tiny-homes.jpg"
            },
            new(){
                Name = "Treehouses",
                DestinationType = DestinationType.Treehouses,
                ImagePath = "treehouses.jpg"
            },
            new(){
                Name = "Top Cities",
                DestinationType = DestinationType.TopCities,
                ImagePath = "top-cities.jpg"
            },
            new(){
                Name = "Trending",
                DestinationType = DestinationType.Trending,
                ImagePath = "trending.jpg"
            },
            new(){
                Name = "Tropical",
                DestinationType = DestinationType.Tropical,
                ImagePath = "tropical.jpg"
            }
        ]);
    }
    public Task<IEnumerable<DestinationTypeSymbol>> GetAllAsync()
    {
        return Task.FromResult(_icons.AsEnumerable());
    }
}
