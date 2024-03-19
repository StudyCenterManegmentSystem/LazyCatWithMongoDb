using Domain.Entities.Entity.Attendances;
using Domain.Entities.Entity.Groups;
using Domain.Entities.Entity.Payments;
using Domain.Entities.Entity.Students;

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

    public static bool IsExist(this Student student, IEnumerable<Student> students)
    {
        return students.Any(s => s.FirstName == student.FirstName &&
                                 s.LastName == student.LastName &&
                                 s.PhoneNumber == student.PhoneNumber);
    }



    public static bool IsValid(this Student student)
    {
        return !string.IsNullOrEmpty(student.FirstName) &&
               !string.IsNullOrEmpty(student.LastName) &&
               !string.IsNullOrEmpty(student.PhoneNumber);
    }

    public static bool IsValid(this Payment payment)
    {
        return !string.IsNullOrEmpty(payment.GroupId!.ToString()) &&
               !string.IsNullOrEmpty(payment.StudentId!.ToString()) &&
               payment.QanchaTolagan > 0;
    }

    public static bool IsExist(this Payment payment, IEnumerable<Payment> payments)
    {
        return payments.Any(p => p.GroupId == payment.GroupId &&
                                 p.StudentId == payment.StudentId &&
                                 p.QanchaTolagan == payment.QanchaTolagan &&
                                 p.paymentType == payment.paymentType &&
                                 p.QachonTolagan == p.QachonTolagan);
    }
    public static bool IsValid(this Attendance attendance)
        => !string.IsNullOrEmpty(attendance.GroupId) &&
           !string.IsNullOrEmpty(attendance.TalabaId) &&
           attendance.Qachon == DateTime.Now;

    public static bool IsExist(this Attendance attendance, IEnumerable<Attendance> attendances)
        => attendances.Any(a => a.TalabaId == attendance.TalabaId &&
                                a.GroupId == attendance.GroupId &&
                                a.KeldiKemadi == attendance.KeldiKemadi &&
                                a.Qachon == attendance.Qachon);
}