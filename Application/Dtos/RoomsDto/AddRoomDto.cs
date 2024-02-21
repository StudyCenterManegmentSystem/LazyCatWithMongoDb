using Domain.Entities.Entity.Rooms;

namespace Application.Dtos.RoomsDto;

public class AddRoomDto
{
    public string RoomName { get; set; } = string.Empty;
    public int Qavat { get; set; }
    public int Sigimi { get; set; }

    public static implicit operator Room(AddRoomDto addRoomDto)
        => new()
        {
            RoomName = addRoomDto.RoomName,
            Qavat = addRoomDto.Qavat,
            Sigimi = addRoomDto.Sigimi
        };
}
