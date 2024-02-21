using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Entity.Rooms;

public class Room : BaseEntity
{
    [Required, StringLength(100)]
    public string RoomName { get; set; } = string.Empty; 
    public int Qavat { get; set; }
    [Required]
    public int Sigimi { get; set; }
}
