using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Stripe.Checkout;
using System.Threading.Tasks;
using TrackBits.Data;
using TrackBits.Models.Entities;
using TrackBits.Models.Enums;
using TrackBits.Services.SendGridEmailService;

namespace TrackBits.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISendGridEmailService _email;
        private readonly SendGridSettings _gridSettings;
         private readonly UserManager<User> _userManager;
        public PaymentsController(ApplicationDbContext context 
            , ISendGridEmailService email 
            , IOptions<SendGridSettings> gridSettings 
            ,UserManager<User> userManager )
        {
            _context = context;
            _email = email;
            _gridSettings = gridSettings.Value;
            _userManager = userManager;
        }


        [HttpPost("create")]

        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
        {

            var payment = new Payment
            {

                UserId = request.UserId,
                Amount = request.Amount,
                Provider = "Stripe",
                Status = PaymentStatus.Pending,
                CreatedAt = DateTime.Now

            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();



            string domain = "https://localhost:44381";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(payment.Amount * 100),
                            Currency = "EGP",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "TrackBits License"
                            }
                        },
                        Quantity = 1
                    }
                },

                SuccessUrl = domain + "/Payments/Success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "/Payments/Cancel",

                Metadata = new Dictionary<string, string>
                {
                    { "PaymentId", payment.Id.ToString() },
                    { "UserId", payment.UserId }
                }






            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            payment.ProviderPaymentId = session.Id;
            await _context.SaveChangesAsync();


            return Ok(new { url = session.Url });


        }

        [Authorize]

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) return RedirectToPage("/Account/Login", new { area = "Identity" });

            var payment = new Payment
            {
                UserId = userId,
                Amount = 3.99m, 
                Provider = "Stripe",
                Status = PaymentStatus.Pending,
                CreatedAt = DateTime.Now
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            string domain = "https://localhost:44381"; 
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(payment.Amount * 100), 
                    Currency = "usd", 
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "TrackBits Monthly Subscription"
                    }
                },
                Quantity = 1
            }
        },
                SuccessUrl = domain + "/Payments/Success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "/Payments/Cancel",
                Metadata = new Dictionary<string, string>
        {
            { "PaymentId", payment.Id.ToString() },
            { "UserId", payment.UserId }
        }
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            payment.ProviderPaymentId = session.Id;
            await _context.SaveChangesAsync();

            return Redirect(session.Url);
        }





        public async Task<IActionResult> Payment()
        {


            return View();
        }



        [HttpGet("/Payments/Success")]
        public async Task<IActionResult> Success(string session_id)
        {

            var payment = _context.Payments
                .FirstOrDefault(p => p.ProviderPaymentId == session_id);

            if (payment is null)
                return RedirectToAction("Error");

            if (payment.Status == PaymentStatus.Pending)
            {
                var service = new SessionService();

                Session session = await service.GetAsync(session_id);


                if (session.PaymentStatus == "paid")
                {
                    payment.Status = PaymentStatus.Success;

                    var serial = _context.SerialNumbers
                        .FirstOrDefault(serial => serial.Status == SerialStatus.Unused);

                    if (serial != null)
                    {
                        serial.Status = SerialStatus.Redeemed;
                        serial.AssignedToUserId = payment.UserId;
                        var user = await _context.Users.FindAsync(payment.UserId);

                        await _context.SaveChangesAsync();

                        if (user != null)
                            await _email.SendTemplateEmailAsync(user.Email,_gridSettings.SerialTemplateId,
                                new
                                { 
                                 userName = user.BusinessUserName,
                                 serial = serial.HashedSerial
                                });

                        return View(serial);


                    }
                    else
                    {

                        return Content("Payment done but serial numbers is out of stock please contact support");


                    }



                }
          

            }
            else if (payment.Status == PaymentStatus.Success)
            {
                var existingSerial = _context.SerialNumbers
                        .FirstOrDefault(serial => serial.AssignedToUserId == payment.UserId);

                return View(existingSerial);
            }


                return RedirectToAction("Error");



        }

        [HttpGet("/Payments/Cancel")]
        public IActionResult Cancel()
        {
            return View();
        }




        public class CreatePaymentRequest
        {
            public string UserId { get; set; }
            public decimal Amount { get; set; }
        }

    }
}
