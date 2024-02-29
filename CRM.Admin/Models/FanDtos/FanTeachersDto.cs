using CRM.Admin.Models.TeacherDtos;

namespace CRM.Admin.Models.FanDtos
{
    public class FanTeachersDto
    {
        public string Id { get; set; } = string.Empty;
        public string FanName { get; set; } = string.Empty;
        public string FanDescription { get; set; } = string.Empty;
        public List<TeacherReturnDto> Teachers { get; set; }
    }
}
