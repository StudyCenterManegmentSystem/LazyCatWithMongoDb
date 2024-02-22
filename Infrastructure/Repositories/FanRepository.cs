using Domain.Entities.Entity.Fans;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class FanRepository(IMongoCollection<Fan> collection)
 : Repository<Fan>(collection), IFanRepository
{
}