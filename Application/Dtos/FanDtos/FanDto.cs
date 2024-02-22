using Domain.Entities.Entity.Fans;

namespace Application.Dtos.FanDtos;

public class FanDto : BaseDto
{
    public string FanName { get; set; } = string.Empty;
    public string FanDescription { get; set; } = string.Empty;

    public static implicit operator FanDto(Fan fan)
    => new()
    {
        Id = fan.Id,
        FanName = fan.FanName,
        FanDescription = fan.FanDescription
    };

    public static implicit operator Fan(FanDto fan)
    => new()
    {
        Id = fan.Id,
        FanName = fan.FanName,
        FanDescription = fan.FanDescription
    };
}