using TrackBits.Models.Enums;

namespace TrackBits.Models.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        //fk
        public string UserId { get; set; }
        public User User { get; set; } = null!;
        public decimal Amount { get; set; }
      
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public string? Provider { get; set; }
        public string? ProviderPaymentId { get; set; }
        public DateTime CreatedAt { get; set; }



    }
}
