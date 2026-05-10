using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackBits.Services
{
   public interface ILicenseGeneratorService
{
    string GenerateKey();
    Task SeedInitialSerialsAsync(int count);
}
}