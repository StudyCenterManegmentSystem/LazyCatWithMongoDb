using Application.Dtos.GroupsDto;

namespace Application.Dtos.StudentDtos;

public class StudentReturnDto : BaseDto
{
    [Required, StringLength(100)]
    public string FirstName { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string LastName { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string PhoneNumber { get; set; } = string.Empty;

    public List<GuruhReturnDto> GuruhReturnDtos { get; set; }
}
