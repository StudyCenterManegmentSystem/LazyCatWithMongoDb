
namespace Domain.Entities.Entity.Attendances;

public class Attendance : BaseEntity
{
    [Required]
    public string? TalabaId { get; set; }
    public DateTime Qachon { get; set; }
    public bool KeldiKemadi { get; set; } = true;
    public string? GroupId { get; set; }

}
