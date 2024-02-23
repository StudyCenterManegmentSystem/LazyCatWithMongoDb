
namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        RoomInterface = new RoomRepository(_dbContext.Rooms);
        FanRepository = new FanRepository(_dbContext.Fans);
    }
    private readonly ApplicationDbContext _dbContext;
    public IRoomInterface RoomInterface { get; }

    public IFanRepository FanRepository { get; }
}
