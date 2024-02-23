namespace Application.Dtos.FanDtos;

public class FanTeachersDto : BaseDto
{
    public string FanName { get; set; } = string.Empty;
    public string FanDescription { get; set; } = string.Empty;
    public List<TeacherReturnDto> Teachers { get; set; }
}
