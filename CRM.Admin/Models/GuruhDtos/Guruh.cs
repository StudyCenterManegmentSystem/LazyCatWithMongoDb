
namespace CRM.Admin.Models.GuruhDtos;

public class Guruh
{
    public string Id { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public string? RoomId { get; set; }
    public string? FanId { get; set; }
    public string? TeacherId { get; set; }
    public List<int>? Weekdays { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public decimal Price { get; set; }
    public string? Duration { get; set; }
}
