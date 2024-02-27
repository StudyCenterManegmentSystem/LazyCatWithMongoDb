using Domain.Entities.Entity.Attendances;

namespace Application.Dtos.AttendanceDtos;

public class AddAttendanceDto
{
    public string? TalabaId { get; set; }
    public DateTime Qachon { get; set; }
    public bool KeldiKemadi { get; set; } = true;
    public string? GroupId { get; set; }

    public static implicit operator Attendance(AddAttendanceDto dto)
        => new Attendance()
        {
            TalabaId = dto.TalabaId,
            GroupId = dto.GroupId,
            Qachon = dto.Qachon,
            KeldiKemadi = dto.KeldiKemadi
        };
    public static implicit operator AddAttendanceDto(Attendance attendance)
        => new AddAttendanceDto()
        {
            TalabaId = attendance.TalabaId,
            GroupId = attendance.GroupId,
            Qachon = attendance.Qachon,
            KeldiKemadi = attendance.KeldiKemadi
        };
}
