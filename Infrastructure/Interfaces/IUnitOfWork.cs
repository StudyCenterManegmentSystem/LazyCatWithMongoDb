namespace Infrastructure.Interfaces;

public interface IUnitOfWork
{
    IRoomInterface RoomInterface { get; }
}
