using Application.Commens.Helpers;
using Application.Dtos.RoomsDto;
using Application.Dtos.RoomsDtol;

namespace Application.Interfaces;

public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAllAsync();
    Task<RoomDto> GetByIdAsync(string id);
    Task<PagedList<RoomDto>> GetPagedReponseAsync(int pageNumber, int pageSize);
    Task AddAsync(AddRoomDto room);
    Task UpdateAsync(RoomDto room);
    Task DeleteAsync(string id);
}
