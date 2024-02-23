using Domain.Entities.Entity.Students;

namespace Application.Dtos.StudentDtos;

public class AddStudentDto
{
    [Required, StringLength(100)]
    public string FirstName { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string LastName { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string PhoneNumber { get; set; } = string.Empty;

    public List<string>? GruopIds { get; set; }

    public static implicit operator AddStudentDto(Student student)
        => new AddStudentDto()
        {
            FirstName = student.FirstName,
            LastName = student.LastName,
            PhoneNumber = student.PhoneNumber,
            GruopIds = student.GruopIds
        };
    public static implicit operator Student(AddStudentDto student)
     => new Student()
     {
         FirstName = student.FirstName,
         LastName = student.LastName,
         PhoneNumber = student.PhoneNumber,
         GruopIds = student.GruopIds
     };
}
