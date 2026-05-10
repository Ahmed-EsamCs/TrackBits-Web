using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackBits.Services.SendGridEmailService
{
    public class LicenseGeneratorService : ILicenseGeneratorService
{
    private readonly ApplicationDbContext _context;

    public LicenseGeneratorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public string GenerateKey() => $"TRK-{Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper()}";

    public async Task SeedInitialSerialsAsync(int count)
    {
        var serials = new List<SerialNumber>();
        for (int i = 0; i < count; i++)
        {
            serials.Add(new SerialNumber { 
                HashedSerial = GenerateKey(), 
                Status = SerialStatus.Unused, 
                DurationInDays = 30 
            });
        }
        _context.SerialNumbers.AddRange(serials);
        await _context.SaveChangesAsync();
    }
}
}