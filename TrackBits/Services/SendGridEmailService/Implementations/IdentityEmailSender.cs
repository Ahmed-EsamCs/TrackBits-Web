using Microsoft.AspNetCore.Identity.UI.Services;

namespace TrackBits.Services.SendGridEmailService
{
    public class IdentityEmailSender : IEmailSender
    {
        private readonly ISendGridEmailService _sendGrid;

        public IdentityEmailSender(ISendGridEmailService sendGrid)
        {
            _sendGrid = sendGrid;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await _sendGrid.SendEmailAsyncRaw(email, subject, htmlMessage);
        }
    }
}
