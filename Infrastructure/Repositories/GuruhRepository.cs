
namespace Infrastructure.Repositories;

public class GuruhRepository(IMongoCollection<Guruh> collection)
 : Repository<Guruh>(collection), IGuruhInterface
{
    
}

