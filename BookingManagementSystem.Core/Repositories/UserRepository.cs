using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Repositories;
public class UserRepository : Repository<User>
{
    public UserRepository()
    {
        _entities.AddRange(
        [
            new() { Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    ShortBio = "My name is John Doe",
                    Email = "johndoe@gmail.com",
                    Phone = "123-456-7890",
                    Address = "123 Main St",
                    GovernmentId = "1234567890",
                    Role = Role.Host,
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
                    Username = "elitehost2",
                    Password = "password"
                }
        ]);
    }
}
