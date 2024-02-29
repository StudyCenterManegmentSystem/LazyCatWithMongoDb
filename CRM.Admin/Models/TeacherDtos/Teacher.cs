using System.ComponentModel.DataAnnotations;

namespace CRM.Admin.Models.TeacherDtos;

public class Teacher
{
    public string? Id { get; set; }
    public string Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;

    public List<string>? FanIds { get; set; }
}
