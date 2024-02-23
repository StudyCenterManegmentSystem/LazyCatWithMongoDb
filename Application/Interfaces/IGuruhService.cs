using Application.Dtos.GroupsDto;
using Domain.Entities.Entity.Groups;

namespace Application.Interfaces;

public interface IGuruhService
{
    Task<Guruh> AddGroupAsync(AddGroupDto dto);
    Task<Guruh> UpdateAsync(UpdateGroupDto updateGroupDto);
    Task<List<GuruhReturnDto>> GetAllGuruhAsync();
    Task<GuruhReturnDto> GetByIdAsync(string id);
    Task DeleteAsync(string id);
}
