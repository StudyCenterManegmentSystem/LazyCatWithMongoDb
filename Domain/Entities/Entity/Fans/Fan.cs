using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Entity.Fans;

public class Fan : BaseEntity
{
    [Required, StringLength(50)]
    public string FanName { get; set; } = string.Empty;
    [Required, StringLength(200)]
    public string FanDescription { get; set; } = string.Empty;
}
