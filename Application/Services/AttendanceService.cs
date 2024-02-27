using Application.Dtos.AttendanceDtos;
using Application.Dtos.GroupsDto;
using Domain.Entities.Entity.Attendances;

namespace Application.Services;

public class AttendanceService(IUnitOfWork unitOfWork) : IAttendanceService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Attendance> AddAttendanceAsync(AddAttendanceDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException(nameof(dto), "Attendance is null");
        }

        var attendance = (Attendance)dto;

        if (attendance.IsValid())
        {
            throw new CustomException("Attendance is invalid");
        }

        if (attendance.GroupId is null)
        {
            throw new CustomException("Attendance Group Id is null");
        }

        if (attendance.TalabaId is null)
        {
            throw new CustomException("Attendance Student Id is null");
        }

        if (!ObjectId.TryParse(dto.TalabaId, out ObjectId id))
        {
            throw new CustomException("Attendance TalabaId is not in ObjectId format");
        }

        if (!ObjectId.TryParse(dto.GroupId, out ObjectId objectId))
        {
            throw new CustomException("Attendance GroupId is not in ObjectId format");
        }

        var attendances = await _unitOfWork.AttendanceInterface.GetAllAsync();

        if (attendance.IsExist(attendances))
        {
            throw new CustomException("Attendance already exists");
        }

        await _unitOfWork.AttendanceInterface.AddAsync(attendance);
        return attendance;
    }

    public async Task<Attendance> DeleteAttendanceAsync(string id)
    {
        if (!ObjectId.TryParse(id, out ObjectId objectId))
        {
            throw new CustomException("AttendanceId is not in ObjectId format");
        }
        var attendace = await _unitOfWork.AttendanceInterface.GetByIdAsync(id);
        if(attendace is null)
        {
            throw new ArgumentNullException("Attendance does not found ");
        }
        await _unitOfWork.AttendanceInterface.DeleteAsync(id);
        return attendace;
    }

    public async Task<Attendance> UpdateAttendanceAsync(UpdateAttendanceDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException("Attendance is null here");
        }
        var attendance = (Attendance)dto;
        if (attendance.IsValid())
        {
            throw new CustomException("Attendance is invalid");
        }
        if (attendance.GroupId is null)
        {
            throw new CustomException("Attendance Group Id is null");
        }
        if (attendance.TalabaId is null)
        {
            throw new CustomException("Attendance Student Id is null");
        }
        if (!ObjectId.TryParse(dto.TalabaId, out ObjectId id))
        {
            throw new CustomException("Attendance TalabaId identifikatorlari ObjectId ko'rinishida emas");
        }
        if (!ObjectId.TryParse(dto.GroupId, out ObjectId objectId))
        {
            throw new CustomException("Attendace GroupId identifikatorlari ObjectId ko'rinishida emas");
        }

        await _unitOfWork.AttendanceInterface.UpdateAsync(attendance);
        return attendance;
    }
}
