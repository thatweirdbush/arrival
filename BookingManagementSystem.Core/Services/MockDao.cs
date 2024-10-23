using System.Xml.Linq;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Services;
public class MockDao : IDao
{
    private static List<Amenity> _allAmenities;
    private static List<Booking> _allBookings;
    private static List<FAQ> _allFAQs;
    private static List<Notification> _allNotifications;
    private static List<Payment> _allPayments;
    private static List<Property> _allProperties;
    private static List<PropertyPolicy> _allPropertyPolicies;
    private static List<QnA> _allQnAs;
    private static List<Review> _allReviews;
    private static List<User> _allUsers;
    private static List<BadReport> _allBadReports;
    private static List<Voucher> _allVouchers;

    private static List<DestinationTypeSymbol> _allDestinationTypeSymbols;

    public MockDao()
    {
    }

    private static List<Amenity> AllAmenities()
    {
        return new List<Amenity>
        {
            new() { Id = 1,
                    Name = "Free Wi-Fi",
                    Description = "High-speed internet"
            },
            new() { Id = 2,
                    Name = "Free Parking",
                    Description = "On-site parking"
            },
            new() { Id = 3,
                    Name = "Swimming Pool",
                    Description = "Outdoor pool"
            },
            new() { Id = 4,
                    Name = "Gym",
                    Description = "Fitness center"
            },
            new() { Id = 5,
                    Name = "Restaurant",
                    Description = "On-site restaurant"
            }
        };
    }

    private static List<Booking> AllBookings()
    {
        return new List<Booking>
        {
            new() { Id = 1,
                    PropertyId = 1,
                    UserId = 1,
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(3),
                    TotalPrice = 299.9M
            },
            new() { Id = 2,
                    PropertyId = 2,
                    UserId = 2,
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(2),
                    TotalPrice = 199.9M
            },
            new() { Id = 3,
                    PropertyId = 3,
                    UserId = 3,
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(1),
                    TotalPrice = 49.9M
            },
            new() { Id = 4,
                    PropertyId = 4,
                    UserId = 4,
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(4),
                    TotalPrice = 99.9M
            },
            new() { Id = 5,
                    PropertyId = 5,
                    UserId = 5,
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(5),
                    TotalPrice = 69.9M
            }
        };
    }

    private static List<FAQ> AllFAQs()
    {
        return new List<FAQ>
        {
            new() { Id = 1,
                    Question = "What is the check-in time?",
                    Answer = "Check-in time is 3:00 PM",
                    FAQCategory = FAQCategory.General
            },
            new() { Id = 2,
                    Question = "What is the check-out time?",
                    Answer = "Check-out time is 11:00 AM",
                    FAQCategory = FAQCategory.General
            },
            new() { Id = 3,
                    Question = "Is breakfast included?",
                    Answer = "Yes, breakfast is included",
                    FAQCategory = FAQCategory.PropertyPolicies
            },
            new() { Id = 4,
                    Question = "Is there a gym?",
                    Answer = "Yes, there is a gym",
                    FAQCategory = FAQCategory.Booking
            },
            new() { Id = 5,
                    Question = "Is there a swimming pool?",
                    Answer = "Yes, there is a swimming pool",
                    FAQCategory = FAQCategory.Booking
            },
            new() { Id = 6,
                    Question = "What is the cancellation policy?",
                    Answer = "Free cancellation up to 24 hours before check-in",
                    FAQCategory = FAQCategory.Cancellations
            },
            new() { Id = 7,
                    Question = "What is the payment policy?",
                    Answer = "Payment is required at the time of booking",
                    FAQCategory = FAQCategory.Payment
            },
            new() { Id = 8,
                    Question = "What is the pet policy?",
                    Answer = "Pets are not allowed",
                    FAQCategory = FAQCategory.PropertyPolicies
            },
            new() { Id = 9,
                    Question = "What is the smoking policy?",
                    Answer = "Smoking is not allowed",
                    FAQCategory = FAQCategory.PropertyPolicies
            },
            new() { Id = 10,
                    Question = "Is there any fee for early check-in?",
                    Answer = "There is no fee for early check-in",
                    FAQCategory = FAQCategory.Pricing
            }
        };
    }

