namespace Infrastructure.Repositories;

public class RoomRepository(IMongoCollection<Room> collection)
    : Repository<Room>(collection), IRoomInterface
{
}
