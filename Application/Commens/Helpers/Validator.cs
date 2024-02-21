using Domain.Entities.Entity.Rooms;

namespace Application.Commens.Helpers;

public static class Validator
{
    public static bool IsExist(this Room room, IEnumerable<Room> rooms)
        => rooms.Any(r => r.Qavat == room.Qavat
                    && room.RoomName == r.RoomName
                    && room.Sigimi == r.Sigimi);

    public static bool IsValid(this Room room)
        => !string.IsNullOrEmpty(room.RoomName)
        && room.Sigimi >= 10
        && room.Sigimi <= 30
        && room.Qavat >= 1;
}
