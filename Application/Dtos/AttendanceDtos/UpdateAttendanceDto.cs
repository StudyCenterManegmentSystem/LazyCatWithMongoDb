using Domain.Entities.Entity.Attendances;

namespace Application.Dtos.AttendanceDtos;

public class UpdateAttendanceDto : BaseDto
{
    public string? TalabaId { get; set; }
    public DateTime Qachon { get; set; }
    public bool KeldiKemadi { get; set; } = true;
    public string? GroupId { get; set; }

    public static implicit operator UpdateAttendanceDto(Attendance attendance)
        => new UpdateAttendanceDto()
        {
            TalabaId = attendance.TalabaId,
            GroupId = attendance.GroupId,
            Qachon = attendance.Qachon,
            KeldiKemadi = attendance.KeldiKemadi
        };
    public static implicit operator Attendance(UpdateAttendanceDto dto)
        => new Attendance()
        {
            TalabaId = dto.TalabaId,
            GroupId = dto.GroupId,
            Qachon = dto.Qachon,
            KeldiKemadi = dto.KeldiKemadi
        };
}
