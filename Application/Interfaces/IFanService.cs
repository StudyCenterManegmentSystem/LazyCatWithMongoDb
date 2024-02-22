using Application.Commens.Helpers;
using Application.Dtos.FanDtos;

namespace Application.Interfaces;

public interface IFanService
{
    Task AddAsync(AddFanDto fanDto);
    Task<List<FanDto>> GetAllAsync();

    Task<FanDto> GetByIdAsync(string id);
    Task UpdateAsync(FanDto fanDto);
    Task DeleteAsync(string id);
}