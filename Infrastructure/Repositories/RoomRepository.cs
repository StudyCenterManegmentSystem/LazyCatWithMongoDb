using Domain.Entities.Entity.Rooms;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class RoomRepository(IMongoCollection<Room> collection)
    : Repository<Room>(collection), IRoomInterface
{
}
