using Domain.Entities.Entity.Fans;

namespace Application.Dtos.TeacherDto
{
    public class TeacherWithFansRequest
    {
        public TeacherDto Teacher { get; set; }
        public List<Fan> Fans { get; set; }
    }
}