    private static List<Notification> AllNotifications()
    {
        return new List<Notification>
        {
            new() { Id = 1,
                    UserId = 1,
                    Message = "Your booking has been confirmed",
                    DateSent = DateTime.Now
            },
            new() { Id = 2,
                    UserId = 2,
                    Message = "Your booking has been canceled",
                    DateSent = DateTime.Now
            },
            new() { Id = 3,
                    UserId = 3,
                    Message = "Your booking is pending", DateSent
                    = DateTime.Now
            },
            new() { Id = 4,
                    UserId = 4,
                    Message = "Your booking has been confirmed",
                    DateSent = DateTime.Now
            },
            new() { Id = 5,
                    UserId = 5,
                    Message = "Your booking has been canceled",
                    DateSent = DateTime.Now
            }
        };
    }

    private static List<Payment> AllPayments()
    {
        return new List<Payment>
        {
            new() { Id = 1,
                    UserId = 1,
                    BookingId = 1,
                    Amount = 299.9M,
                    PaymentDate = DateTime.Now
            },
            new() { Id = 2,
                    UserId = 2,
                    BookingId = 2,
                    Amount = 199.9M,
                    PaymentDate = DateTime.Now
            },
            new() { Id = 3,
                    UserId = 3,
                    BookingId = 3,
                    Amount = 49.9M,
                    PaymentDate = DateTime.Now
            },
            new() { Id = 4,
                    UserId = 4,
                    BookingId = 4,
                    Amount = 99.9M,
                    PaymentDate = DateTime.Now
            },
            new() { Id = 5,
                    UserId = 5,
                    BookingId = 5,
                    Amount = 69.9M,
                    PaymentDate = DateTime.Now
            }
        };
    }

