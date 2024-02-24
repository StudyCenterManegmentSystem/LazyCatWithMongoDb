namespace Infrastructure.Interfaces;

public interface IUnitOfWork
{
    IRoomInterface RoomInterface { get; }
    IFanRepository FanRepository { get; } 
    IGuruhInterface GuruhInterface { get; }
    IStudentInterface StudentInterface { get; }
    IPaymentInterface PaymentInterface { get; }
    
}
