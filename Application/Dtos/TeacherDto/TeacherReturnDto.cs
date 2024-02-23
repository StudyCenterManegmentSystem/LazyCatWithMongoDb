using Domain.Entities.Entity.Teachers;

namespace Application.Dtos.TeacherDto
{
    public class TeacherReturnDto
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

        public static implicit operator TeacherReturnDto(Teacher teacher)
        {
            return new TeacherReturnDto
            {
                TeacherId = teacher.Id.ToString(),
                Email = teacher.Email,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
            };
        }
    }
}