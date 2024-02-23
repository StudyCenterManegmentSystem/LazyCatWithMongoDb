using Application.Dtos.GroupsDto;
using Domain.Entities.Entity.Groups;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Application.Services;

public class GuruhService(IUnitOfWork unitOfWork,
                          UserManager<Teacher> userManager) : IGuruhService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<Teacher> _userManager = userManager;

    public async Task<Guruh> AddGroupAsync(AddGroupDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto), "Group is null");
        }

        var group = (Guruh)dto;

        if (!group.IsValid())
        {
            throw new CustomException("Gruh is invalid");
        }

        var existingGroups = await _unitOfWork.GuruhInterface.GetAllAsync();

        if (existingGroups == null)
        {
            throw new NotFoundException("Gruh topilmadi");
        }

        if (group.IsExist(existingGroups))
        {
            throw new CustomException("Gruh is already exist");
        }

        var teacher = await _userManager.FindByIdAsync(group.TeacherId!);

        if (teacher == null)
        {
            throw new NotFoundException("Teacher topilmadi");
        }

        var fan = await _unitOfWork.FanRepository.GetByIdAsync(group.FanId!);

        if (fan == null)
        {
            throw new NotFoundException("Fan topilmadi");
        }

        var room = await _unitOfWork.RoomInterface.GetByIdAsync(group.RoomId!);

        if (room == null)
        {
            throw new NotFoundException("Room topilmadi");
        }

        var weekdays = group.Weekdays;

        if (weekdays == null || weekdays.Count == 0 || weekdays.Count > 7)
        {
            throw new CustomException("Hafta kunlari noto'g'ri");
        }

        foreach (var weekday in weekdays)
        {
            if (weekday < 0 || weekday > 6) 
            {
                throw new CustomException("Hafta kuni noto'g'ri");
            }
        }

        await _unitOfWork.GuruhInterface.AddAsync(group);
        return group;
    }
    public async Task<Guruh> UpdateAsync(UpdateGroupDto updateGroupDto)
    {
        if (updateGroupDto == null)
        {
            throw new ArgumentNullException(nameof(updateGroupDto), "UpdateGroupDto is null");
        }

        var group = (Guruh)updateGroupDto;

        if (!group.IsValid())
        {
            throw new CustomException("Gruh is invalid");
        }

        var existingGroup = await _unitOfWork.GuruhInterface.GetByIdAsync(group.Id);

        if (existingGroup == null)
        {
            throw new NotFoundException("Gruh topilmadi");
        }

        var teacher = await _userManager.FindByIdAsync(group.TeacherId!);

        if (teacher == null)
        {
            throw new NotFoundException("Teacher topilmadi");
        }

        var fan = await _unitOfWork.FanRepository.GetByIdAsync(group.FanId!);

        if (fan == null)
        {
            throw new NotFoundException("Fan topilmadi");
        }

        var room = await _unitOfWork.RoomInterface.GetByIdAsync(group.RoomId!);

        if (room == null)
        {
            throw new NotFoundException("Room topilmadi");
        }

        var weekdays = group.Weekdays;

        if (weekdays == null || weekdays.Count == 0 || weekdays.Count > 7)
        {
            throw new CustomException("Hafta kunlari noto'g'ri");
        }

        foreach (var weekday in weekdays)
        {
            if (weekday < 0 || weekday > 6) 
            {
                throw new CustomException("Hafta kuni noto'g'ri");
            }
        }

        await _unitOfWork.GuruhInterface.UpdateAsync(group);
        return group;
    }

    public async Task<List<GuruhReturnDto>> GetAllGuruhAsync()
    {
        var groups = await _unitOfWork.GuruhInterface.GetAllAsync();
        if(groups is null)
        {
            throw new NotFoundException("Hech qanday quruh mavjud emas");
        }
        List<GuruhReturnDto> guruhReturnDtos = new();
        foreach (var item in groups)
        {
            var teacher = await _userManager.FindByIdAsync(item.TeacherId!);
            var room = await _unitOfWork.RoomInterface.GetByIdAsync(item.RoomId!);
            var fan = await _unitOfWork.FanRepository.GetByIdAsync(item.FanId!);
            var result = new GuruhReturnDto()
            {
                Id = item.Id.ToString(),
                GroupName = item.GroupName,
                Weekdays = item.Weekdays,
                Start = item.Start,
                End = item.End,
                Price = item.Price,
                Duration = item.Duration,
                Teacher = teacher,
                Fan = fan,
                Room = room
            };
            guruhReturnDtos.Add(result);
        }

        return guruhReturnDtos;

    }

    public async Task<GuruhReturnDto> GetByIdAsync(string id)
    {
        var guruh = await _unitOfWork.GuruhInterface.GetByIdAsync(id);
        var teacher = await _userManager.FindByIdAsync(guruh.TeacherId!);
        if(guruh is null)
        {
            throw new NotFoundException("Guruh topilmadi");
        }
        var guruhReturnDto = new GuruhReturnDto()
        {
            Id = guruh.Id.ToString(),
            GroupName = guruh.GroupName,
            Weekdays = guruh.Weekdays,
            Start = guruh.Start,
            End = guruh.End,
            Price = guruh.Price,
            Duration = guruh.Duration,
            Teacher = teacher!,
            Fan = await _unitOfWork.FanRepository.GetByIdAsync(guruh.FanId!),
            Room = await _unitOfWork.RoomInterface.GetByIdAsync(guruh.RoomId!)

        };
        return guruhReturnDto;
    }
}
