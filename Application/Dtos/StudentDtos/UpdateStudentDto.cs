using Domain.Entities.Entity.Students;

namespace Application.Dtos.StudentDtos
{
    public class UpdateStudentDto : BaseDto
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string PhoneNumber { get; set; } = string.Empty;

        public List<string>? GruopIds { get; set; }

        public static implicit operator UpdateStudentDto(Student student)
            => new UpdateStudentDto()
            {
                Id = student.Id.ToString(),
                FirstName = student.FirstName,
                LastName = student.LastName,
                PhoneNumber = student.PhoneNumber,
                GruopIds = student.GruopIds
            };
        public static implicit operator Student(UpdateStudentDto student)
         => new Student()
         {
             Id = student.Id.ToString(),
             FirstName = student.FirstName,
             LastName = student.LastName,
             PhoneNumber = student.PhoneNumber,
             GruopIds = student.GruopIds
         };
    }
}
