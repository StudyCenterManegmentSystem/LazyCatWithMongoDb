using Application.Dtos.AttendanceDtos;
using Domain.Entities.Entity.Attendances;

namespace Application.Interfaces;

public interface IAttendanceService
{
    Task<Attendance> AddAttendanceAsync(AddAttendanceDto dto);
    Task<Attendance> UpdateAttendanceAsync(UpdateAttendanceDto dto);
    Task<Attendance> DeleteAttendanceAsync(string id);

    Task<List<AttendanceReturnDto>> GetAllAttendanceAsync();
    Task<AttendanceReturnDto> GetByIdAttendanceAsync(string id);
}
