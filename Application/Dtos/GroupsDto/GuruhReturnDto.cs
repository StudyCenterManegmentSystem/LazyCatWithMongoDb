namespace Application.Dtos.GroupsDto;

public class GuruhReturnDto
{
    [Key, Required]
    public string Id { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string GroupName { get; set; } = string.Empty;
    public List<int>? Weekdays { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required, StringLength(100)]
    public string? Duration { get; set; }
    public RoomDto? Room { get; set; }
    public TeacherReturnDto? Teacher { get; set; }
    public FanDto? Fan { get; set; }
}
