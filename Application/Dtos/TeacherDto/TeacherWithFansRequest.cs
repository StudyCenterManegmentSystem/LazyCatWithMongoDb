using Domain.Entities.Entity.Fans;

namespace Application.Dtos.TeacherDto;

public class TeacherWithFansRequest
{
    public TeacherReturnDto Teacher { get; set; }
    public List<Fan> Fans { get; set; }
}
