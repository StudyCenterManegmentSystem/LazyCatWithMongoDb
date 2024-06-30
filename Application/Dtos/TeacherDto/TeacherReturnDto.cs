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
            if (teacher == null)
            {
                return null; // or handle the case where teacher is null
            }

            return new TeacherReturnDto
            {
                TeacherId = teacher.Id.ToString() ?? string.Empty,
                Email = teacher.Email ?? string.Empty,
                FirstName = teacher.FirstName ?? string.Empty,
                LastName = teacher.LastName ?? string.Empty,
            };

        }
    }
}