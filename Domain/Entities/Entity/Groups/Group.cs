using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Entity.Groups;
public class Group : BaseEntity
{
    [Required, StringLength(100)]
    public string GroupName { get; set; } = string.Empty;
    public string? RoomId { get; set; }
    public string? FanId { get; set; }
    public string? TeacherId { get; set; }
    public List<int>? Weekdays { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required, StringLength(100)]
    public string? Duration { get; set; }
}
