using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BookingManagementSystem.Core.Models;
public partial class PropertyPolicy
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsMandatory { get; set; }

    public int? PropertyId { get; set; }

    public virtual Property Property { get; set; }
}
