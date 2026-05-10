namespace TrackBits.Services.SendGridEmailService
{
    public class SendGridSettings
    {
        public string ApiKey { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string VerificationTemplateId { get; set; }
        public string SerialTemplateId { get; set; }
        public string ResetPasswordTemplateId { get; set; }
        public string PasswordChangedTemplateId { get; set; }
    }
}
