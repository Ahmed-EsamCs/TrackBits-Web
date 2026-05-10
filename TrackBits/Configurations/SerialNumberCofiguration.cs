using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TrackBits.Models.Entities;

namespace TrackBits.Configurations
{
    public class SerialNumberCofiguration : IEntityTypeConfiguration<SerialNumber>
    {
        public void Configure(EntityTypeBuilder<SerialNumber> builder)
        {

            //default values
            builder.Property(serial => serial.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");






        }
    }
}
