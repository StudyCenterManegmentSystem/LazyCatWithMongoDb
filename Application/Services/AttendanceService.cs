using Application.Dtos.AttendanceDtos;
using Application.Dtos.GroupsDto;
using Application.Dtos.StudentDtos;
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

        var talaba = _unitOfWork.StudentInterface.GetByIdAsync(attendance.TalabaId);
        if (talaba is null)
        {
            throw new CustomException("Bunday talaba mavjud emas");
        }

        if (!ObjectId.TryParse(dto.GroupId, out ObjectId objectId))
        {
            throw new CustomException("Attendance GroupId is not in ObjectId format");
        }

        var guruh = await _unitOfWork.GuruhInterface.GetByIdAsync(attendance.GroupId);
        if (guruh is null)
        {
            throw new CustomException("Bunday Guruh mavjud emas");
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
        if (!ObjectId.TryParse(dto.Id, out ObjectId objectId1))
        {
            throw new CustomException("Attendance TalabaId identifikatorlari ObjectId ko'rinishida emas");
        }
        var existAttendance = await _unitOfWork.AttendanceInterface.GetByIdAsync(dto.Id);
        if(existAttendance is null)
        {
            throw new CustomException("Attendance does not found");
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

        var talaba = _unitOfWork.StudentInterface.GetByIdAsync(attendance.TalabaId);
        if (talaba is null)
        {
            throw new CustomException("Bunday talaba mavjud emas");
        }

        if (!ObjectId.TryParse(dto.GroupId, out ObjectId objectId))
        {
            throw new CustomException("Attendace GroupId identifikatorlari ObjectId ko'rinishida emas");
        }
        var guruh = await _unitOfWork.GuruhInterface.GetByIdAsync(attendance.GroupId);
        if(guruh is null)
        {
            throw new CustomException("Bunday Guruh mavjud emas");
        }

        await _unitOfWork.AttendanceInterface.UpdateAsync(attendance);
        return attendance;
    }

    public async Task<List<AttendanceReturnDto>> GetAllAttendanceAsync()
    {
        var attendances = await _unitOfWork.AttendanceInterface.GetAllAsync();
        List<AttendanceReturnDto> result = new();
        foreach (var attendance in attendances)
        {
            var talaba = await _unitOfWork.StudentInterface.GetByIdAsync(attendance.TalabaId!);
            var natija = new AttendanceReturnDto()
            {
                Id = attendance.Id,
                KeldiKemadi = attendance.KeldiKemadi,
                Qachon = attendance.Qachon,
                Student =(StudentAttendanceDto)talaba 
            };
            result.Add(natija);
        }
        return result;
    }
    public async Task<AttendanceReturnDto> GetByIdAttendanceAsync(string id)
    {
        if (!ObjectId.TryParse(id, out ObjectId objectId))
        {
            throw new CustomException("Attendance  identifikatorlari ObjectId ko'rinishida emas");
        }
        var existAttendance = await _unitOfWork.AttendanceInterface.GetByIdAsync(id);
        if(existAttendance is null)
        {
            throw new CustomException("Attendance does not found");
        }
        var attendance = await _unitOfWork.AttendanceInterface.GetByIdAsync(id);
        var talaba = await _unitOfWork.StudentInterface.GetByIdAsync(attendance.TalabaId!);
        var natija = new AttendanceReturnDto()
        {
            Id = attendance.Id,
            KeldiKemadi = attendance.KeldiKemadi,
            Qachon = attendance.Qachon,
            Student = (StudentAttendanceDto)talaba
        };
        
        return natija;
    }

}

