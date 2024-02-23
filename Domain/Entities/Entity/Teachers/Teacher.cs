using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Entity.Teachers;
[CollectionName("teachers")]
public class Teacher : MongoIdentityUser<Guid>
{
    [Required, StringLength(100)]
    public string FirstName { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string LastName { get; set; } = string.Empty;
    public List<string>? FanIds { get; set; }
}
