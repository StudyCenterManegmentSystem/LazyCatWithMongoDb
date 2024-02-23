using Application.Dtos.GroupsDto;
using Application.Dtos.StudentDtos;
using Domain.Entities.Entity.Students;

namespace Application.Services;

public class StudentService (IUnitOfWork unitOfWork, 
                             UserManager<Teacher> userManager) : IStudentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<Teacher> _userManager = userManager;

    public async Task<Student> AddStudentAsync(AddStudentDto dto)
    {
        if(dto is null)
        {
            throw new ArgumentNullException("StudentDto is null");
        }
        var student = (Student)dto;
        if(student is null)
        {
            throw new ArgumentNullException("Student is null");
        }
        var guruhIds = student.GruopIds;
        foreach (var item in guruhIds)
        {
            var guruh = await _unitOfWork.GuruhInterface.GetByIdAsync(item);
            if(guruh is null)
            {
                throw new CustomException("Bunday guruh mavjud emas");
            }
        }
        if (!student.IsValid())
        {
            throw new CustomException("Student is invalid");
        }
        var students = await _unitOfWork.StudentInterface.GetAllAsync();

        if (student.IsExist(students))
        {
            throw new CustomException("Student is already exist");
        }
        await _unitOfWork.StudentInterface.AddAsync(student);
        return student;
    }

    public async Task<Student> UpdateStudentAsync(UpdateStudentDto dto)
    {
    
        if (dto is null)
        {
            throw new ArgumentNullException("UpdateStudentDto is null");
        }
        var student = (Student)dto;
        var existingStudent = await _unitOfWork.StudentInterface.GetByIdAsync(student.Id);
        if (existingStudent is null)
        {
            throw new CustomException("Student not found");
        }
        if (!student.IsValid())
        {
            throw new CustomException("Updated student is invalid");
        }

        var students = await _unitOfWork.StudentInterface.GetAllAsync();
        if (student.IsExist(students))
        {
            throw new CustomException("Updated student already exists");
        }

        await _unitOfWork.StudentInterface.UpdateAsync(student);
        return existingStudent;
    }


    public async Task DeleteStudentAsync(string studentId)
    {
        if (!ObjectId.TryParse(studentId, out ObjectId objectId))
        {
            throw new CustomException("Fan identifikatorlari ObjectId ko'rinishida emas");
        }
        if (string.IsNullOrEmpty(studentId))
        {
            throw new ArgumentNullException("Student ID is null or empty");
        }

        var existingStudent = await _unitOfWork.StudentInterface.GetByIdAsync(studentId);
        if (existingStudent is null)
        {
            throw new CustomException("Student not found");
        }

        await _unitOfWork.StudentInterface.DeleteAsync(studentId);
    }

    public async Task<List<StudentReturnDto>> GetAllStudent()
    {
        var students = await _unitOfWork.StudentInterface.GetAllAsync();
        var studentReturnDtos = new List<StudentReturnDto>();

        foreach (var item in students)
        {
            var guruhReturnDtos = new List<GuruhReturnDto>();

            if (item.GruopIds != null)
            {
                foreach (var id in item.GruopIds)
                {
                    var guruh = await _unitOfWork.GuruhInterface.GetByIdAsync(id);
                    if (guruh != null)
                    {
                        var teacher = await _userManager.FindByIdAsync(guruh.TeacherId!);
                        guruhReturnDtos.Add(new GuruhReturnDto
                        {
                            Id = guruh.Id,
                            GroupName = guruh.GroupName,
                            Weekdays = guruh.Weekdays,
                            Start = guruh.Start,
                            End = guruh.End,
                            Price = guruh.Price,
                            Duration = guruh.Duration,
                            Room = await _unitOfWork.RoomInterface.GetByIdAsync(guruh.RoomId!),
                            Teacher = teacher!,
                            Fan = await _unitOfWork.FanRepository.GetByIdAsync(guruh.FanId!)
                        }); 
                    }
                }
            }

            studentReturnDtos.Add(new StudentReturnDto
            {
                Id = item.Id.ToString(),
                FirstName = item.FirstName,
                LastName = item.LastName,
                PhoneNumber = item.PhoneNumber,
                GuruhReturnDtos = guruhReturnDtos
            });
        }

        return studentReturnDtos;
    }
    public async Task<List<StudentReturnDto>> GetByIdStudent(string id)
    {
        if (!ObjectId.TryParse(id, out ObjectId objectId))
        {
            throw new CustomException("Student  identifikatorlari ObjectId ko'rinishida emas");
        }
        var student = await _unitOfWork.StudentInterface.GetByIdAsync(id);
        if (student is null)
        {
            throw new NotFoundException("Bunday student mavjud emas");
        }

        var studentReturnDto = new List<StudentReturnDto>();
        var guruhReturnDtos = new List<GuruhReturnDto>();

        if (student.GruopIds != null)
        {
            foreach (var groupId in student.GruopIds)
            {
                var guruh = await _unitOfWork.GuruhInterface.GetByIdAsync(groupId);
                if (guruh != null)
                {
                    var teacher = await _userManager.FindByIdAsync(guruh.TeacherId!);
                    var room = await _unitOfWork.RoomInterface.GetByIdAsync(guruh.RoomId!);
                    var fan = await _unitOfWork.FanRepository.GetByIdAsync(guruh.FanId!);

                    guruhReturnDtos.Add(new GuruhReturnDto
                    {
                        Id = guruh.Id,
                        GroupName = guruh.GroupName,
                        Weekdays = guruh.Weekdays,
                        Start = guruh.Start,
                        End = guruh.End,
                        Price = guruh.Price,
                        Duration = guruh.Duration,
                        Room = room,
                        Teacher = teacher!,
                        Fan = fan
                    });
                }
            }
        }

        studentReturnDto.Add(new StudentReturnDto
        {
            Id = student.Id.ToString(),
            FirstName = student.FirstName,
            LastName = student.LastName,
            PhoneNumber = student.PhoneNumber,
            GuruhReturnDtos = guruhReturnDtos
        });

        return studentReturnDto;
    }
}

