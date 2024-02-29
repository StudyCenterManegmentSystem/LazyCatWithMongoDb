namespace CRM.Admin.Models.RoomDtos
{
    public class Room
    {
        public string? Id { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public int Qavat { get; set; }
        public int Sigimi { get; set; }
    }
}
