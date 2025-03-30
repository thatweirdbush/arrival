using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementSystem.Core.Models;
public partial class PropertyPolicy
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsMandatory { get; set; }

    public int? PropertyId { get; set; }

    public virtual Property Property { get; set; }
}
