

namespace Application.Services;

public class FanService(IUnitOfWork unitOfWork, 
                        UserManager<Teacher> userManager) : IFanService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<Teacher> _userManager = userManager;

    public async Task AddAsync(AddFanDto fanDto)
    {
        if (fanDto == null)
        {
            throw new ArgumentNullException("Fan cannot be null");
        }
        if(fanDto.FanName is null)
        {
            throw new ArgumentNullException("Fan Name cannot be null");

        }
        if (fanDto.FanDescription is null)
        {
            throw new ArgumentNullException("Fan Description cannot be null");

        }
        var fan = (Fan)fanDto;
        var fanList = await _unitOfWork.FanRepository.GetAllAsync();
        if (!fan.IsValid())
        {
            throw new CustomException("Fan is not valid");
        }
        if (fan.IsExist(fanList))
        {
            throw new CustomException("Fan already exist");
        }
        await _unitOfWork.FanRepository.AddAsync(fan);
    }

    public async Task DeleteAsync(string id)
    {
        if (!ObjectId.TryParse(id, out ObjectId objectId))
        {
            throw new CustomException("Fan identifikatorlari ObjectId ko'rinishida emas");
        }
        var fan = await _unitOfWork.FanRepository.GetByIdAsync(id);
        if (fan is null)
        {
            throw new NotFoundException("Fan topilmadi");
        }
        await _unitOfWork.FanRepository.DeleteAsync(id);

    }

    public async Task UpdateAsync(FanDto fanDto)
    {
        var id = fanDto.Id;
        if (!ObjectId.TryParse(id, out ObjectId objectId))
        {
            throw new CustomException("Fan identifikatorlari ObjectId ko'rinishida emas");
        }
        if (fanDto == null)
        {
            throw new ArgumentNullException("Fan cannot be null");
        }
        var fan1 = await _unitOfWork.FanRepository.GetByIdAsync(id);
        if (fan1 is null)
        {
            throw new CustomException("Bunday fan mavjud emas");
        }
        var fan = (Fan)fanDto;

        if (!fan.IsValid())
        {
            throw new CustomException("Fan is not valid");
        }

        await _unitOfWork.FanRepository.UpdateAsync(fan);

    }
    public async Task<List<FanDto>> GetAllAsync()
    {
        var fans = await _unitOfWork.FanRepository.GetAllAsync();
        if(fans is null)
        {
            throw new NotFoundException("Hech qanday fan mavjud emas");
        }
        return fans.Select(x => (FanDto)x).ToList();
    }

    public async Task<List<FanTeachersDto>> GetAllFanTeachers()
    {
        var teachers = await _userManager.GetUsersInRoleAsync("Teacher"); // Assuming "Teacher" is the role for teachers
        var fans = await _unitOfWork.FanRepository.GetAllAsync();

        var fanTeachersDtoList = new List<FanTeachersDto>();

        foreach (var fan in fans)
        {
            var fanTeachersDto = new FanTeachersDto
            {
                Id = fan.Id,
                FanName = fan.FanName,
                FanDescription = fan.FanDescription,
                Teachers = new List<TeacherReturnDto>()
                
            };

            foreach (var teacher in teachers)
            {
                var fanids = teacher.FanIds;
                if (fanids.Contains(fan.Id))
                {
                    fanTeachersDto.Teachers.Add(new TeacherReturnDto
                    {
                        TeacherId = teacher.Id.ToString(),
                        Email = teacher.Email,
                        FirstName = teacher.FirstName,
                        LastName = teacher.LastName
                    });
                }
            
            }

            fanTeachersDtoList.Add(fanTeachersDto);
        }

        return fanTeachersDtoList;
    }

    public async Task<FanTeachersDto> GetByIdFanWithTeachers(string id)
    {
        if (!ObjectId.TryParse(id, out ObjectId objectId))
        {
            throw new CustomException("Fan identifikatorlari ObjectId ko'rinishida emas");
        }
        var fan = await _unitOfWork.FanRepository.GetByIdAsync(id);

        if (fan == null)
        {
            throw new NotFoundException("Fan topilmadi");
        }


        var teachers = await _userManager.GetUsersInRoleAsync("Teacher");

        var fanTeachersDto = new FanTeachersDto
        {
            Id = fan.Id,
            FanName = fan.FanName,
            FanDescription = fan.FanDescription,
            Teachers = new List<TeacherReturnDto>()
        };

        foreach (var teacher in teachers)
        {
            var fanids = teacher.FanIds;
            if (fanids.Contains(fan.Id))
            {
                fanTeachersDto.Teachers.Add(new TeacherReturnDto
                {
                    TeacherId = teacher.Id.ToString(),
                    Email = teacher.Email,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName
                });
            }
        }

        return fanTeachersDto;
    }

    public async Task<FanDto> GetByIdAsync(string id)
    {
        var fan = await _unitOfWork.FanRepository.GetByIdAsync(id);
        if(fan is null)
        {
            throw new NotFoundException("Fan topilmadi");
        }
        return fan;
    }  



  
}