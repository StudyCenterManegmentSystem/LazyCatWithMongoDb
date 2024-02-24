namespace Infrastructure.Repositories;

public class PaymentRepository(IMongoCollection<Payment> collection)
 : Repository<Payment>(collection), IPaymentInterface
{
}
