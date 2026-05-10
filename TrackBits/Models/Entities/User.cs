using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TrackBits.Models.Entities
{
    public class User:IdentityUser
    {
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public string BusinessUserName { get; set; }

  
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<SerialNumber> SerialNumbers { get; set; } = new List<SerialNumber>();

    }
}
