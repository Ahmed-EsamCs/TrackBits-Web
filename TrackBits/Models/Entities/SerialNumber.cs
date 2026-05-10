using TrackBits.Models.Enums;

namespace TrackBits.Models.Entities
{
    public class SerialNumber
    {
        public int Id { get; set; }
        public User? AssignedToUser { get; set; } = null;
        public string HashedSerial { get; set; }

        public SerialStatus Status { get; set; } = SerialStatus.Unused;


        public  int DurationInDays {get ; set; } = 30;
        public DateTime CreatedAt { get; set; }
        //fk
        public string? AssignedToUserId { get; set; }


    }
}
