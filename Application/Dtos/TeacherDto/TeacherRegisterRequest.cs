using Domain.Entities.Entity.Teachers;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.TeacherDto;

public class TeacherRegisterRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required, DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public List<string>? FanIds { get; set; }

    public static implicit operator TeacherReturnDto(TeacherRegisterRequest teacherRequest)
    {
        return new TeacherReturnDto
        {
            Email = teacherRequest.Email,
            FirstName = teacherRequest.FirstName,
            LastName = teacherRequest.LastName,
        };
    }
}
