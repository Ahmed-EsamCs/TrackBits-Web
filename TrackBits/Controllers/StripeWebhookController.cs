using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using TrackBits.Data;
using TrackBits.Models.Entities;
using TrackBits.Models.Enums;
using TrackBits.Services.SendGridEmailService;



namespace TrackBits.Controllers
{
    [ApiController]
    [Route("api/stripe/webhook")]
    public class StripeWebhookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly ISendGridEmailService _email;

        public StripeWebhookController(ApplicationDbContext context, IConfiguration config, ISendGridEmailService email)
        {
            _context = context;
            _config = config;
            _email = email;
        }










        [HttpPost]
        public async Task<IActionResult> Handle()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var endpointSecret = _config["Stripe:WebhookSecret"];

            Event stripeEvent;

            try
            {
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    endpointSecret
                );
            }
            catch
            {
                return BadRequest();
            }

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Session;

                var paymentId = int.Parse(session.Metadata["PaymentId"]);
                var userId = session.Metadata["UserId"];


                var payment = await _context.Payments.FindAsync(paymentId);


            }

            return Ok();
        }
    }
}




