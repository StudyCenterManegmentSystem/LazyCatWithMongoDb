using Application.Commens.Helpers;
using Application.Dtos.RoomsDto;
using Application.Dtos.RoomsDtol;
using Domain.Entities.Entity.Rooms;
using Infrastructure.Interfaces;

namespace Application.Services;

public class RoomService(IUnitOfWork unitOfWork) : IRoomService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddAsync(AddRoomDto roomDto)
    {
        if (roomDto == null)
        {
            throw new ValidationException("Room cannot be null");
        }

        var rooms = await _unitOfWork.RoomInterface.GetAllAsync();
        var room = (Room) roomDto;

        if (room.IsExist(rooms))
        {
            throw new CustomException("Room already exist");
        }

        if (!room.IsValid())
        {
            throw new CustomException("Room is not valid");
        }

        try
        {
            await _unitOfWork.RoomInterface.AddAsync(room);
        }
        catch(CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
        catch (NotFoundException ex)
        {
            throw new NotFoundException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteAsync(string id)
    {
        try
        {
            await _unitOfWork.RoomInterface.DeleteAsync(id);
        }
        catch (CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
        catch (NotFoundException ex)
        {
            throw new NotFoundException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public async Task<IEnumerable<RoomDto>> GetAllAsync()
    {
        try
        {
            var rooms = await _unitOfWork.RoomInterface.GetAllAsync();

            return rooms.Select(x => (RoomDto) x);
        }
        catch (CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
        catch (NotFoundException ex)
        {
            throw new NotFoundException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<RoomDto> GetByIdAsync(string id)
    {
        try
        {
            var room = await _unitOfWork.RoomInterface.GetByIdAsync(id);
            return (RoomDto)room;
        }
        catch (CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
        catch (NotFoundException ex)
        {
            throw new NotFoundException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<PagedList<RoomDto>> GetPagedReponseAsync(int pageNumber, int pageSize)
    {
        try
        {
            var rooms = await GetAllAsync();
            PagedList<RoomDto> pagedList = new(rooms.ToList(), rooms.ToList().Count, pageNumber, pageSize);
            return pagedList;
        }
        catch (CustomException ex)
        {   
            throw new CustomException(ex.Message);
        }
        catch (NotFoundException ex)
        {
            throw new NotFoundException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateAsync(RoomDto roomDto)
    {
        if (roomDto == null)
        {
            throw new ValidationException("Room cannot be null");
        }

        var rooms = await _unitOfWork.RoomInterface.GetAllAsync();
        var room = (Room)roomDto;

        if (room.IsExist(rooms))
        {
            throw new CustomException("Room already exist");
        }

        if (!room.IsValid())
        {
            throw new CustomException("Room is not valid");
        }

        try
        {
            await _unitOfWork.RoomInterface.UpdateAsync(room);
        }
        catch (CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
        catch (NotFoundException ex)
        {
            throw new NotFoundException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
