using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class StudentRepository : Repository<Student>, IStudentInterface
{
    public StudentRepository(IMongoCollection<Student> collection) : base(collection)
    {
    }
}
