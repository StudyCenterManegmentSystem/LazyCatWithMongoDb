using Domain.Entities.Entity.Groups;

namespace Application.Dtos.GroupsDto;

public class AddGroupDto
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

    public static implicit operator Guruh(AddGroupDto dto)
    {
        return new Guruh
        {
            GroupName = dto.GroupName,
            RoomId = dto.RoomId,
            FanId = dto.FanId,
            TeacherId = dto.TeacherId,
            Weekdays = dto.Weekdays,
            Start = dto.Start,
            End = dto.End,
            Price = dto.Price,
            Duration = dto.Duration
        };
    }
    public static implicit operator AddGroupDto(Guruh dto)
    {
        return new AddGroupDto
        {
            GroupName = dto.GroupName,
            RoomId = dto.RoomId,
            FanId = dto.FanId,
            TeacherId = dto.TeacherId,
            Weekdays = dto.Weekdays,
            Start = dto.Start,
            End = dto.End,
            Price = dto.Price,
            Duration = dto.Duration
        };
    }
}
