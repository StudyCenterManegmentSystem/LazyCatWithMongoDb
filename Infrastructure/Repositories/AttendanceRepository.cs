using Domain.Entities.Entity.Attendances;

namespace Infrastructure.Repositories;

public class AttendanceRepository : Repository<Attendance>, IAttendanceInterface
{
    public AttendanceRepository(IMongoCollection<Attendance> collection) : base(collection)
    {
    }
}
