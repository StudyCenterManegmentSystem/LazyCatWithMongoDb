
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        RoomInterface = new RoomRepository(_dbContext.Rooms);
        FanRepository = new FanRepository(_dbContext.Fans);
        GuruhInterface = new GuruhRepository(_dbContext.Guruhlar);
        StudentInterface = new StudentRepository(_dbContext.Students);
        PaymentInterface = new PaymentRepository(_dbContext.Payments);
        AttendanceInterface = new AttendanceRepository(_dbContext.Attendances);
    }
    private readonly ApplicationDbContext _dbContext;
    public IRoomInterface RoomInterface { get; }

    public IFanRepository FanRepository { get; }
    public IGuruhInterface GuruhInterface { get; }

    public IStudentInterface StudentInterface { get; }
    public IPaymentInterface PaymentInterface { get; }

    public IAttendanceInterface AttendanceInterface { get; }
}