    private static List<Property> AllProperties()
    {
        return new List<Property>
        {
            new()
            {
                Id = 1,
                Name = "Cozy room in Montmartre",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
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
                },
                Location = "Paris, France",
                Capacity = 2,
                PricePerNight = 99.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 48.864716,
                Longitude = 2.349014,
                IsPriority = true
            },
            new()
            {
                Id = 2,
                Name = "Hidden Bamboo Bali",
                Type = PropertyType.Hotel,
                Description = "Hidden Bamboo Bali is the unique Eco Friendly Bamboo House in Bali, located in Tampakasing village which is 30 minute from Ubud city center and 1 hour 40 minute from Airport. a private house in the midle of nature which is good for nature lover, yoga, music, and traveler who want to escape from crowded cities. Wake up to the sound of nature, watch sunrise and enjoy the incredible view overlooking the quiet forest hills from your bed. Our bamboo huts will make your experience perfect in Bali.\r\nThe space\r\nThere we have two separate buildings first building on top we use for the lobby and kitchen behind.\r\nThe second building is private room with two floors:\r\nOn the first floor, there we a open room with a king bed and the curtains if you need more privacy , there is also safety box in the cupboard, relaxing sofa with a table, coffee and tea you can serve at any time, a dispenser for hot water and a mineral water provided for free. bathroom with a door made of bamboo and a shower under an open sky where you can take a shower at anytime while looking at the view of the sky and the stars.\r\nThere is a hammock in the corner of the pristine garden, a balcony complete with sofa and a dining table is provided in front of the room with an open view of the nature good for relaxing and working.\r\n\r\nOn the second floor there is a comfortable king bed to view the morning sunrise.\r\n\r\nSleeping arrangement:\r\nThe bamboo house sleep with 4 people, a king size bed at the first floor perfect for two and a loft king size bed for additional guest.\r\n\r\nIt is set amidst Balinese nature ;the perfect backdrop for nature lovers, story tellers, yogis, adventurers, art lovers and people who really want to have a unique experience in Bali.\r\n\r\nwe do have fiber WIFI in the property with 20Mbps and you will have full access to the internet during your stay. this is perfect for those who are travelling for work or even just a holiday.\r\n\r\nWe invite you to sit with us in the community area. Here, we help you organise your day and give you advises on places to visit and explore. We also provide scooters to rent an affordable cost, just ask us we are happy to assist you with anything you need.\r\nGuest access\r\nThe bamboo house is located on the edge of a hilly valley can be acces by the car or scooter and hidden in the forest.\r\nThere you can do many new experience like walking and tracking to the river , explore the traditional village with the local people, visit many nice garden and 15 minute from the house you will find the holy spring water and usually in the afternoon you can see many of local people taking a bath together like in the story of Bali long time ago that they still do in our village.\r\nOther things to note\r\nThis is a unique bamboo house in the midst of nature. And this is a place that is not for everyone who is afraid of nature. Because most likely you will adapt or see first hand some insects that exist in the natural world of Bali, but there is no need to worry because in Bali is a safe place and we do not have insects or poisonous spiders.You need to know the spider can make its nest very fast. And the only thing we can do is clean it every time and make sure you will feel free.\r\n\r\nA classic open room with walls made of woven bamboo will make your stay very comfortable and overlooking beautiful natural scenery right from the bedroom.\r\n\r\nWe provide a safety box in the room for your valuable or feel free to leave it with our staff if you would feel it more comfortable.\r\n\r\nBreakfast is served in the lobby in front of the room, and is right away deliver to the cottage upon you wake up.\r\n\r\nThis will make your holiday truly perfect.\r\n\r\nHere you can take a walk freely to enjoy the true nature of Bali while looking at various types of wild tropical animals such as lizards, ants, chickens, butterflies, dragonflies, as well.\r\nYou can hear as many species of birds singing while you are having breakfast.\r\n\r\nAt night you might see little lights flying around you, and yes, they are fireflies.\r\n\r\nThis place will make your holiday very special and comfortable in rural Bali.",
                ImagePaths = new List<string>
                {
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
                },
                Location = "Tampaksiring, Indonesia",
                Capacity = 4,
                PricePerNight = 199.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = -8.409518,
                Longitude = 115.188919,
                IsPriority = true
            },
            new()
            {
                Id = 3,
                Name = "Nature stone house for rent in Siquijor",
                Type = PropertyType.Hotel,
                Description = "Casita Isabella, your chance to experience living in a tiny house on wheels. A serene place to escape the bustling and hustling of city life. Have a dip in our outdoor tub, light up a bonfire and make some smores, or just chill out and have a coffee or wine.\r\n\r\nPerfect for Staycation, Prenup Photoshoot, Birthday, and other Celebration.\r\n\r\n(Inquire about our prenup photoshoot rates before booking).\r\n\nThe space:\r\n\nESCAPTE TO TRANQUILITY AT CASITA ISABELLA:\r\nCasita Isabella is a charming off-grid tiny house on wheels, nestled in Tagaytay-Mendez, Cavite. Imagine waking up to breathtaking views of sprawling grasslands and a picturesque pineapple plantation – that's the magic that awaits you here.\r\n\r\nDESIGNED FOR COMFORT AND CONNECTION:\r\nStep inside and be greeted by a surprisingly spacious interior thanks to the clever angular design that creates lofty ceilings. Wood-engineered flooring provides a modern touch throughout the living area, kitchen, and bedroom. Relax on the comfy bean bag or unwind in the hammock that doubles as a daybed. Open the French windows and embrace the fresh air, blurring the lines between indoor and outdoor living. The strategically placed huge French windows and doors not only enhance the feeling of spaciousness but also connect you seamlessly with the natural beauty that surrounds you.\r\n\r\nA WELL-EQUIPPED KITCHENETTE:\r\nThe kitchenette is a delight for any home cook, featuring an acacia wood countertop, a sleek matte black tap and sink, and all the essentials you'll need to whip up delicious meals. A rice cooker, microwave oven, utensils, tableware, cookware, and a cleverly tucked-away refrigerator ensure you're fully equipped.\r\n\r\nA SERENE BEDROOM:\r\nRest and recharge in the cozy bedroom, furnished with a comfortable double bed, luxurious pillows, and a blanket to guarantee a restful sleep. Two large windows bathe the space in natural light and offer calming views of the outdoors. An extra bed is thoughtfully provided to accommodate additional guests.\r\n\r\nLUXURIOUS BATHROOM:\r\nA selection of high-end black matte materials that exude elegance. You'll find bath towels, bathroom tissue, and guest kit (shampoo, soap, toothbrush, toothpaste) for your convenience.\r\n\r\nCLIMATE-CONTROLLED COMFORT:\r\nRockwool insulation keeps you comfortable year-round. It helps regulate the temperature of the tiny house during the hot summer months and warm during the cooler seasons.\r\n\r\nRELAXATION OUTDOORS:\r\nEnjoy al fresco dining under the shade and protection of the provided awning, perfect for creating lasting memories with family and friends.\r\n\r\nSUSTAINABLE LIVING:\r\nCasita Isabella is powered by a responsible 1.5KW off-grid solar power system with a 24V 200AH battery capacity, ensuring you have enough power to run essential appliances throughout your stay, including during the night.\r\n\r\nBOOK YOUR UNFORTETTABLE tiny house getaway at Casita Isabella today!\r\n\nOther things to note\r\n\nMPORTANT NOTE: Please note that this is an off-grid unit with limited power that is powered by solar energy. For guests' convenience, we recommend preserving electricity and using it only when absolutely necessary (especially during the rainy season) to avoid power outages. Overall, minimal consumption is advised.\r\n\r\n- Fan room (enjoy Tagaytay weather)\r\n- The tiny house is fully powered by solar energy and can only supply electricity to essential appliances such as lights, fan, fridge, microwave, TV, etc.\r\n- No WiFi\r\n- Mobile reception is good for most networks (except for Sun network)\r\n- Firewood for your bonfire can be purchased directly from the caretaker. A bucket costs ₱200, but the price may fluctuate slightly depending on the supplier's cost.\r\n- Bring your own charcoal or purchase it here: Guests can bring their own charcoal or purchase it from Casita Isabella for ₱120 per kilo.\r\n- Lighting Up: To easily light your charcoal or bonfire, it's recommended that you bring a can of LPG/butane gas, we're happy to lend you a gas torch tool. Alternatively, you can also purchase one from Casita Isabella in case you forgot to bring one.\r\n- No smoking allowed inside the tiny house.\r\n- No unregistered guests or visitors are allowed\r\n- We would like to advise guests that longer stays or later check-outs will incur an additional charge\r\n\r\nOn Pets:\r\nDuring your stay, you and your pet(s) are allowed to experience enriching moments.\r\n\r\nWhile inside the tiny house property please be informed of the following:\r\n\r\n- For hygiene reasons, we kindly ask that pets don't get on the beds.\r\n- While we allow pets inside the tiny house, pet owners should provide the pets with beds and mats.\r\n- Pets must wear diapers at all times while inside the tiny house.\r\n- Please clean up after your pet’s shedding before checking out as a courtesy to other guests\r\n- Pets are allowed to stay in the garden or lawn, provided that their waste will be cleaned and will be disposed of by their owners in secured disposable bags. At all times, the guest shall maintain and keep the property in a good and sanitary condition.",
                ImagePaths = new List<string>
                {
                    "nature-stone-house-siquijor/nature-stone-house-siquijor-0.jpg",
                    "nature-stone-house-siquijor/nature-stone-house-siquijor-2.jpg",
                    "nature-stone-house-siquijor/nature-stone-house-siquijor-1.jpg",
                    "nature-stone-house-siquijor/nature-stone-house-siquijor-3.jpg",
                    "nature-stone-house-siquijor/nature-stone-house-siquijor-4.jpg",
                    "nature-stone-house-siquijor/nature-stone-house-siquijor-5.jpg",
                    "nature-stone-house-siquijor/nature-stone-house-siquijor-6.jpg"
                },
                Location = "Siquijor, Philippines",
                Capacity = 6,
                PricePerNight = 299.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 9.2219739,
                Longitude = 123.5347378
            },
            new()
            {
                Id = 4,
                Name = "Abong 2 A-Frame House Great View",
                Type = PropertyType.Hotel,
                Description = "Escape to our A-Frame Houses with breathtaking views. Perched on a hill, each of our houses offers a cozy blend of modern living while waking up to panoramic views.\r\n\nEach unit has its own toilet & bath room. The private deck is perfect for coffee or stargazing.\r\n\nConveniently located near the city, our development promises a unique, tranquil & convenient retreat. Ideal for romantic getaways or family adventures, book your stay for a unique baguio stay soon! We look forward to hosting you.",
                ImagePaths = new List<string>
                {
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
                },
                Location = "Baguio, Philippines",
                Capacity = 8,
                PricePerNight = 399.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 16.41639,
                Longitude = 120.59306,
                IsPriority = true
            },
            new()
            {
                Id = 5,
                Name = "Property 5",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
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
                },
                Location = "Location 5",
                Capacity = 10,
                PricePerNight = 499.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 37.6062,
                Longitude = -50.3321
            },
            new()
            {
                Id = 6,
                Name = "Property 6",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
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
                },
                Location = "Location 6",
                Capacity = 12,
                PricePerNight = 599.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 57.6062,
                Longitude = -42.3321,
                IsPriority = true
            },
            new()
            {
                Id = 7,
                Name = "Property 7",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
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
                },
                Location = "Location 7",
                Capacity = 14,
                PricePerNight = 699.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 77.6062,
                Longitude = -32.3321,
                IsPriority = true
            },
            new()
            {
                Id = 8,
                Name = "Property 8",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
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
                },
                Location = "Location 8",
                Capacity = 16,
                PricePerNight = 799.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 87.6062,
                Longitude = -12.3321
            },
            new()
            {
                Id = 9,
                Name = "Property 9",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
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
                },
                Location = "Location 9",
                Capacity = 18,
                PricePerNight = 899.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 30.6062,
                Longitude = -22.3321
            },
            new()
            {
                Id = 10,
                Name = "Property 10",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
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
                },
                Location = "Location 10",
                Capacity = 20,
                PricePerNight = 999.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 72.6062,
                Longitude = 32.3321
            },
            new()
            {
                Id = 11,
                Name = "Property 11",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
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
                },
                Location = "Location 11",
                Capacity = 22,
                PricePerNight = 1099.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 17.6062,
                Longitude = -42.3321,
                IsPriority = true
            },
            new()
            {
                Id = 12,
                Name = "Property 12",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
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
                },
                Location = "Location 12",
                Capacity = 24,
                PricePerNight = 1199.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 0.6062,
                Longitude = -2.3321
            },
            new()
            {
                Id = 13,
                Name = "Property 13",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
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
                },
                Location = "Location 13",
                Capacity = 26,
                PricePerNight = 1299.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 50.6062,
                Longitude = 42.3321,
                IsPriority = true
            },
            new()
            {
                Id = 14,
                Name = "Property 14",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
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
                },
                Location = "Location 14",
                Capacity = 28,
                PricePerNight = 1399.9M,
                IsFavourite = true,
                IsAvailable = true,
                Latitude = 6.6062,
                Longitude = -72.3321
            },
            new()
            {
                Id = 15,
                Name = "Property 15",
                Type = PropertyType.Hotel,
                Description = "At the foot of the Montmartre hill, you will find everything in this historic neighborhood.\r\nAt 200m, at the top of the steps, you will find a unique view of Montmartre.\r\n\r\nAccess by line 12 Lamarck Station\r\nOr line 4 Station Château Rouge\r\n\r\nThe space\r\nA bedroom, like a cocoon\r\nIn a very pleasant and bright apartment, located at the foot of the Montmartre hill.\r\nThe kitchen and the bathroom are shared spaces.\r\n\r\nGuest access\r\nElevator\r\n\r\nOther things to note\r\nI can only accept people who have reviews/history on Air bnb",
                ImagePaths = new List<string>
                {
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
                },
                Location = "Location 15",
                Capacity = 30,
                PricePerNight = 1499.9M,
                IsFavourite = false,
                IsAvailable = true,
                Latitude = 57.6062,
                Longitude = -82.3321,
                IsPriority = true
            }
        };
    }

    private static List<PropertyPolicy> AllPropertyPolicies()
    {
        return new List<PropertyPolicy>
        {
            new() { Id = 1,
                    Name = "No smoking",
                    Description = "Smoking is not allowed",
                    IsMandatory = true
            },
            new() { Id = 2,
                    Name = "No pets",
                    Description = "Pets are not allowed",
                    IsMandatory = true
            },
            new() { Id = 3,
                    Name = "No parties",
                    Description = "Parties are not allowed",
                    IsMandatory = true
            },
            new() { Id = 4,
                    Name = "No loud music",
                    Description = "Loud music is not allowed",
                    IsMandatory = false
            },
            new() { Id = 5,
                    Name = "No outside food",
                    Description = "Outside food is not allowed",
                    IsMandatory = false
            },
            new() { Id = 6,
                    Name = "No outside drinks",
                    Description = "Outside drinks are not allowed",
                    IsMandatory = false
            },
            new() { Id = 7,
                    Name = "No smoking in rooms",
                    Description = "Smoking is not allowed in rooms",
                    IsMandatory = true
            },
            new() { Id = 8,
                    Name = "No pets in rooms",
                    Description = "Pets are not allowed in rooms",
                    IsMandatory = true
            },
            new() { Id = 9,
                    Name = "No parties in rooms",
                    Description = "Parties are not allowed in rooms",
                    IsMandatory = true
            },
            new() { Id = 10,
                     Name = "No loud music in rooms",
                     Description = "Loud music is not allowed in rooms",
                     IsMandatory = false
            }
        };
    }

    private static List<QnA> AllQnAs()
    {
        return new List<QnA>
        {
            new() { Id = 1,
                    Question = "Do they serve breakfast?",
                    Answer = "There are breakfast options available.",
                    PropertyId = 1,
                    CustomerId = 1,
                    HostId = 2,
                    CreatedAt = DateTime.Now,
                    Status = QnAStatus.Answered
            },
            new() { Id = 2,
                    Question = "How do i book with breakfast?",
                    Answer = "Dear Guest\r\nWhen you make a booking you can choose included breakfast or only room.\r\nAnd also we have restaurant open 7.00 A.M. - 19.00 P.M.\r\nRegards\r\nMr.Key",
                    PropertyId = 2,
                    CustomerId = 2,
                    HostId = 3,
                    CreatedAt = DateTime.Now,
                    Status = QnAStatus.Answered
            },
            new() { Id = 3,
                    Question = "Can I park there?",
                    Answer = "This is what Lamphuhouse Bangkok - SHA Extra Plus Certified says about parking:\r\nNo parking available.",
                    PropertyId = 3,
                    CustomerId = 3,
                    HostId = 4,
                    CreatedAt = DateTime.Now,
                    Status = QnAStatus.Answered
            },
            new() { Id = 4,
                    Question = "Are there rooms with a hot tub?",
                    Answer = "No we don't have rooms with a hot tub.",
                    PropertyId = 4,
                    CustomerId = 4,
                    HostId = 5,
                    CreatedAt = DateTime.Now,
                    Status = QnAStatus.Answered
            },
            new() { Id = 5,
                    Question = "What are the check-in and check-out times?",
                    Answer = "✓ Check-in from 14:00 ✓ Check-out until 11:30\r\nIf you'd like to request an early or late check-in or check-out, you can make a special request when you book.\r\nNote: Special requests can't be guaranteed. If early or late check-in or check-out is essential to your travel plans, check the cancellation options before booking.",
                    PropertyId = 5,
                    CustomerId = 5,
                    HostId = 1,
                    CreatedAt = DateTime.Now,
                    Status = QnAStatus.Answered
            },
            new() { Id = 6,
                    Question = "Hi there just wondering if  we can smoke on Balcony?",
                    Answer = "Dear Guest\r\nYou can smoke on balcony but in the room not allow.\r\nRegards\r\nMr.Key",
                    PropertyId = 6,
                    CustomerId = 6,
                    HostId = 6,
                    CreatedAt = DateTime.Now,
                    Status = QnAStatus.Answered
            }
        };
    }

    private static List<Review> AllReviews()
    {
        return new List<Review>
        {
            new() { Id = 1,
                    PropertyId = 1,
                    UserId = 1,
                    Rating = 5,
                    Comment = "Excellent property",
                    CreatedAt = DateTime.Now
            },
            new() { Id = 2,
                    PropertyId = 2,
                    UserId = 2,
                    Rating = 4,
                    Comment = "Great property",
                    CreatedAt = DateTime.Now
            },
            new() { Id = 3,
                    PropertyId = 3,
                    UserId = 3,
                    Rating = 3,
                    Comment = "Good property",
                    CreatedAt = DateTime.Now
            },
            new() { Id = 4,
                    PropertyId = 4,
                    UserId = 4,
                    Rating = 2,
                    Comment = "Average property",
                    CreatedAt = DateTime.Now
            },
            new() { Id = 5,
                    PropertyId = 5,
                    UserId = 5,
                    Rating = 1,
                    Comment = "Poor property",
                    CreatedAt = DateTime.Now
            }
        };
    }

    private static List<User> AllUsers()
    {
        return new List<User>
        {
            new() { Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    ShortBio = "My name is John Doe",
                    Email = "johndoe@gmail.com",
                    Phone = "123-456-7890",
                    Address = "123 Main St",
                    GovernmentId = "1234567890",
                    Role = Role.Host,
                    CreatedAt = DateTime.Now,
                    Username = "johndoe",
                    Password = "password"
            },
            new() { Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    ShortBio = "My name is Jane Smith",
                    Email = "janesmith@gmail.com",
                    Phone = "234-567-8901",
                    Address = "234 Main St",
                    GovernmentId = "2345678901",
                    Role = Role.Guest,
                    CreatedAt = DateTime.Now,
                    Username = "janesmith",
                    Password = "password"
                },
            new() { Id = 3,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    ShortBio = "My name is Alice Johnson",
                    Email = "alicejs@gmail.com",
                    Phone = "345-678-9012",
                    Address = "345 Main St",
                    GovernmentId = "3456789012",
                    Role = Role.Host,
                    CreatedAt = DateTime.Now,
                    Username = "alicejohnson",
                    Password = "password"
                },
            new() { Id = 4,
                    FirstName = "Bob",
                    LastName = "Brown",
                    ShortBio = "My name is Bob Brown",
                    Email = "bobbrown@gmail.com",
                    Phone = "456-789-0123",
                    Address = "456 Main St",
                    GovernmentId = "4567890123",
                    Role = Role.Guest,
                    CreatedAt = DateTime.Now,
                    Username = "bobbrown",
                    Password = "password"
                },
            new() { Id = 5,
                    FirstName = "Eve",
                    LastName = "White",
                    ShortBio = "My name is Eve White",
                    Email = "evewhite@gmail.com",
                    Phone = "567-890-1234",
                    Address = "567 Main St",
                    GovernmentId = "5678901234",
                    Role = Role.Host,
                    CreatedAt = DateTime.Now,
                    Username = "evewhite",
                    Password = "password"
                },
            new() { Id = 6,
                    FirstName = "Admin",
                    LastName = "Admin",
                    ShortBio = "My name is Admin",
                    Email = "admin@gmail.com",
                    Phone = "678-901-2345",
                    Address = "678 Main St",
                    GovernmentId = "6789012345",
                    Role = Role.Admin,
                    CreatedAt = DateTime.Now,
                    Username = "admin",
                    Password = "password"
                },
            new() { Id = 7,
                    FirstName = "Host",
                    LastName = "Host",
                    ShortBio = "My name is Host",
                    Email = "host@gmail.com",
                    Phone = "789-012-3456",
                    Address = "789 Main St",
                    GovernmentId = "7890123456",
                    Role = Role.Host,
                    CreatedAt = DateTime.Now,
                    Username = "host",
                    Password = "password"
                },
            new() { Id = 8,
                    FirstName = "Guest",
                    LastName = "Guest",
                    ShortBio = "My name is Guest",
                    Email = "guest@gmail.com",
                    Phone = "890-123-4567",
                    Address = "890 Main St",
                    GovernmentId = "8901234567",
                    Role = Role.Guest,
                    CreatedAt = DateTime.Now,
                    Username = "guest",
                    Password = "password"
                },
            new() { Id = 9,
                    FirstName = "Elite",
                    LastName = "Host",
                    ShortBio = "My name is Elite Host",
                    Email = "elite@gmail.com",
                    Phone = "901-234-5678",
                    Address = "901 Main St",
                    GovernmentId = "9012345678",
                    Role = Role.Host,
                    CreatedAt = DateTime.Now,
                    Username = "elitehost",
                    Password = "password"
                },
            new() { Id = 10,
                    FirstName = "Elite Two",
                    LastName = "Host",
                    ShortBio = "My name is Elite Host",
                    Email = "elite2@gmail.com",
                    Phone = "012-345-6789",
                    Address = "012 Main St",
                    GovernmentId = "0123456789",
                    Role = Role.Host,
                    CreatedAt = DateTime.Now,
                    Username = "elitehost2",
                    Password = "password"
                }
        };
    }

    private static List<DestinationTypeSymbol> AllDestinationTypeSymbols()
    {
        return new List<DestinationTypeSymbol>
        {
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
        };
    }

    private static List<BadReport> AllBadReports()
    {
        return new List<BadReport>
        {
            new() { Id = 1,
                    UserId = 1,
                    ReportReason = "Reason 1",
                    Description = "Description 1",
                    Status = ReportStatus.Approved,
                    EntityType= EntityType.Property
            },
            new() { Id = 2,
                    UserId = 2,
                    ReportReason = "Reason 2",
                    Description = "Description 2",
                    Status = ReportStatus.Approved,
                    EntityType= EntityType.User
            },
            new() { Id = 3,
                    UserId = 3,
                    ReportReason = "Reason 3",
                    Description = "Description 3",
                    Status = ReportStatus.Pending,
                    EntityType= EntityType.Property
            },
            new() { Id = 4,
                    UserId = 4,
                    ReportReason = "Reason 4",
                    Description = "Description 4",
                    Status = ReportStatus.Rejected,
                    EntityType= EntityType.User
            },
            new() { Id = 5,
                    UserId = 5,
                    ReportReason = "Reason 5",
                    Description = "Description 5",
                    Status = ReportStatus.Pending,
                    EntityType= EntityType.Review
            }
        };
    }

    private static List<Voucher> AllVouchers()
    {
        return new List<Voucher>
        {
            new() { Id = 1,
                    Code = "Voucher 1",
                    DiscountPercentage = 10,
                    ValidUntil = DateTime.Now.AddDays(30),
                    IsUsed = true
            },
            new() { Id = 2,
                    Code = "Voucher 2",
                    DiscountPercentage = 20,
                    ValidUntil = DateTime.Now.AddDays(60),
                    IsUsed = false
            },
            new() { Id = 3,
                    Code = "Voucher 3",
                    DiscountPercentage = 30,
                    ValidUntil = DateTime.Now.AddDays(90),
                    IsUsed = false
            },
            new() { Id = 4,
                    Code = "Voucher 4",
                    DiscountPercentage = 40,
                    ValidUntil = DateTime.Now.AddDays(120),
                    IsUsed = false
            },
            new() { Id = 5,
                    Code = "Voucher 5",
                    DiscountPercentage = 50,
                    ValidUntil = DateTime.Now.AddDays(150),
                    IsUsed = true
            }
        };
    }
    
    public async Task<IEnumerable<Amenity>> GetAmenityListDataAsync()
    {
        _allAmenities ??= AllAmenities();

        await Task.CompletedTask;
        return _allAmenities;
    }

    public async Task<IEnumerable<Booking>> GetBookingListDataAsync()
    {
        _allBookings ??= AllBookings();

        await Task.CompletedTask;
        return _allBookings;
    }

    public async Task<IEnumerable<FAQ>> GetFAQListDataAsync()
    {
        _allFAQs ??= AllFAQs();

        await Task.CompletedTask;
        return _allFAQs;
    }

    public async Task<IEnumerable<Notification>> GetNotificationListDataAsync()
    {
        _allNotifications ??= AllNotifications();

        await Task.CompletedTask;
        return _allNotifications;
    }

    public async Task<IEnumerable<Payment>> GetPaymentListDataAsync()
    {
        _allPayments ??= AllPayments();

        await Task.CompletedTask;
        return _allPayments;
    }

    public async Task<IEnumerable<Property>> GetPropertyListDataAsync()
    {
        _allProperties ??= AllProperties();

        await Task.CompletedTask;
        return _allProperties;
    }

    public async Task<IEnumerable<PropertyPolicy>> GetPropertyPolicyListDataAsync()
    {
        _allPropertyPolicies ??= AllPropertyPolicies();

        await Task.CompletedTask;
        return _allPropertyPolicies;
    }

    public async Task<IEnumerable<QnA>> GetQnAListDataAsync()
    {
        _allQnAs ??= AllQnAs();

        await Task.CompletedTask;
        return _allQnAs;
    }

    public async Task<IEnumerable<Review>> GetReviewListDataAsync()
    {
        _allReviews ??= AllReviews();

        await Task.CompletedTask;
        return _allReviews;
    }

    public async Task<IEnumerable<User>> GetUserListDataAsync()
    {
        _allUsers ??= AllUsers();

        await Task.CompletedTask;
        return _allUsers;
    }

    public async Task<IEnumerable<DestinationTypeSymbol>> GetDestinationTypeSymbolDataAsync()
    {
        _allDestinationTypeSymbols ??= AllDestinationTypeSymbols();

        await Task.CompletedTask;
        return _allDestinationTypeSymbols;
    }

    public async Task<IEnumerable<BadReport>> GetBadReportListDataAsync()
    {
        _allBadReports ??= AllBadReports();

        await Task.CompletedTask;
        return _allBadReports;
    }

    public async Task<IEnumerable<Voucher>> GetVoucherListDataAsync()
    {
        _allVouchers ??= AllVouchers();

        await Task.CompletedTask;
        return _allVouchers;
    }
}
