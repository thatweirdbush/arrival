using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Services;

namespace BookingManagementSystem.Core.Repositories;
public class PropertyRepository : Repository<Property>
{
    public List<CountryInfo> CountryList { get; private set; } = [];
    public async Task LoadCountriesAsync()
    {
        var geonamesService = new GeographicNameService();
        var countries = await geonamesService.GetAllCountryInfoAsync();
        foreach (var country in countries)
        {
            CountryList.Add(country);
        }
    }
    public CountryInfo GetCountryByCode(string countryCode)
    {
        return CountryList.FirstOrDefault(c => c.CountryCode == countryCode);
    }
    public PropertyRepository()
    {
        // Load country list for querying below 
        _ = LoadCountriesAsync();

        _entities.AddRange(
        [
            new()
            {
                Id = 1,
                Name = "Cozy room in Montmartre",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths =
                [
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                ],
                DestinationTypes = 
                [
                    DestinationType.AmazingPools, 
                    DestinationType.AmazingViews, 
                    DestinationType.Beach, 
                    DestinationType.City, 
                    DestinationType.TopCities, 
                    DestinationType.Luxe, 
                    DestinationType.Room, 
                    DestinationType.NationalParks
                ],
                Country = GetCountryByCode("FR"),
                StateOrProvince = "Paris",
                CityOrDistrict = "Montmartre",
                StreetAddress = "Rue de la Bonne",
                PostalCode = "75018",
                PricePerNight = 99.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 48.864716,
                Longitude = 2.349014,
                IsPriority = true,
                IsPetFriendly = true,
                MaxGuests = 2
            },
            new()
            {
                Id = 2,
                Name = "Hidden Bamboo Bali",
                Type = PropertyType.Hotel,
                Description = "Hidden Bamboo Bali is the unique Eco Friendly Bamboo House in Bali, located in Tampakasing village which is 30 minute from Ubud city center and 1 hour 40 minute from Airport. a private house in the midle of nature which is good for nature lover, yoga, music, and traveler who want to escape from crowded cities.\r\n\r\nWake up to the sound of nature, watch sunrise and enjoy the incredible view overlooking the quiet forest hills from your bed. Our bamboo huts will make your experience perfect in Bali.\r\n\r\nThe space\r\nThere we have two separate buildings first building on top we use for the lobby and kitchen behind.\r\n\r\nThe second building is private room with two floors:\r\nOn the first floor, there we a open room with a king bed and the curtains if you need more privacy, there is also safety box in the cupboard, relaxing sofa with a table, coffee and tea you can serve at any time, a dispenser for hot water and a mineral water provided for free. Bathroom with a door made of bamboo and a shower under an open sky where you can take a shower at anytime while looking at the view of the sky and the stars.\r\n\r\nThere is a hammock in the corner of the pristine garden, a balcony complete with sofa and a dining table is provided in front of the room with an open view of the nature good for relaxing and working.\r\n\r\nOn the second floor there is a comfortable king bed to view the morning sunrise.\r\n\r\nSleeping arrangement:\r\nThe bamboo house sleep with 4 people, a king size bed at the first floor perfect for two and a loft king size bed for additional guest.\r\n\r\nIt is set amidst Balinese nature ;the perfect backdrop for nature lovers, story tellers, yogis, adventurers, art lovers and people who really want to have a unique experience in Bali.\r\n\r\nWe do have fiber WIFI in the property with 20Mbps and you will have full access to the internet during your stay. this is perfect for those who are travelling for work or even just a holiday.\r\n\r\nWe invite you to sit with us in the community area. Here, we help you organise your day and give you advises on places to visit and explore. We also provide scooters to rent an affordable cost, just ask us we are happy to assist you with anything you need.\r\n\r\nGuest access\r\nThe bamboo house is located on the edge of a hilly valley can be acces by the car or scooter and hidden in the forest.\r\n\r\nThere you can do many new experience like walking and tracking to the river, explore the traditional village with the local people, visit many nice garden and 15 minute from the house you will find the holy spring water and usually in the afternoon you can see many of local people taking a bath together like in the story of Bali long time ago that they still do in our village.\r\n\r\nOther things to note\r\nThis is a unique bamboo house in the midst of nature. And this is a place that is not for everyone who is afraid of nature. Because most likely you will adapt or see first hand some insects that exist in the natural world of Bali, but there is no need to worry because in Bali is a safe place and we do not have insects or poisonous spiders.You need to know the spider can make its nest very fast. And the only thing we can do is clean it every time and make sure you will feel free.\r\n\r\nA classic open room with walls made of woven bamboo will make your stay very comfortable and overlooking beautiful natural scenery right from the bedroom.\r\n\r\nWe provide a safety box in the room for your valuable or feel free to leave it with our staff if you would feel it more comfortable.\r\n\r\nBreakfast is served in the lobby in front of the room, and is right away deliver to the cottage upon you wake up.\r\n\r\nThis will make your holiday truly perfect.\r\n\r\nHere you can take a walk freely to enjoy the true nature of Bali while looking at various types of wild tropical animals such as lizards, ants, chickens, butterflies, dragonflies, as well.\r\n\r\nYou can hear as many species of birds singing while you are having breakfast.\r\n\r\nAt night you might see little lights flying around you, and yes, they are fireflies.\r\n\r\nThis place will make your holiday very special and comfortable in rural Bali.",
                ImagePaths =
                [
                    "hidden-bamboo-bali/hidden-bamboo-bali-0.jpg",
                    "hidden-bamboo-bali/hidden-bamboo-bali-2.jpg",
                    "hidden-bamboo-bali/hidden-bamboo-bali-1.jpg",
                    "hidden-bamboo-bali/hidden-bamboo-bali-3.jpg",
                    "hidden-bamboo-bali/hidden-bamboo-bali-4.jpg",
                    "hidden-bamboo-bali/hidden-bamboo-bali-5.jpg",
                    "hidden-bamboo-bali/hidden-bamboo-bali-6.jpg",
                    "hidden-bamboo-bali/hidden-bamboo-bali-7.jpg",
                    "hidden-bamboo-bali/hidden-bamboo-bali-8.jpg",
                    "hidden-bamboo-bali/hidden-bamboo-bali-9.jpg",
                    "hidden-bamboo-bali/hidden-bamboo-bali-10.jpg"
                ],
                DestinationTypes =
                [
                    DestinationType.AmazingViews,
                    DestinationType.Countryside,
                    DestinationType.Farm,
                    DestinationType.Treehouses,
                    DestinationType.Lakefront,
                    DestinationType.Tropical,
                    DestinationType.Room,
                ],
                Country = GetCountryByCode("ID"),
                StateOrProvince = "Bali",
                CityOrDistrict = "Tampaksiring",
                StreetAddress = "4394E Jalan Raya",
                PostalCode = "80552",
                PricePerNight = 199.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = -8.409518,
                Longitude = 115.188919,
                IsPriority = true,
                IsPetFriendly = true,
                MaxGuests = 4
            },
            new()
            {
                Id = 3,
                Name = "Nature stone house for rent in Siquijor",
                Type = PropertyType.Hotel,
                Description = "Casita Isabella, your chance to experience living in a tiny house on wheels. A serene place to escape the bustling and hustling of city life. Have a dip in our outdoor tub, light up a bonfire and make some smores, or just chill out and have a coffee or wine.\r\n\r\nPerfect for Staycation, Prenup Photoshoot, Birthday, and other Celebration.\r\n\r\n(Inquire about our prenup photoshoot rates before booking).\r\n\nThe space:\r\n\nESCAPTE TO TRANQUILITY AT CASITA ISABELLA:\r\nCasita Isabella is a charming off-grid tiny house on wheels, nestled in Tagaytay-Mendez, Cavite. Imagine waking up to breathtaking views of sprawling grasslands and a picturesque pineapple plantation – that's the magic that awaits you here.\r\n\r\nDESIGNED FOR COMFORT AND CONNECTION:\r\nStep inside and be greeted by a surprisingly spacious interior thanks to the clever angular design that creates lofty ceilings. Wood-engineered flooring provides a modern touch throughout the living area, kitchen, and bedroom. Relax on the comfy bean bag or unwind in the hammock that doubles as a daybed. Open the French windows and embrace the fresh air, blurring the lines between indoor and outdoor living. The strategically placed huge French windows and doors not only enhance the feeling of spaciousness but also connect you seamlessly with the natural beauty that surrounds you.\r\n\r\nA WELL-EQUIPPED KITCHENETTE:\r\nThe kitchenette is a delight for any home cook, featuring an acacia wood countertop, a sleek matte black tap and sink, and all the essentials you'll need to whip up delicious meals. A rice cooker, microwave oven, utensils, tableware, cookware, and a cleverly tucked-away refrigerator ensure you're fully equipped.\r\n\r\nA SERENE BEDROOM:\r\nRest and recharge in the cozy bedroom, furnished with a comfortable double bed, luxurious pillows, and a blanket to guarantee a restful sleep. Two large windows bathe the space in natural light and offer calming views of the outdoors. An extra bed is thoughtfully provided to accommodate additional guests.\r\n\r\nLUXURIOUS BATHROOM:\r\nA selection of high-end black matte materials that exude elegance. You'll find bath towels, bathroom tissue, and guest kit (shampoo, soap, toothbrush, toothpaste) for your convenience.\r\n\r\nCLIMATE-CONTROLLED COMFORT:\r\nRockwool insulation keeps you comfortable year-round. It helps regulate the temperature of the tiny house during the hot summer months and warm during the cooler seasons.\r\n\r\nRELAXATION OUTDOORS:\r\nEnjoy al fresco dining under the shade and protection of the provided awning, perfect for creating lasting memories with family and friends.\r\n\r\nSUSTAINABLE LIVING:\r\nCasita Isabella is powered by a responsible 1.5KW off-grid solar power system with a 24V 200AH battery capacity, ensuring you have enough power to run essential appliances throughout your stay, including during the night.\r\n\r\nBOOK YOUR UNFORTETTABLE tiny house getaway at Casita Isabella today!\r\n\nOther things to note\r\n\nMPORTANT NOTE: Please note that this is an off-grid unit with limited power that is powered by solar energy. For guests' convenience, we recommend preserving electricity and using it only when absolutely necessary (especially during the rainy season) to avoid power outages. Overall, minimal consumption is advised.\r\n\r\n- Fan room (enjoy Tagaytay weather)\r\n- The tiny house is fully powered by solar energy and can only supply electricity to essential appliances such as lights, fan, fridge, microwave, TV, etc.\r\n- No WiFi\r\n- Mobile reception is good for most networks (except for Sun network)\r\n- Firewood for your bonfire can be purchased directly from the caretaker. A bucket costs ₱200, but the price may fluctuate slightly depending on the supplier's cost.\r\n- Bring your own charcoal or purchase it here: Guests can bring their own charcoal or purchase it from Casita Isabella for ₱120 per kilo.\r\n- Lighting Up: To easily light your charcoal or bonfire, it's recommended that you bring a can of LPG/butane gas, we're happy to lend you a gas torch tool. Alternatively, you can also purchase one from Casita Isabella in case you forgot to bring one.\r\n- No smoking allowed inside the tiny house.\r\n- No unregistered guests or visitors are allowed\r\n- We would like to advise guests that longer stays or later check-outs will incur an additional charge\r\n\r\nOn Pets:\r\nDuring your stay, you and your pet(s) are allowed to experience enriching moments.\r\n\r\nWhile inside the tiny house property please be informed of the following:\r\n\r\n- For hygiene reasons, we kindly ask that pets don't get on the beds.\r\n- While we allow pets inside the tiny house, pet owners should provide the pets with beds and mats.\r\n- Pets must wear diapers at all times while inside the tiny house.\r\n- Please clean up after your pet’s shedding before checking out as a courtesy to other guests\r\n- Pets are allowed to stay in the garden or lawn, provided that their waste will be cleaned and will be disposed of by their owners in secured disposable bags. At all times, the guest shall maintain and keep the property in a good and sanitary condition.",
                ImagePaths =
                [
                    "nature-stone-house-siquijor/nature-stone-house-siquijor-0.jpg",
                    "nature-stone-house-siquijor/nature-stone-house-siquijor-2.jpg",
                    "nature-stone-house-siquijor/nature-stone-house-siquijor-1.jpg",
                    "nature-stone-house-siquijor/nature-stone-house-siquijor-3.jpg",
                    "nature-stone-house-siquijor/nature-stone-house-siquijor-4.jpg",
                    "nature-stone-house-siquijor/nature-stone-house-siquijor-5.jpg",
                    "nature-stone-house-siquijor/nature-stone-house-siquijor-6.jpg"
                ],
                DestinationTypes =
                [
                    DestinationType.Countryside,
                    DestinationType.Tropical,
                    DestinationType.Room,
                    DestinationType.Farm,
                    DestinationType.TinyHomes,
                ],
                Country = GetCountryByCode("PH"),
                StateOrProvince = "Central Visayas",
                CityOrDistrict = "Siquijor",
                StreetAddress = "Lazi",
                PostalCode = "6226",
                PricePerNight = 299.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 9.2219739,
                Longitude = 123.5347378,
                IsRequested = true,
                IsPetFriendly = true,
                MaxGuests = 6
            },
            new()
            {
                Id = 4,
                Name = "Abong 2 A-Frame House Great View",
                Type = PropertyType.Hotel,
                Description = "Escape to our A-Frame Houses with breathtaking views. Perched on a hill, each of our houses offers a cozy blend of modern living while waking up to panoramic views.\r\n\nEach unit has its own toilet & bath room. The private deck is perfect for coffee or stargazing.\r\n\nConveniently located near the city, our development promises a unique, tranquil & convenient retreat. Ideal for romantic getaways or family adventures, book your stay for a unique baguio stay soon! We look forward to hosting you.",
                ImagePaths =
                [
                    "abong-2-a-frame-house-great-view/abong-2-a-frame-house-great-view-0.jpg",
                    "abong-2-a-frame-house-great-view/abong-2-a-frame-house-great-view-1.jpg",
                    "abong-2-a-frame-house-great-view/abong-2-a-frame-house-great-view-2.jpg",
                    "abong-2-a-frame-house-great-view/abong-2-a-frame-house-great-view-3.jpg",
                    "abong-2-a-frame-house-great-view/abong-2-a-frame-house-great-view-4.jpg",
                    "abong-2-a-frame-house-great-view/abong-2-a-frame-house-great-view-5.jpg",
                    "abong-2-a-frame-house-great-view/abong-2-a-frame-house-great-view-6.jpg",
                    "abong-2-a-frame-house-great-view/abong-2-a-frame-house-great-view-7.jpg",
                    "abong-2-a-frame-house-great-view/abong-2-a-frame-house-great-view-8.jpg",
                    "abong-2-a-frame-house-great-view/abong-2-a-frame-house-great-view-9.jpg",
                    "abong-2-a-frame-house-great-view/abong-2-a-frame-house-great-view-10.jpg",
                    "abong-2-a-frame-house-great-view/abong-2-a-frame-house-great-view-11.jpg",
                    "abong-2-a-frame-house-great-view/abong-2-a-frame-house-great-view-12.jpg",
                    "abong-2-a-frame-house-great-view/abong-2-a-frame-house-great-view-13.jpg",
                    "abong-2-a-frame-house-great-view/abong-2-a-frame-house-great-view-14.jpg",
                    "abong-2-a-frame-house-great-view/abong-2-a-frame-house-great-view-15.jpg",
                    "abong-2-a-frame-house-great-view/abong-2-a-frame-house-great-view-16.jpg"
                ],
                DestinationTypes =
                [
                    DestinationType.AmazingViews,
                    DestinationType.Beach,
                    DestinationType.City,
                    DestinationType.TopCities,
                    DestinationType.Luxe,
                    DestinationType.Room,
                    DestinationType.TinyHomes
                ],
                Country = GetCountryByCode("PH"),
                StateOrProvince = "Cordillera",
                CityOrDistrict = "Baguio",
                StreetAddress = "29 Francisco St.",
                PostalCode = "2600",
                PricePerNight = 399.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 16.41639,
                Longitude = 120.59306,
                IsPriority = true,
                IsPetFriendly = false,
                MaxGuests = 2
            },
            new()
            {
                Id = 5,
                Name = "Simple and comfortable room in Florence",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths =
                [
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                ],
                DestinationTypes =
                [
                    DestinationType.City,
                    DestinationType.Room,
                    DestinationType.TinyHomes,
                    DestinationType.Mansions
                ],
                Country = GetCountryByCode("IT"),
                StateOrProvince = "Tuscany",
                CityOrDistrict = "Florence",
                StreetAddress = "Via dei Serragli",
                PostalCode = "50124",
                PricePerNight = 499.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 37.6062,
                Longitude = -50.3321,
                IsRequested = true,
                IsPetFriendly = false,
                MaxGuests = 2
            },
            new()
            {
                Id = 6,
                Name = "Cozy room in the Langen Reihe, St. Georg",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths =
                [
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                ],
                DestinationTypes =
                [
                    DestinationType.City,
                    DestinationType.Room,
                    DestinationType.TinyHomes,
                    DestinationType.Mansions
                ],
                Country = GetCountryByCode("DE"),
                StateOrProvince = "Hamburg",
                CityOrDistrict = "St. Georg",
                StreetAddress = "Langen Reihe",
                PostalCode = "20099",
                PricePerNight = 599.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 57.6062,
                Longitude = -42.3321,
                IsPriority = true,
                IsPetFriendly = true,
                MaxGuests = 2
            },
            new()
            {
                Id = 7,
                Name = "Spacious Double Room in a Modern Apartment",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths =
                [
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                ],
                DestinationTypes =
                [
                    DestinationType.AmazingPools,
                    DestinationType.City,
                    DestinationType.TopCities,
                    DestinationType.Luxe,
                    DestinationType.Room,
                    DestinationType.TinyHomes
                ],
                Country = GetCountryByCode("GB"),
                StateOrProvince = "Colchester",
                CityOrDistrict = "Essex",
                StreetAddress = "Colchester Town centre",
                PricePerNight = 699.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 77.6062,
                Longitude = -32.3321,
                IsPriority = true,
                IsPetFriendly = false,
                MaxGuests = 8
            },
            new()
            {
                Id = 8,
                Name = "Luxurious room and ensuite with large balcony",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths =
                [
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                ],
                DestinationTypes =
                [
                    DestinationType.AmazingViews,
                    DestinationType.City,
                    DestinationType.Luxe,
                    DestinationType.Room,
                    DestinationType.Mansions
                ],
                Country = GetCountryByCode("ZA"),
                StateOrProvince = "Western Cape",
                CityOrDistrict = "Cape Town",
                StreetAddress = "7th Floor, 106 Adderley Street",
                PostalCode = "8000",
                PricePerNight = 799.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 87.6062,
                Longitude = -12.3321,
                IsRequested = true,
                IsPetFriendly = false,
                MaxGuests = 10
            },
            new()
            {
                Id = 9,
                Name = "Cosy vibes in central Vesterbro",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths =
                [
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                ],
                DestinationTypes =
                [
                    DestinationType.AmazingViews,
                    DestinationType.City,
                    DestinationType.Room,
                    DestinationType.TinyHomes,
                    DestinationType.Mansions,
                    DestinationType.Lakefront,
                ],
                Country = GetCountryByCode("DK"),
                StateOrProvince = "Copenhagen",
                CityOrDistrict = "Vesterbro",
                StreetAddress = "119 Vesterbrogade",
                PostalCode = "1620",
                PricePerNight = 899.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 30.6062,
                Longitude = -22.3321,
                IsRequested = true,
                IsPetFriendly = false,
                MaxGuests = 4
            },
            new()
            {
                Id = 10,
                Name = "The Black Barn",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths =
                [
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                ],
                Country = GetCountryByCode("NZ"),
                DestinationTypes =
                [
                    DestinationType.AmazingPools,
                    DestinationType.Countryside,
                    DestinationType.Farm,
                    DestinationType.Treehouses,
                    DestinationType.Tropical,
                    DestinationType.Room,
                ],
                StateOrProvince = "Auckland",
                CityOrDistrict = "Kumeū",
                StreetAddress = "212 Access Road",
                PostalCode = "0891",
                PricePerNight = 999.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 72.6062,
                Longitude = 32.3321,
                IsRequested = true,
                IsPetFriendly = true,
                MaxGuests = 6
            },
            new()
            {
                Id = 11,
                Name = "Comfortable Private Room in huge central Apartment",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths =
                [
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                ],
                DestinationTypes =
                [
                    DestinationType.AmazingViews,
                    DestinationType.Farm,
                    DestinationType.Beach,
                    DestinationType.Tropical,
                    DestinationType.Room,
                ],
                Country = GetCountryByCode("AT"),
                StateOrProvince = "Vienna",
                CityOrDistrict = "Reumannplatz",
                StreetAddress = "1100 Favoritenstraße",
                PostalCode = "1100",
                PricePerNight = 1099.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 17.6062,
                Longitude = -42.3321,
                IsPriority = true,
                IsPetFriendly = false,
                MaxGuests = 4
            },
            new()
            {
                Id = 12,
                Name = "Stone-Walled Room",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths =
                [
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg"
                ],
                DestinationTypes =
                [
                    DestinationType.AmazingViews,
                    DestinationType.Countryside,
                    DestinationType.Treehouses,
                    DestinationType.Lakefront,
                    DestinationType.Tropical,
                    DestinationType.Room,
                ],
                Country = GetCountryByCode("CY"),
                StateOrProvince = "Nicosia",
                CityOrDistrict = "Nicosia",
                StreetAddress = "Pangea",
                PostalCode = "1100",
                PricePerNight = 1199.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 0.6062,
                Longitude = -2.3321,
                IsRequested = true,
                IsPetFriendly = true,
                MaxGuests = 2
            },
            new()
            {
                Id = 13,
                Name = "Inspiring Room with Workstation for Remote Workers",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths =
                [
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                ],
                DestinationTypes =
                [
                    DestinationType.AmazingViews,
                    DestinationType.Countryside,
                    DestinationType.Treehouses,
                    DestinationType.Lakefront,
                    DestinationType.Tropical,
                    DestinationType.Room,
                ],
                Country = GetCountryByCode("TR"),
                StateOrProvince = "Istanbul",
                CityOrDistrict = "Kadıköy",
                StreetAddress = "Kad ı Neyy",
                PostalCode = "34710",
                PricePerNight = 1299.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 50.6062,
                Longitude = 42.3321,
                IsPriority = true,
                IsPetFriendly = true,
                MaxGuests = 2
            },
            new()
            {
                Id = 14,
                Name = "Citygate condo at Kamala Beach",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths =
                [
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                ],
                DestinationTypes =
                [
                    DestinationType.AmazingViews,
                    DestinationType.Countryside,
                    DestinationType.Farm,
                    DestinationType.Treehouses,
                    DestinationType.Lakefront,
                    DestinationType.Tropical,
                    DestinationType.Room,
                ],
                Country = GetCountryByCode("TH"),
                StateOrProvince = "Phuket",
                CityOrDistrict = "Kamala",
                StreetAddress = "Citygate Condo",
                PostalCode = "83150",
                PricePerNight = 1399.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 6.6062,
                Longitude = -72.3321,
                IsPetFriendly = false,
                MaxGuests = 4
            },
            new()
            {
                Id = 15,
                Name = "Free pool 3 bedroom sea view Center",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths =
                [
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-2.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-0.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-1.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-3.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-4.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-5.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-6.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-7.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-8.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-9.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-10.jpg",
                    "cozy-room-in-montmartre/cozy-room-in-montmartre-11.jpg"
                ],
                DestinationTypes =
                [
                    DestinationType.AmazingViews,
                    DestinationType.Countryside,
                    DestinationType.Treehouses,
                    DestinationType.Lakefront,
                    DestinationType.Tropical,
                    DestinationType.Room,
                ],
                Country = GetCountryByCode("VN"),
                StateOrProvince = "Khanh Hoa",
                CityOrDistrict = "Nha Trang",
                StreetAddress = "28 Trần Phú",
                PostalCode = "650000",
                PricePerNight = 1499.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 57.6062,
                Longitude = -82.3321,
                IsPriority = true,
                IsPetFriendly = true,
                MaxGuests = 2
            }
        ]);
    }
}
