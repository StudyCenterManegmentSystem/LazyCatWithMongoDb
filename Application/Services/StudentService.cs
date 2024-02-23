using Application.Dtos.StudentDtos;
using Domain.Entities.Entity.Students;

namespace Application.Services;

public class StudentService (IUnitOfWork unitOfWork) : IStudentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Student> AddStudentAsync(AddStudentDto dto)
    {
        if(dto is null)
        {
            throw new ArgumentNullException("StudentDto is null");
        }
        var student = (Student)dto;
        if(student is null)
        {
            throw new ArgumentNullException("Student is null");
        }
        var guruhIds = student.GruopIds;
        foreach (var item in guruhIds)
        {
            var guruh = await _unitOfWork.GuruhInterface.GetByIdAsync(item);
            if(guruh is null)
            {
                throw new CustomException("Bunday guruh mavjud emas");
            }
        }
        if (!student.IsValid())
        {
            throw new CustomException("Student is invalid");
        }
        var students = await _unitOfWork.StudentInterface.GetAllAsync();

        if (student.IsExist(students))
        {
            throw new CustomException("Student is already exist");
        }
        await _unitOfWork.StudentInterface.AddAsync(student);
        return student;
    }

    public async Task<Student> UpdateStudentAsync(UpdateStudentDto dto)
    {
    
        if (dto is null)
        {
            throw new ArgumentNullException("UpdateStudentDto is null");
        }
        var student = (Student)dto;
        var existingStudent = await _unitOfWork.StudentInterface.GetByIdAsync(student.Id);
        if (existingStudent is null)
        {
            throw new CustomException("Student not found");
        }
        if (!student.IsValid())
        {
            throw new CustomException("Updated student is invalid");
        }

        var students = await _unitOfWork.StudentInterface.GetAllAsync();
        if (student.IsExist(students))
        {
            throw new CustomException("Updated student already exists");
        }

        await _unitOfWork.StudentInterface.UpdateAsync(student);
        return existingStudent;
    }


    public async Task DeleteStudentAsync(string studentId)
    {
        if (string.IsNullOrEmpty(studentId))
        {
            throw new ArgumentNullException("Student ID is null or empty");
        }

        var existingStudent = await _unitOfWork.StudentInterface.GetByIdAsync(studentId);
        if (existingStudent is null)
        {
            throw new CustomException("Student not found");
        }

        await _unitOfWork.StudentInterface.DeleteAsync(studentId);
    }
}
