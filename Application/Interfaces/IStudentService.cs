using Application.Dtos.StudentDtos;
using Domain.Entities.Entity.Students;

namespace Application.Interfaces;

public interface IStudentService
{
    Task<Student> AddStudentAsync(AddStudentDto dto);
    Task<Student> UpdateStudentAsync(UpdateStudentDto dto);
    Task DeleteStudentAsync(string studentId);
}

