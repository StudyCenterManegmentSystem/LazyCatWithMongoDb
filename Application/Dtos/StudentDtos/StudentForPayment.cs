using Application.Dtos.GroupsDto;
using Domain.Entities.Entity;

namespace Application.Dtos.StudentDtos;

public class StudentForPayment : BaseEntity
{
    [Required, StringLength(100)]
    public string FirstName { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string LastName { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string PhoneNumber { get; set; } = string.Empty;
    public GuruhReturnDto? Guruh {  get; set; }  
}
