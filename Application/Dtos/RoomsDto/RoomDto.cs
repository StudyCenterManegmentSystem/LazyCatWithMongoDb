using Amazon.Util;
using Domain.Entities.Entity.Rooms;

namespace Application.Dtos.RoomsDtol;

public class RoomDto : BaseDto
{
    [Required, StringLength(100)]
    public string RoomName { get; set; } = string.Empty;

    public int Qavat { get; set; }

    [Required]
    public int Sigimi { get; set; }

    public static implicit operator RoomDto(Room room)
        => new()
        {
            Id = room.Id,
            RoomName = room.RoomName,
            Qavat = room.Qavat,
            Sigimi = room.Sigimi
        };

    public static implicit operator Room(RoomDto room)
        => new()
        {
            Id = room.Id,
            RoomName = room.RoomName,
            Qavat = room.Qavat,
            Sigimi = room.Sigimi
        };
}
