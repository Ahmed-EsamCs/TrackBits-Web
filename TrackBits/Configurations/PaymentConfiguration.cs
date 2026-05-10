using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackBits.Models.Entities;

namespace TrackBits.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {

            builder.Property(payment => payment.CreatedAt)
               .HasDefaultValueSql("GETDATE()");
        }

        
    }
}
