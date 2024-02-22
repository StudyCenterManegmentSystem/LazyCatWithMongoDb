using Application.Commens.Helpers;
using Application.Dtos.FanDtos;
using Application.Dtos.TeacherDto;
using Domain.Entities.Entity.Fans;
using Domain.Entities.Entity.Teachers;
using Infrastructure.Interfaces;

namespace Application.Services;

public class FanService(IUnitOfWork unitOfWork) : IFanService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddAsync(AddFanDto fanDto)
    {
        if (fanDto == null)
        {
            throw new ValidationException("Fan cannot be null");
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
        await _unitOfWork.FanRepository.DeleteAsync(id);
    }


    public async Task<List<FanDto>> GetAllAsync()
    {
        var fans = await _unitOfWork.FanRepository.GetAllAsync();
        return fans.Select(x => (FanDto)x).ToList();
    }
    public async Task<FanDto> GetByIdAsync(string id)
    {
        var fan = await _unitOfWork.FanRepository.GetByIdAsync(id);
        return fan;
    }
    public async Task UpdateAsync(FanDto fanDto)
    {
        var fans = await _unitOfWork.FanRepository.GetAllAsync();
        var fanmap = (Fan)fanDto;
        await _unitOfWork.FanRepository.UpdateAsync(fanmap);

    }

  
}