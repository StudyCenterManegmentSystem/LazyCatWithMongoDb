using Application.Dtos.AttendanceDtos;
using Domain.Entities.Entity.Attendances;

namespace Application.Services;

public class AttendanceService(IAttendanceInterface attendanceInterface) : IAttendanceService
{
    private readonly IAttendanceInterface _attendanceInterface = attendanceInterface;

    public Task<Attendance> AddAttendanceAsync(AddAttendanceDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Attendance> DeleteAttendanceAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Attendance> UpdateAttendanceAsync(UpdateAttendanceDto dto)
    {
        throw new NotImplementedException();
    }
}
