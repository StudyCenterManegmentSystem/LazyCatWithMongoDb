namespace Infrastructure.Repositories;

public class FanRepository(IMongoCollection<Fan> collection)
 : Repository<Fan>(collection), IFanRepository
{
}