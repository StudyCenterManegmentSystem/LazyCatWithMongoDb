using Domain.Entities.Entity.Fans;

 namespace Application.Dtos.FanDtos;

public class AddFanDto
{
    [Required, StringLength(50)]
    public string FanName { get; set; } = string.Empty;
    [Required, StringLength(200)]
    public string FanDescription { get; set; } = string.Empty;

    public static implicit operator Fan(AddFanDto dto)
    => new()
    {
        FanName = dto.FanName,
        FanDescription = dto.FanDescription
    };
}