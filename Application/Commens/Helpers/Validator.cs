


using Domain.Entities.Entity.Groups;

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

    public static bool IsExist(this Fan fan, IEnumerable<Fan> fans)
       => fans.Any(f => f.FanName == fan.FanName
           && f.FanDescription == fan.FanDescription);

    public static bool IsValid(this Fan fan)
        => !string.IsNullOrEmpty(fan.FanName)
        && !string.IsNullOrEmpty(fan.FanDescription);

    public static bool IsExist(this Guruh group, IEnumerable<Guruh> groups)
    {
        return groups.Any(f => f.GroupName == group.GroupName &&
                                f.Price == group.Price &&
                                f.Duration == group.Duration &&
                                f.RoomId == group.RoomId &&
                                f.TeacherId == group.TeacherId &&
                                f.Start == group.Start &&
                                f.End == group.End &&
                                f.Weekdays.SequenceEqual(group.Weekdays) &&
                                f.FanId == group.FanId);
    }

    public static bool IsValid(this Guruh group)
    {
        return !string.IsNullOrEmpty(group.GroupName) &&
               !string.IsNullOrEmpty(group.RoomId!.ToString()) &&
               !string.IsNullOrEmpty(group.FanId!.ToString()) &&
               !string.IsNullOrEmpty(group.TeacherId!.ToString()) &&
               !string.IsNullOrEmpty(group.Duration) &&
               group.Price > 0 &&
               group.Start <= group.End; 
    }

}
