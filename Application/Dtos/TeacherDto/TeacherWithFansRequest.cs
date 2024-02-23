using Domain.Entities.Entity.Fans;

namespace Application.Dtos.TeacherDto;

public class TeacherWithFansRequest
{
    public string TeacherId { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;
    public List<Fan> Fans { get; set; }
}
