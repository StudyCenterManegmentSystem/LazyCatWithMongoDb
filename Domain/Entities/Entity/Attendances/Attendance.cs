using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Entity.Attendances;

public class Attendance : BaseEntity
{
    [Required]
    public int TalabaId { get; set; }
    public DateTime Qachon { get; set; }
    public bool KeldiKemadi { get; set; } = true;
    public string? GroupId { get; set; }

}
