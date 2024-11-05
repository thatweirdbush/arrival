using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Repositories;
public class PropertyPolicyRepository : Repository<PropertyPolicy>
{
    public PropertyPolicyRepository()
    {
        _entities.AddRange(
        [
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
        ]);
    }
}
