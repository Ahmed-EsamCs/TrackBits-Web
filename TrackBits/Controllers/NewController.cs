using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TrackBits.Controllers
{
    // This controller is for testing purposes only. It allows us to generate a bulk of serial keys for testing the email sending functionality
    // it should be used in admin mode only and should be removed in production
   public class SerialsController : Controller
{
    private readonly ILicenseGeneratorService _licenseService;

    public SerialsController(ILicenseGeneratorService licenseService)
    {
        _licenseService = licenseService;
    }

    [HttpPost]
    public async Task<IActionResult> GenerateBulk()
    {
        await _licenseService.SeedInitialSerialsAsync(50);
        return Ok();
    }
}
}