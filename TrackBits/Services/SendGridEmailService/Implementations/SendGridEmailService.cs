using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace TrackBits.Services.SendGridEmailService
{



    public class SendGridEmailService : ISendGridEmailService
    {
        private readonly SendGridSettings _settings;

        public SendGridEmailService(IOptions<SendGridSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<(int StatusCode, string Body)> SendEmailAsyncRaw(string to, string subject, string body)
        {
            var client = new SendGridClient(_settings.ApiKey);

            var from = new EmailAddress(_settings.FromEmail, _settings.FromName);
            var toEmail = new EmailAddress(to);

            var msg = MailHelper.CreateSingleEmail(from, toEmail, subject, null, body);

            var response = await client.SendEmailAsync(msg);
            var resultBody = await response.Body.ReadAsStringAsync();

            return ((int)response.StatusCode, resultBody);
        }
        public async Task SendTemplateEmailAsync(string to, string templateId, object templateData)
        {
            var client = new SendGridClient(_settings.ApiKey);

            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress(_settings.FromEmail, _settings.FromName));
            msg.AddTo(new EmailAddress(to));

            msg.SetTemplateId(templateId);
            msg.SetTemplateData(templateData);

            await client.SendEmailAsync(msg);
        }
    }



}
  





