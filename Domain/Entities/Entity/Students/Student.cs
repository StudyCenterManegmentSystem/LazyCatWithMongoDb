namespace Domain.Entities.Entity.Students;

public class Student : BaseEntity
{
    [Required, StringLength(100)]
    public string FirstName { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string LastName { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string PhoneNumber { get; set; } = string.Empty;

    public List<string>? GruopIds { get; set; }
}
