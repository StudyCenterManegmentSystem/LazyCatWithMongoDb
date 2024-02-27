using Application.Dtos.GroupsDto;
using Application.Dtos.StudentDtos;

namespace Application.Dtos.AttendanceDtos;

public class AttendanceReturnDto : BaseDto
{
    public StudentReturnDto? Student { get; set; } 
    public DateTime Qachon { get; set; }
    public bool KeldiKemadi { get; set; } = true;
    public GuruhReturnDto? Guruh { get; set; }
}
